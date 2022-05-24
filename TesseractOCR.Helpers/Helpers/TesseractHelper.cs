using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using MigraDoc.Rendering;
using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;
using MigraDoc.DocumentObjectModel;
using TesseractOCR.Layout;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.Advanced;

namespace TesseractOCR.Helpers.Helpers
{
    public static class TesseractHelper
    {
        public static string GetPDFText(Stream pdfStream, string dataPath)
        {
            List<string> paragraphTexts = new List<string>();

           PdfDocument pdfDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.ReadOnly);

            foreach (PdfPage page in pdfDocument.Pages)
            {

            }
        }
    }
}
