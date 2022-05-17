using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;
using TesseractOCR.Renderers;
using System.Drawing;
using System;

namespace Tesseract.ConsoleDemo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.CreateSubdirectory("Images");
            string fileName = "phototest.tif";
            string imagePath = Path.Combine(currentProjectDirectory, fileName);

            string tessData = Directory.GetParent(workingDirectory).Parent.Parent.EnumerateDirectories("tessdata").FirstOrDefault().FullName;
            string bla = Directory.GetParent(workingDirectory).Root.EnumerateDirectories("tessdata").FirstOrDefault().FullName;

            #region PDF Folder Handling

            DirectoryInfo[] presentPDFDirectory = null;
            DirectoryInfo createdPDFirectory = null;
            string PDFPathToSave = "";
            //var bla = Directory.GetParent(workingDirectory).Parent.Parent.GetDirectories();
            // Check if Images folder exists
            presentPDFDirectory = Directory.GetParent(workingDirectory).Parent.Parent.GetDirectories("PDFs");

            if (presentPDFDirectory.Length != 0)
            {
                // Bir Images klasörü olacağından bir kere foreach'te dönecek (eğer dönerse)
                foreach (var directory in (IEnumerable<dynamic>)presentPDFDirectory)
                {
                    PDFPathToSave += directory.FullName;
                }
            }

            else
            {
                // Create if Images folder does not exist
                createdPDFirectory = Directory.GetParent(workingDirectory).Parent.Parent.CreateSubdirectory("PDFs");
                PDFPathToSave = createdPDFirectory.FullName;
            }
            string PDFfileName = "test";
            string PDFfilePath = Path.Combine(PDFPathToSave, PDFfileName);
            Console.WriteLine(PDFfilePath);
            #endregion
            using (IResult renderer = Result.CreatePdfRenderer(PDFfilePath, tessData, false))
            {
                // PDF Title
                using (renderer.BeginDocument("SearchablePdfTest"))
                {
                    using (var engine = new Engine(tessData, Language.English, EngineMode.TesseractAndLstm))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            
                            using (Image img = Image.LoadFromFile(imagePath))
                            {
                                using (Page page = engine.Process(img, "SearchablePdfTest"))
                                {
                                    renderer.AddPage(page);
                                }
                            }
                        }
                       
                    }
                }
            }

        }
    }
}

