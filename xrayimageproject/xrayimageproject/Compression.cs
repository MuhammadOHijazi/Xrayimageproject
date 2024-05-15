using NAudio.Wave;
using System.Drawing.Imaging;
using NAudio.Lame;

namespace xrayimageproject
{
    public class AudioCompression
    {
        public static void CompressAudio(string inputPath, string outputPath, IAudioCompressor compressor)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("The input file does not exist.", inputPath);
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or whitespace.", nameof(outputPath));

            try
            {
                using (FileStream inputFileStream = File.OpenRead(inputPath))
                using (FileStream outputFileStream = File.Create(outputPath))
                {
                    compressor.Compress(inputFileStream, outputFileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("An error occurred during audio compression: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public interface IAudioCompressor
    {
        void Compress(Stream input, Stream output);
    }
    public class Mp3AudioCompressor : IAudioCompressor
    {
        private readonly int _bitRate;

        public Mp3AudioCompressor(int bitRate)
        {
            _bitRate = bitRate;
        }

        public void Compress(Stream input, Stream output)
        {
            using (var reader = new WaveFileReader(input))
            using (var writer = new LameMP3FileWriter(output, reader.WaveFormat, _bitRate))
            {
                reader.CopyTo(writer);
            }
        }
    }
    public class ImageCompression
    {
        public static void CompressJpegImage(System.Drawing.Image image, string outputPath, long quality)
        {
            try
            {
                // Validate input parameters
                if (image == null)
                    throw new ArgumentNullException(nameof(image));
                if (string.IsNullOrWhiteSpace(outputPath))
                    throw new ArgumentException("Output path cannot be null or whitespace.", nameof(outputPath));
                if (quality < 0 || quality > 100)
                    throw new ArgumentOutOfRangeException(nameof(quality), "Quality must be between 0 and 100.");

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
            catch (Exception ex)
            {
                // Log the exception details if necessary
                // Display a prompt to the user
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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