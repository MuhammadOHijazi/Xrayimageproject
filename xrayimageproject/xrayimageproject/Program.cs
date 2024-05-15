namespace xrayimageproject
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //String inputPath = "C:\\Users\\HP\\Documents\\Years To Go\\4th Year\\2nd Semester\\Multimedia\\P\\project\\D Code\\xrayimageproject\\xrayimageproject\\assets\\inputs\\xray1.jpeg";
            //string outputpath = "c:\\users\\hp\\documents\\years to go\\4th year\\2nd semester\\multimedia\\p\\project\\d code\\xrayimageproject\\xrayimageproject\\assets\\outputs\\xray1_with_caption.jpeg";

            //// Add caption on the image
            //CaptionOnImage coi = new();
            //CaptionOnImage.SetCaption(inputPath, outputpath, "patient name");

            //// cCreating the report end exporting as pdf
            //string name = "Abu Yani";
            //string id = "1001";
            //string info = "Has a medium tumor";
            //string imgPath = outputpath;
            //string pdfOutputPath = "C:\\Users\\HP\\Documents\\Years To Go\\4th Year\\2nd Semester\\Multimedia\\P\\project\\D Code\\xrayimageproject\\xrayimageproject\\assets\\outputs\\patient_report.pdf";

            //ReportGeneration repoGene = new();
            //repoGene.GenerateReport(name, id, info, imgPath, pdfOutputPath);


            Application.Run(new Form1());
        }
    }
}