using System;
using System.Windows.Forms;
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Image<Hsv, byte> ConvertRGBtoHSV(Image<Bgr, byte> imgInput)
        {
            Image<Hsv, byte> imgOutput = new Image<Hsv, byte>(imgInput.Width, imgInput.Height);
            Image<Bgr, byte> imgFinalOutput = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.CvtColor(imgInput, imgOutput, ColorConversion.Bgr2Hsv);
            imgFinalOutput.Data = imgOutput.Data;
            return imgOutput;
        }

        public void CameraCapture()
        {
            ImageViewer viewer = new ImageViewer();
            Capture capture = new Capture();
            Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
            {
                viewer.Image = capture.QueryFrame();
            });
            viewer.ShowDialog();
        }

        public int GetMaximumValue(Image<Hsv, byte> imgInput)
        {
            int maxValue = 0;
            
            return maxValue;
        }
    }
}
