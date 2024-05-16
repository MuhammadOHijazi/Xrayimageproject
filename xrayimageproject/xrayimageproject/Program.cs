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
            string firstPath = "C:\\Users\\number one\\Downloads\\03-copd-comparison2.jpg";
            string secondPath = "C:\\Users\\number one\\Downloads\\03-copd-comparison.jpg";
            Evaluator evaluator = new Evaluator(firstPath, secondPath);
            string evaluation = evaluator.Evaluate();
            Console.WriteLine(evaluation);
        }
    }
}