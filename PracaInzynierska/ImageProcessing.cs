namespace PrzetwarzanieObrazow
{
    #region Usings
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;
    using Emgu.CV.UI;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    #endregion
    public static class ImageProcessing
    {
        #region Fields
        private static Image<Bgr, byte> sourceImage;
        private static Image<Hsv, byte> outputImage;
        private static string fileName;
        private static double maximumValue;
        private static double dvThreshold;
        private static List<Pixel> selectedPixels;
        private static int laserSpotWidth;
        private static int laserSpotHeight;
        private static Pixel laserSpotPosition;
        #endregion

        #region Public Properties
        public static Image<Bgr, byte> SourceImage { get => sourceImage; set => sourceImage = value; }
        public static Image<Hsv, byte> OutputImage { get => outputImage; set => outputImage = value; }
        public static string FileName { get => fileName; set => fileName = value; }
        public static double MaximumValue { get => maximumValue; set => maximumValue = value; }
        public static double DvThreshold { get => dvThreshold; set => dvThreshold = value; }
        public static List<Pixel> SelectedPixels { get => selectedPixels; set => selectedPixels = value; }
        public static int LaserSpotWidth { get => laserSpotWidth; set => laserSpotWidth = value; }
        public static int LaserSpotHeight { get => laserSpotHeight; set => laserSpotHeight = value; }
        public static Pixel LaserSpotPosition { get => laserSpotPosition; set => laserSpotPosition = value; }
        #endregion

        #region Public Methods and Operators
        public static void DetectLaserSpot(Image<Bgr, byte> sourceImg)
        {
            SourceImage = sourceImg;
            OutputImage = ConvertImageToHsv(SourceImage);
            MaximumValue = GetMaximumValue(OutputImage);
            DvThreshold = CalculateDvThreshold(MaximumValue);
            SelectedPixels = GetAllPixels(OutputImage);
            CalculateLaserSpotSizeAndPosition();
        }

        public static void CameraCapture()
        {
            VideoCapture capture = new VideoCapture();
            var Image = capture.QueryFrame();
            Image.Save("capture.jpg");
        }
        #endregion

        #region Methods
        private static Image<Hsv, Byte> ConvertImageToHsv(Image<Bgr, byte> imgInput)
        {
            Image<Hsv, byte> imgOutput = new Image<Hsv, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.CvtColor(imgInput, imgOutput, ColorConversion.Bgr2Hsv);
            return imgOutput;
        }

        private static double GetMaximumValue(Image<Hsv, byte> imgInput)
        {
            double maximumValue = 0.00;
            for (int i = 0; i < imgInput.Height; i++)
            {
                for (int j = 0; j < imgInput.Width; j++)
                {
                    if (maximumValue < imgInput.Data[i, j, 2])
                        maximumValue = imgInput.Data[i, j, 2];                    
                }
            }
            return maximumValue;
        }

        private static double CalculateDvThreshold(double value)
        {
            int percent = 95;
            return value * percent / 100;
        }

        private static List<Pixel> GetAllPixels(Image<Hsv, byte> inputImage)
        {
            List<Pixel> pixels = new List<Pixel>();
            for (int i = 0; i < inputImage.Height; i++)
            {
                for (int j = 0; j < inputImage.Width; j++)
                {
                    if (inputImage.Data[i, j, 2] > DvThreshold)
                    {
                        pixels.Add(new Pixel(j, i));
                    }
                }
            }
            return pixels;
        }

        private static void CalculateLaserSpotSizeAndPosition()
        {
            int minX, maxX = 0, minY, maxY = 0;
            minX = SelectedPixels[0].x;
            minY = SelectedPixels[0].y;
            foreach (Pixel p in SelectedPixels)
            {
                if (p.x > maxX)
                    maxX = p.x;
                else if (p.x < minX)
                    minX = p.x;

                if (p.y > maxY)
                    maxY = p.y;
                else if (p.y < minY)
                    minY = p.y;
            }
            LaserSpotHeight = maxY - minY;
            LaserSpotWidth = maxX - minX;
            LaserSpotPosition = new Pixel(maxX - LaserSpotWidth / 2, maxY - LaserSpotHeight / 2);
        }
        #endregion

        #region Pixels
        public struct Pixel
        {
            public int x;
            public int y;

            public Pixel(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return "X: " + x + " Y: " + y;
            }
        }
        #endregion
    }
}
