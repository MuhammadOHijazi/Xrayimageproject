using System.Drawing.Imaging;

namespace xrayimageproject
{
    public class Compression
    {
        public static void CompressJpegImage(System.Drawing.Image image, string outputPath, long quality)
        {
            // Create an ImageCodecInfo object for the codec information
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object with the Quality parameter
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;

            // Save the compressed image
            image.Save(outputPath, jpgEncoder, myEncoderParameters);
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}