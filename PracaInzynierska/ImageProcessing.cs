namespace PracaInzynierska
{
    #region Usings
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;
    using System;
    using System.Collections.Generic;
    #endregion
    public static class ImageProcessing
    {
        #region Private Properties
        /// <summary>
        /// Source image from camera
        /// </summary>
        private static Image<Bgr, byte> sourceImage;

        /// <summary>
        /// Image converted to HSV colormode
        /// </summary>
        private static Image<Hsv, byte> outputImage;

        /// <summary>
        /// File path + file name with extension
        /// </summary>
        private static string fileName;

        /// <summary>
        /// Maximum value of OutputImage Value Channel
        /// </summary>
        private static double maximumValue;

        /// <summary>
        /// Calculated DV Threshold of the maximumValue
        /// </summary>
        private static double dvThreshold;

        /// <summary>
        /// List of pixels with value larger than dvThreshold
        /// </summary>
        private static List<Pixel> selectedPixels;

        /// <summary>
        /// Width of laser circle on image
        /// </summary>
        private static int laserSpotWidth;

        /// <summary>
        /// Height of laser circle on image
        /// </summary>
        private static int laserSpotHeight;

        /// <summary>
        /// Coordinates of laser spot
        /// </summary>
        private static Pixel laserSpotPosition;
        #endregion

        #region Public Properties
        /// <summary>
        /// Public property to set or get sourceImage (RGB)
        /// </summary>
        public static Image<Bgr, byte> SourceImage
        {
            get
            {
                return sourceImage;
            }
            set
            {
                sourceImage = value;
            }
        }

        /// <summary>
        /// Public property to set or get outputImage (HSV)
        /// </summary>
        public static Image<Hsv, byte> OutputImage
        {
            get
            {
                return outputImage;
            }
            set
            {
                outputImage = value;
            }
        }

        /// <summary>
        /// Public property to set or get path to source file
        /// </summary>
        public static string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        /// <summary>
        /// Public property to set or get maximum value of outputImage value channel
        /// </summary>
        public static double MaximumValue
        {
            get
            {
                return maximumValue;
            }
            set
            {
                maximumValue = value;
            }
        }

        /// <summary>
        /// Public property to set or get DV Threshold
        /// </summary>
        public static double DvThreshold
        {
            get
            {
                return dvThreshold;
            }
            set
            {
                dvThreshold = value;
            }
        }

        /// <summary>
        /// Public property to set or get selectedPixels list
        /// </summary>
        public static List<Pixel> SelectedPixels
        {
            get
            {
                return selectedPixels;
            }
            set
            {
                selectedPixels = value;
            }
        }

        /// <summary>
        /// Public property to set or get laserSpotWidth
        /// </summary>
        public static int LaserSpotWidth
        {
            get
            {
                return laserSpotWidth;
            }
            set
            {
                laserSpotWidth = value;
            }
        }

        /// <summary>
        /// Public property to set or get laserSpotHeight
        /// </summary>
        public static int LaserSpotHeight
        {
            get
            {
                return laserSpotHeight;
            }
            set
            {
                laserSpotHeight = value;
            }
        }

        /// <summary>
        /// Public property to set or get laserSpotPosition
        /// </summary>
        public static Pixel LaserSpotPosition
        {
            get
            {
                return laserSpotPosition;
            }
            set
            {
                laserSpotPosition = value;
            }
        }

        #endregion

        #region Public Functions
        public static void DetectLaserSpot(Image<Bgr, byte> sourceImg)
        {
            SourceImage = sourceImg;
            OutputImage = ConvertImageToHsv(SourceImage);
            MaximumValue = GetMaximumValue(OutputImage);
            DvThreshold = CalculateDvThreshold(MaximumValue);
            SelectedPixels = GetAllPixels(OutputImage);
            CalculateLaserSpotSizeAndPosition();
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Convert RGB image to HSV
        /// </summary>
        /// <param name="imgInput"></param>
        /// <returns></returns>
        private static Image<Hsv, Byte> ConvertImageToHsv(Image<Bgr, byte> imgInput)
        {
            Image<Hsv, byte> imgOutput = new Image<Hsv, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.CvtColor(imgInput, imgOutput, ColorConversion.Bgr2Hsv);
            return imgOutput;
        }

        /// <summary>
        /// Returns maximum value of value channel
        /// </summary>
        /// <param name="imgInput"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculate maximum DV Threshold - 95% of max value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double CalculateDvThreshold(double value)
        {
            int percent = 95;
            return value * percent / 100;
        }

        /// <summary>
        /// Returns all pixels that fit DV Threshold
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns position height and width of laser spot
        /// </summary>
        private static void CalculateLaserSpotSizeAndPosition()
        {
            int minX, maxX = 0, minY, maxY = 0;
            minX = selectedPixels[0].x;
            minY = selectedPixels[0].y;
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
        /// <summary>
        /// Structure with X, Y coordinates
        /// </summary>
        public struct Pixel
        {
            /// <summary>
            /// X coordinate of Pixel
            /// </summary>
            public int x;

            /// <summary>
            /// Y coordinate od Pixel
            /// </summary>
            public int y;

            /// <summary>
            /// Constructor with 2 arguments
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public Pixel(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            /// <summary>
            /// Returns string with X,Y position of pixel
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "X: " + x + " Y: " + y;
            }
        }
        #endregion
    }
}
