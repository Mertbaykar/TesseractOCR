using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
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
        //public static PdfDocument SavePDFByImageText(this Image image, string dataPath, string savePath, string? PDFtitle = null)
        //{
        //    try
        //    {
        //        PdfDocument pdfDocument = new PdfDocument();
        //        pdfDocument.PageLayout = PdfPageLayout.SinglePage;

        //        if (!string.IsNullOrWhiteSpace(PDFtitle))
        //            pdfDocument.Info.Title = PDFtitle;

        //        using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
        //        {
        //            using (Page imagePage = engine.Process(image))
        //            {
        //                string text = imagePage.Text;
        //                PdfPage pdfPage = pdfDocument.AddPage();
        //                pdfPage.Size = PageSize.A4;
        //                pdfPage.Orientation = PageOrientation.Portrait;
        //                XGraphics gfx = XGraphics.FromPdfPage(pdfPage);
        //                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
        //                XFont font = new XFont("Arial", 12, XFontStyle.Regular, options);
        //                gfx.DrawString(text, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width, pdfPage.Height));
        //                pdfDocument.Save(savePath);
        //                return pdfDocument;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Bir hata oluştu");
        //    }

        //}

        public static PdfDocument SavePDFByImageText(string dataPath, string imagePath, string savePath, string? PDFtitle = null)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();

                if (!string.IsNullOrWhiteSpace(PDFtitle))
                    pdfDocument.Info.Title = PDFtitle;

                string text = "";

                // Extract text from single image
                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Image image = Image.LoadFromFile(imagePath))
                    {
                        using (Page imagePage = engine.Process(image))
                        {
                            text = imagePage.Text.ReplaceLineEndings(" ");
                        }
                    }
                }
                var textCopy = text;
                for (int i = 0; i < 100; i++)
                {
                    text += textCopy;
                }

                #region PDF Handling and Saving
                // Initial Page
                PdfPage pdfPage = pdfDocument.AddPage();
                //PdfPage pdfPage = new PdfPage();
                pdfPage.Size = PageSize.A4;
                pdfPage.Orientation = PageOrientation.Portrait;
                XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
                XFont font = new XFont("Arial", 12, XFontStyle.Regular, options);

                // Yazı için gereken alanı hesaplamak için gerekli width ve height bulunuyor.
                var sizeOfText = gfx.MeasureString(text, font);
                // DrawString'teki XRect için Width ve height set edilecek
                // A4'ün boyutları üzerinden marginlere göre yazıya kalabilecek en fazla genişlik ve yükseklik bulunur
                // XRect'in x ve y'sinin koordinatları marginlere göre belirlenecek (başlangıç noktası) ve her sayfada bu hesaba göre hareket edilecek.
                pdfPage.TrimMargins.Left = new XUnit(30);
                pdfPage.TrimMargins.Right = new XUnit(30);
                pdfPage.TrimMargins.Top = new XUnit(30);
                pdfPage.TrimMargins.Bottom = new XUnit(30);

                double textWidthInPage = pdfPage.Width - (pdfPage.TrimMargins.Left + pdfPage.TrimMargins.Right);
                double textHeightInPage = pdfPage.Height - (pdfPage.TrimMargins.Top + pdfPage.TrimMargins.Bottom);

                double areaOfText = sizeOfText.Width * sizeOfText.Height;
                double reservedTextAreaPerPage = textWidthInPage * textHeightInPage;
                // -1 comes from initial page
                var pageCount = Convert.ToInt32(Math.Round(areaOfText / reservedTextAreaPerPage, MidpointRounding.ToPositiveInfinity)) - 1;
                pdfDocument.PageLayout = PdfPageLayout.OneColumn;

                
                //double textTotalHeight = areaOfText / textWidthInPage;
                XTextFormatter formatter = new XTextFormatter(gfx);
                // Same layoutRectangle for each page
                XRect layoutRectangle = new XRect(new XSize(pdfPage.Width, pdfPage.Height));
                // Tüm pageler eklendikten sonra bu kısım çalışacak, bu kısım birden çok kere çalışacak
                formatter.DrawString(text, font, XBrushes.Black, layoutRectangle);

                if (pageCount > 0)
                {
                    // TODO: Text should be divided to pages based on it's area, and should be found the next part to write in next page
                    // IMPORTANT: How to divide text based on layoutrectangle area should be found
                    // Maybe check the point in each line and when page height equals to line's height, go next page and get the rest of other string per page

                    for (int i = 0; i < pageCount; i++)
                    {
                        PdfPage nextPage = (PdfPage)pdfPage.Clone();
                        pdfDocument.AddPage(nextPage);
                        // Buradaki text ayarlanacak (sayfa bazında)
                        formatter.DrawString(text, font, XBrushes.Black, layoutRectangle);
                    }
                }

                
                pdfDocument.Save(savePath);
                return pdfDocument;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }

        public static PdfPage ConfigurePdfPage(this PdfPage pdfPage)
        {
            pdfPage.Size = PageSize.A4;
            pdfPage.Orientation = PageOrientation.Portrait;

            pdfPage.TrimMargins.Left = new XUnit(30);
            pdfPage.TrimMargins.Right = new XUnit(30);
            pdfPage.TrimMargins.Top = new XUnit(30);
            pdfPage.TrimMargins.Bottom = new XUnit(30);
            return pdfPage;
        }
    }
}