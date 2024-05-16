using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrayimageproject
{
    internal class Evaluator
    {
        private Bitmap HealthyStateImage;
        private Bitmap CurrentStateImage;

        public Evaluator(string hsip, string csip) 
        {
            var images = Evaluator.MatchImagesSize (new Bitmap(hsip), new Bitmap(csip));
            this.HealthyStateImage = images[0];
            this.CurrentStateImage = images[1];
        }

        public string Evaluate()
        {
            double rmse = Evaluator.GetRMSE(this.HealthyStateImage, this.CurrentStateImage);
            if (rmse >= 0 && rmse < 25)
            {
                return "Healthy";
            }
            if (rmse >= 25 && rmse < 50)
            {
                return "Mild Case";
            }
            else if (rmse >= 50)
            {
                return "Serious Case";
            }

            return "Error";
        }

        private static double GetRMSE(Bitmap firstImage, Bitmap secondImage)
        {
            int width = firstImage.Width;
            int height = secondImage.Height;
            double error = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var firstImageColor = firstImage.GetPixel(j, i);
                    var secondImageColor = secondImage.GetPixel(j, i);
                    double firstImageGray = 0.2989 * firstImageColor.R + 0.5870 * firstImageColor.G + 0.1140 * firstImageColor.B;
                    double secondImageGray = 0.2989 * secondImageColor.R + 0.5870 * secondImageColor.G + 0.1140 * secondImageColor.B;
                    double squareDifference = Math.Pow((firstImageGray - secondImageGray), 2);
                    error += squareDifference;
                }
            }
            double meanError = error / (width * height);
            double rootMeanSquareError = Math.Sqrt(meanError);
            return rootMeanSquareError;
        }

        public static List<Bitmap> MatchImagesSize(Bitmap firstImage, Bitmap secondImage)
        {
            int firstImageWidth = firstImage.Width;
            int secondImageWidth = secondImage.Width;
            int firstImageHeight = firstImage.Height;
            int secondImageHeight = secondImage.Height;

            int minWidth = Math.Min(firstImageWidth, secondImageWidth);
            int minHeight = Math.Min(firstImageHeight, secondImageHeight);

            int x1 = (firstImageWidth - minWidth) / 2;
            int y1 = (firstImageHeight - minHeight) / 2;
            Rectangle cropRectangle1 = new Rectangle(x1, y1, minWidth, minHeight);

            int x2 = (secondImageWidth - minWidth) / 2;
            int y2 = (secondImageHeight - minHeight) / 2;
            Rectangle cropRectangle2 = new Rectangle(x2, y2, minWidth, minHeight);

            Bitmap firstImageCropped = new Bitmap(cropRectangle1.Width, cropRectangle1.Height);
            Bitmap secondImageCropped = new Bitmap(cropRectangle2.Width, cropRectangle2.Height);

            using (Graphics g = Graphics.FromImage(firstImageCropped))
            {
                g.DrawImage(firstImage, new Rectangle(0, 0, firstImageCropped.Width, firstImageCropped.Height), cropRectangle1, GraphicsUnit.Pixel);
            }

            using (Graphics g = Graphics.FromImage(secondImageCropped))
            {
                g.DrawImage(secondImage, new Rectangle(0, 0, secondImageCropped.Width, secondImageCropped.Height), cropRectangle2, GraphicsUnit.Pixel);
            }
            List<Bitmap> images = new List<Bitmap>();
            images.Add(firstImageCropped);
            images.Add(secondImageCropped);
            return images;
        }

    }
}
