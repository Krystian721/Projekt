namespace PrzetwarzanieObrazow
{
    #region Usings
    using Emgu.CV;
    using Emgu.CV.Structure;
    using System.ComponentModel;
    using System.Windows.Forms;
    #endregion
    public partial class Main : Form
    {
        #region Fields
        #endregion

        #region Public Properties
        #endregion
        public BackgroundWorker bw = new BackgroundWorker();

        #region Public Methods and Operators
        /// <summary>
        /// Main function of application
        /// </summary>
        public Main()
        {
            InitializeComponent();

            bw.DoWork += Bw_DoWork;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += Bw_ProgressChanged;

            bw.RunWorkerAsync();
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelPosition.Text = ImageProcessing.LaserSpotPosition.ToString();
            labelMaximumValue.Text = ImageProcessing.MaximumValue.ToString();

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //string FileName1 = @"C:\\Users\\kryst\\Desktop\\kaczka.png";
                string FileName2 = @"C:\Users\kryst\Desktop\night-forest-game-background-2d-260nw-231473788.jpg";
                Image<Bgr, byte> i = new Image<Bgr, byte>(FileName2);
                ImageProcessing.DetectLaserSpot(i);

                bw.ReportProgress(0, "");
                //ImageProcessing.DetectLaserSpot(new Image<Bgr, byte>(FileName1));
                //bw.ReportProgress(0, "");
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
