using System.Text;
using TesseractOCR.Core;
using TesseractOCR.Helpers.Helpers;

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
            string mergedFileName = "merge.pdf";

            string imagePath = Path.Combine(currentProjectDirectory, fileName);
            string pdfPath = Path.Combine(currentProjectDirectory, pdfFileName);
            string mergedFilePath = Path.Combine(currentProjectDirectory, mergedFileName);
            //string dataPath = Directory.GetParent(workingDirectory).Root.EnumerateDirectories("tessdata").FirstOrDefault().FullName;
            string dataPath = Directory.GetParent(workingDirectory).Root.EnumerateDirectories("tessdata").FirstOrDefault().FullName;

            using (StreamCollection streamCollection = new StreamCollection())
            {
                FileStream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
                streamCollection.Streams.Add(imageStream);
                streamCollection.Streams.Add(pdfStream);
                PDFHelper.SavePDFByFiles(dataPath, streamCollection.Streams,mergedFilePath);
            }
            //PDFHelper.SavePDFByImageText(dataPath, imagePath, savePath);

        }
    }
}

//https://www.codeproject.com/Questions/1089974/How-to-save-image-into-folder-or-directory