using NAudio.Wave;
using System.Drawing.Imaging;
using NAudio.Lame;
using System.IO.Compression;

namespace xrayimageproject
{
    public class FileAndDirectoryCompression
    {
        // This function determines if the path is a directory or a file and compresses accordingly
        public static void Compress(string inputPath, string outputPath)
        {
            // Ensure the output file name ends with '.zip'
            if (!outputPath.EndsWith(".zip"))
            {
                outputPath += ".zip";
            }
            try
            {
                using (FileStream zipToOpen = new FileStream(outputPath, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        // Check if the input path is a directory or a file
                        if (Directory.Exists(inputPath))
                        {
                            // Call the extension method to add the directory to the archive
                            archive.CreateEntryFromDirectory(inputPath, Path.GetFileName(inputPath));
                        }
                        else if (File.Exists(inputPath))
                        {
                            // Call the extension method to add the file to the archive
                            archive.CreateEntryFromFile(inputPath, Path.GetFileName(inputPath), CompressionLevel.Optimal);
                        }
                        else
                        {
                            throw new FileNotFoundException($"The input path '{inputPath}' does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                // Display a prompt to the user
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public static class ZipArchiveExtensions
    {
        public static void CreateEntryFromDirectory(this ZipArchive archive, string sourceDirPath, string entryName = "")
        {
            // Add the directory
            if (!string.IsNullOrEmpty(entryName))
            {
                archive.CreateEntry(entryName + "/");
            }

            // Add the files from the directory
            DirectoryInfo di = new DirectoryInfo(sourceDirPath);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                string fileEntryName = string.IsNullOrEmpty(entryName) ? file.Name : entryName + "/" + file.Name;
                archive.CreateEntryFromFile(file.FullName, fileEntryName, CompressionLevel.Optimal);
            }

            // Recursively add subdirectories
            DirectoryInfo[] subDirectories = di.GetDirectories();
            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                string subDirEntryName = string.IsNullOrEmpty(entryName) ? subDirectory.Name : entryName + "/" + subDirectory.Name;
                CreateEntryFromDirectory(archive, subDirectory.FullName, subDirEntryName);
            }
        }
    }
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