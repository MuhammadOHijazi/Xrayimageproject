using AForge.Imaging.Filters;
using AForge.Imaging;

namespace xrayimageproject
{
    public class HighAndLowPassFilter
    {
        public enum FilterType
        {
            High,
            Low
        }
        public static Bitmap ApplyFilter(System.Drawing.Image image, float cutoffFrequency, FilterType filterType)
        {
            // Load the image
            Bitmap sourceImage = new Bitmap(image);

            // Convert the image to grayscale the parameters are the standard coefficients
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = grayscaleFilter.Apply(sourceImage);

            // Apply FFT to the grayscale image
            ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
            complexImage.ForwardFourierTransform();

            // Create a high-pass filter mask
            int width = grayImage.Width;
            int height = grayImage.Height;
            float[,] hpfMask = CreateFilterMask(width, height, cutoffFrequency, filterType);

            // Apply the high-pass filter mask to the FFT image
            ApplyMaskToComplexImage(complexImage, hpfMask);

            // Perform an inverse Fourier transform
            complexImage.BackwardFourierTransform();

            // Convert the complex image back to bitmap
            Bitmap hpfImage = complexImage.ToBitmap();

            return hpfImage;
        }
        private static float[,] CreateFilterMask(int width, int height, float cutoffFrequency, FilterType filterType)
        {
            float[,] mask = new float[width, height];
            int cx = width / 2;
            int cy = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));
                    if (filterType == FilterType.High)
                    {
                        mask[x, y] = distance > cutoffFrequency ? 1 : 0;
                    }
                    else if (filterType == FilterType.Low)
                    {
                        mask[x, y] = distance <= cutoffFrequency ? 1 : 0;
                    }
                }
            }

            return mask;
        }
        private static void ApplyMaskToComplexImage(ComplexImage complexImage, float[,] mask)
        {
            int width = complexImage.Width;
            int height = complexImage.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    complexImage.Data[x, y].Re *= mask[x, y];
                    complexImage.Data[x, y].Im *= mask[x, y];
                }
            }
        }
    }
}