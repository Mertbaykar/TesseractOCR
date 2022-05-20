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

                //string text = "";
                List<string> textParagraphs = new List<string>();

                // Extract text from single image
                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Image image = Image.LoadFromFile(imagePath))
                    {
                        using (Page imagePage = engine.Process(image))
                        {
                            using (Blocks blocks = imagePage.Layout)
                            {
                                foreach (Block block in blocks)
                                {
                                    foreach (Layout.Paragraph para in block.Paragraphs)
                                    {
                                        string paragraphToAdd = "";

                                        foreach (TextLine textLine in para.TextLines)
                                        {
                                            string text = textLine.Text.ReplaceLineEndings(" ");
                                            paragraphToAdd += text;
                                        }
                                        textParagraphs.Add(paragraphToAdd);
                                    }
                                }
                            }
                        }
                    }
                }
               

                #region PDF Handling and Saving

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                Document document = new Document();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                DefinePageSetup(document);
                DefineStyle(document);

                // Extend string just to see how it look on pages
                var bla = textParagraphs;
                for (int i = 0; i < 100; i++)
                {
                    textParagraphs.Add(bla[i]);
                }

                Section section = document.AddSection();
                foreach (string paragraphs in textParagraphs)
                {
                    MigraDoc.DocumentObjectModel.Paragraph para = section.AddParagraph(paragraphs);
                }

                if (!string.IsNullOrWhiteSpace(PDFtitle))
                    document.Info.Title = PDFtitle;

                renderer.Document = document;
                renderer.RenderDocument();
                renderer.Save(savePath);
                //pdfDocument.Save(savePath);
                return renderer.PdfDocument;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }

        public static PdfPage SetupPdfPage(this PdfPage pdfPage)
        {
            pdfPage.Size = PageSize.A4;
            pdfPage.Orientation = PageOrientation.Portrait;
            pdfPage.TrimMargins.Left = new XUnit(30);
            pdfPage.TrimMargins.Right = new XUnit(30);
            pdfPage.TrimMargins.Top = new XUnit(30);
            pdfPage.TrimMargins.Bottom = new XUnit(30);
            return pdfPage;
        }

        public static void DefineStyle(Document document)
        {
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.SpaceAfter = 4;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.FirstLineIndent = 12;
            style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
            style.ParagraphFormat.LineSpacing = 13;
            style.ParagraphFormat.KeepTogether = true;
            //style.ParagraphFormat.ListInfo.ListType = ListType.NumberList1;
            //style.ParagraphFormat.ListInfo.NumberPosition = 3;
        }

        public static void DefinePageSetup(Document document)
        {
            document.DefaultPageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;
            document.DefaultPageSetup.PageFormat = PageFormat.A4;
            document.DefaultPageSetup.TopMargin = Unit.FromPoint(30);
            document.DefaultPageSetup.BottomMargin = Unit.FromPoint(30);
            document.DefaultPageSetup.LeftMargin = Unit.FromPoint(30);
            document.DefaultPageSetup.RightMargin = Unit.FromPoint(30);
        }

        //public static List<string> GetStringForPDFPages(string text, XFont font, XSize size,System.Drawing.StringFormat stringFormat, out int charactersFitted, out int linesFilled)
        //{
        //    var graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
        //    graphics.MeasureString(text, new System.Drawing.Font(font.FontFamily.Name,float(font.Size)));

        //}
    }
}