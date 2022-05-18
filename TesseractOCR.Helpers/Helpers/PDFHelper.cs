using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text;
//using System.Drawing.Imaging;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;


namespace TesseractOCR.Core
{
    public static class PDFHelper
    {
        // Creates a PDF from a single image file, datapath: tessdata folder path
        public static PdfDocument SavePDFByImageText(this Image image, string dataPath, string savePath, string? PDFtitle = null)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.PageLayout = PdfPageLayout.SinglePage;

                if (!string.IsNullOrWhiteSpace(PDFtitle))
                    pdfDocument.Info.Title = PDFtitle;

                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Page imagePage = engine.Process(image))
                    {
                        string text = imagePage.Text;
                        PdfPage pdfPage = pdfDocument.AddPage();
                        pdfPage.Size = PageSize.A4;
                        pdfPage.Orientation = PageOrientation.Portrait;
                        XGraphics gfx = XGraphics.FromPdfPage(pdfPage);
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                        XFont font = new XFont("Arial", 12, XFontStyle.Regular, options);
                        gfx.DrawString(text, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width, pdfPage.Height));
                        pdfDocument.Save(savePath);
                        return pdfDocument;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }

        public static PdfDocument SavePDFByImageText(string dataPath, string imagePath, string savePath, string? PDFtitle = null)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();

                if (!string.IsNullOrWhiteSpace(PDFtitle))
                    pdfDocument.Info.Title = PDFtitle;

                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Image image = Image.LoadFromFile(imagePath))
                    {
                        using (Page imagePage = engine.Process(image))
                        {
                            string text = imagePage.Text.ReplaceLineEndings(" ");
                            PdfPage pdfPage = pdfDocument.AddPage();
                            pdfPage.Size = PageSize.A4;
                            pdfPage.Orientation = PageOrientation.Portrait;
                            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                            XFont font = new XFont("Arial", 12, XFontStyle.Regular, options);
                            var size = gfx.MeasureString(text, font);
                            // DrawString'teki XRect için Width ve height set edilecek
                            // A4'ün boyutları üzerinden marginlere göre yazıya kalabilecek en fazla genişlik ve yükseklik bulunur
                            // XRect'in x ve y'sinin koordinatları marginlere göre belirlenecek (başlangıç noktası) ve her sayfada bu hesaba göre hareket edilecek.
                            pdfPage.TrimMargins.Left = new XUnit(30);
                            pdfPage.TrimMargins.Right = new XUnit(30);
                            pdfPage.TrimMargins.Top = new XUnit(50);
                            pdfPage.TrimMargins.Bottom = new XUnit(50);
                            // PageLayout : Birden fazla sayfa kullanılırsa değişebilir
                            pdfDocument.PageLayout = PdfPageLayout.SinglePage;

                            // Tüm pageler eklendikten sonra bu kısım çalışacak
                            gfx.DrawString(text, font, XBrushes.Black, new XRect(0, 0, size.Width, 0));
                            pdfDocument.Save(savePath);
                            return pdfDocument;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }
    }
}