using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
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

        public void convertRGBtoHSV(Image<Bgr, byte> imgInput)
        {
            Image<Hsv, byte> imgOutput = new Image<Hsv, byte>(imgInput.Width, imgInput.Height);
            Image<Bgr, byte> imgFinalOutput = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.CvtColor(imgInput, imgOutput, ColorConversion.Bgr2Hsv);
            imgFinalOutput.Data = imgOutput.Data;
        }
    }
}
