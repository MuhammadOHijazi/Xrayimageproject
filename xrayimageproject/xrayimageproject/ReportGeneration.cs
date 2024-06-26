﻿using System;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.xmp.impl.xpath;
using System.Drawing.Imaging;

namespace xrayimageproject
{
    internal class ReportGeneration
    {
        public static void GenerateReport(string name, string id, string diagnosis, System.Drawing.Image image) {

           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1; // Default to PDF files
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save X-ray Report";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string outputPdfPath = saveFileDialog.FileName;

                // --- Generate the PDF and save it ---
                try
                {
                    using (FileStream stream = new FileStream(outputPdfPath, FileMode.Create))
                    {
                        // Create a new PDF document
                        Document document = new(PageSize.A4);
                        PdfWriter.GetInstance(document, stream);
                        document.Open();

                        // Add a title
                        Paragraph title = new("X-ray Report", FontFactory.GetFont("Calibri", 30, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);

                        document.Add(new Paragraph(" "));

                        //  --- Patient Name & ID Section ---
                        // 1. Create a table with 2 columns
                        PdfPTable table = new(2);
                        table.WidthPercentage = 100;

                        // 2. Set relative column widths (30% for the first column, 70% for the second)
                        float[] columnWidths = { 30f, 70f };
                        table.SetWidths(columnWidths);

                        // 2. Add patient name
                        PdfPCell cell = new(new Phrase("Patient Name:", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                        cell.BackgroundColor = new BaseColor(240, 240, 240);
                        table.AddCell(cell);
                        table.AddCell(new Phrase(name, FontFactory.GetFont("Calibri", 14)));

                        // 3. Add Patient ID date
                        cell = new(new Phrase("Patient ID:", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                        cell.BackgroundColor = new BaseColor(240, 240, 240);
                        table.AddCell(cell);
                        table.AddCell(new Phrase(id, FontFactory.GetFont("Calibri", 14)));

                        document.Add(table);

                        document.Add(new Paragraph(" "));

                        // --- Diagnosis Section ---
                        // 1. Add Label
                        Paragraph diagnosisLabel = new("Diagnosis:", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD));
                        document.Add(diagnosisLabel);

                        // 2. Add Content
                        Paragraph diagnosisContent = new(diagnosis, FontFactory.GetFont("Calibri", 14));
                        diagnosisContent.FirstLineIndent = 20f; // Indent the diagnosis text
                        document.Add(diagnosisContent);

                        document.Add(new Paragraph(" "));

                        // --- Image Section ---
                        // 1. Add Label
                        Paragraph imageLabel = new("X-ray Image:", FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD));
                        document.Add(imageLabel);

                        // 2. Add The Image
                        //if (File.Exists(imgPath))
                        //{
                        //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imgPath);

                        //}
                        iTextSharp.text.Image iimage = ConvertImageForItextSharp(image);
                        iimage.ScaleToFit(document.PageSize.Width - document.LeftMargin - document.RightMargin,
                                                    document.PageSize.Height - document.TopMargin - document.BottomMargin);
                        iimage.Alignment = Element.ALIGN_CENTER;
                        document.Add(iimage);

                        document.Close();
                    }
                    Console.WriteLine($"PDF Report generated at: {outputPdfPath}");
                    MessageBox.Show("X-ray report generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public static iTextSharp.text.Image ConvertImageForItextSharp(System.Drawing.Image systemDrawingImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                systemDrawingImage.Save(ms, ImageFormat.Png); // You can change the format if needed
                byte[] imageBytes = ms.ToArray();

                return iTextSharp.text.Image.GetInstance(imageBytes);
            }
        }
    }
}
