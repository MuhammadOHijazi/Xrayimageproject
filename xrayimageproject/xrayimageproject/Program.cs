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
            Application.Run(new Form1());
            //DateTime searchDate = new DateTime(2024, 5, 13);
            //DateSearcher searcher = new DateSearcher(searchDate);
            //searcher.search();
        }
    }
}