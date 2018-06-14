namespace GlownaAplikacja
{
    #region Usings
    using System.Windows.Forms;
    using System.ComponentModel;
    using Kaczki;
    using NAudio.CoreAudioApi;
    using System;
    using Emgu.CV.UI;
    using PrzetwarzanieObrazow;
    using Emgu.CV;
    using Emgu.CV.Structure;
    using Microsoft.Xna.Framework;
    #endregion

    public partial class MainWindow : Form
    {
        #region Fields
        private BackgroundWorker soundWorker;
        private BackgroundWorker imageProcessingWorker;
        private BackgroundWorker gameWorker;
        private Kaczki.Main game;
        private MMDevice device;
        private double soundVolume;
        private int soundBreakLevel;
        private int speed;
        #endregion

        #region Public Properties
        public BackgroundWorker SoundWorker { get => soundWorker; set => soundWorker = value; }
        public BackgroundWorker ImageProcessingWorker { get => imageProcessingWorker; set => imageProcessingWorker = value; }
        public Kaczki.Main Game { get => game; set => game = value; }
        public BackgroundWorker GameWorker { get => gameWorker; set => gameWorker = value; }
        public MMDevice Device { get => device; set => device = value; }
        public double SoundVolume { get => soundVolume; set => soundVolume = value; }
        public int SoundBreakLevel { get => soundBreakLevel; set => soundBreakLevel = value; }
        public int Speed { get => speed; set => speed = value; }
        #endregion

        #region Constructors and Deconstructors
        public MainWindow()
        {
            InitializeComponent();
            SoundBreakLevel = 75;

            SoundWorker = new BackgroundWorker();
            SoundWorker.DoWork += SoundWorker_DoWork;
            SoundWorker.RunWorkerCompleted += SoundWorker_RunWorkerCompleted;

            ImageProcessingWorker = new BackgroundWorker();
            ImageProcessingWorker.DoWork += ImageProcessingWorker_DoWork;
            ImageProcessingWorker.RunWorkerCompleted += ImageProcessingWorker_RunWorkerCompleted;

            GameWorker = new BackgroundWorker();
            GameWorker.DoWork += GameWorker_DoWork;

            imageProcessingWorker.RunWorkerAsync();
        }
        #endregion

        #region Methods
        private void SoundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                SoundVolume = Math.Round(Device.AudioMeterInformation.MasterPeakValue * 100);
            }));
        }

        private void SoundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SoundVolume > SoundBreakLevel)
            {
                ImageProcessingWorker.RunWorkerAsync();
            }
            SoundWorker.RunWorkerAsync();
        }

        private void ImageProcessingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Game.Game.LaserPosition = new Point(ImageProcessing.LaserSpotPosition.x, ImageProcessing.LaserSpotPosition.y);
            Game.Game.IsShooting = true;
        }

        private void ImageProcessingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ImageProcessing.CameraCapture();
            var fileName = "capture.jpg";
            Image<Bgr, byte> image = new Image<Bgr, byte>(fileName);
            ImageProcessing.DetectLaserSpot(image);
        }

        private void GameWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Game = new Kaczki.Main();
            Game.StartApplication();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
           if (MessageBox.Show("Czy na pewno chcesz opuścić aplikację?", "Czy na pewno?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void btnProfessionalMode_Click(object sender, System.EventArgs e)
        {
            GameWorker.RunWorkerAsync();
            SoundWorker.RunWorkerAsync();
        }

        private void btnOptions_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Options o = new Options();
            o.ShowDialog();
            if (o.DialogResult == DialogResult.OK)
            {
                if (o.Device != null)
                {
                    Device = o.Device;
                    this.btnProfessionalMode.Enabled = true;
                    lblError.Visible = false;                   
                }
                if (o.Speed > 0)
                {
                    Speed = o.Speed;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = " Aby rozpocząć należy wybrać urządzenie \n do nagrywania dżwięku na ekranie ustawień!";
                this.btnProfessionalMode.Enabled = false;
            }
            this.Show();
        }
        #endregion
    }
}
