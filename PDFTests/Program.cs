using System.Text;
using TesseractOCR.Core;

namespace Tesseract.ConsoleDemo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = "phototest.tif";
            string pdfFileName = "test.pdf";

            string imagePath = Path.Combine(currentProjectDirectory, fileName);
            string savePath = Path.Combine(currentProjectDirectory, pdfFileName);
            string dataPath = Directory.GetParent(workingDirectory).Root.EnumerateDirectories("tessdata").FirstOrDefault().FullName;

           PDFHelper.SavePDFByImageText(dataPath,imagePath,savePath);

        }
    }
}

//https://www.codeproject.com/Questions/1089974/How-to-save-image-into-folder-or-directory