using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrayimageproject
{
    internal class CaptionOnImage
    {
        
        public static void SetCaption(Image image, string outputPath, String caption)
        {

            try
            {
                // Get Current Modifying Date
                String lastModifiedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Create a Graphics object
                using (Graphics graphics = Graphics.FromImage(image))
                {                    
                    Font font = new("Calibri", 40, FontStyle.Bold, GraphicsUnit.Pixel);

                    Brush captionBrush = new SolidBrush(Color.Gold);
                    Brush modifiedDateBrush = new SolidBrush(Color.Red);

                    // Set starting point for name
                    PointF captionPoint = new(10, 10);

                    // Calculate the size of the modified date text
                    SizeF textSize = graphics.MeasureString(lastModifiedText, font);
                    // Position the modified date text dynamically
                    PointF modifiedDatePoint = new(image.Width - textSize.Width - 10, image.Height - textSize.Height - 10);

                    // Draw the text
                    graphics.DrawString(caption, font, captionBrush, captionPoint);
                    graphics.DrawString(lastModifiedText, font, modifiedDateBrush, modifiedDatePoint);

                }

                // Save the modified image 
                image.Save(outputPath, ImageFormat.Png);

                Console.WriteLine("Text added to image successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }


    }
}
