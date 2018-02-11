namespace PracaInzynierska
{
    #region Usings
    using Emgu.CV;
    using Emgu.CV.Structure;
    using System.Windows.Forms;
    #endregion
    public partial class Main : Form
    {
        #region Private Properties
        #endregion

        #region Public Properties
        #endregion

        #region Public Functions
        /// <summary>
        /// Main function of application
        /// </summary>
        public Main()
        {
            InitializeComponent();

            string FileName = @"C:\\Users\\kryst\\Desktop\\sourceWhite.jpg";
            ImageProcessing.DetectLaserSpot(new Image<Bgr, byte>(FileName));
            labelMaximumValue.Text = ImageProcessing.MaximumValue.ToString();
            labelDvThreshold.Text = ImageProcessing.DvThreshold.ToString();
            labelPosition.Text = ImageProcessing.LaserSpotPosition.ToString();
            FileName = @"C:\\Users\\kryst\\Desktop\\sourceBlack.jpg";
            ImageProcessing.DetectLaserSpot(new Image<Bgr, byte>(FileName));
            labelMaximumValue.Text = ImageProcessing.MaximumValue.ToString();
            labelDvThreshold.Text = ImageProcessing.DvThreshold.ToString();
            labelPosition.Text = ImageProcessing.LaserSpotPosition.ToString();
        }
        #endregion

        #region Private Functions
        #endregion
    }
}
