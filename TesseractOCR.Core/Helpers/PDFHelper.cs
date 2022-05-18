using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;


namespace TesseractOCR.Core
{
    public static class PDFHelper
    {
        // Creates a PDF from a single image file, datapath: tessdata folder path
        public static void CreatePDFByImageText(this Image image, string dataPath, string? PDFtitle = null)
        {
            PdfDocument pdfDocument = new PdfDocument();

            using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
            {
                using (Page imagePage = engine.Process(image))
                {
                    string text = imagePage.Text;
                    PdfPage pdfPage = pdfDocument.AddPage();
                    pdfPage.Size = PageSize.A4;
                    pdfPage.Orientation = PageOrientation.Portrait;
                    XGraphics gfx = XGraphics.FromPdfPage(pdfPage);
                    XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                    XFont font = new XFont("Arial", 12, XFontStyle.Regular, options);
                    gfx.DrawString()
                }
            }
        }
    }
}