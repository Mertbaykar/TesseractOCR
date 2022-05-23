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
using TesseractOCR.Helpers;

namespace TesseractOCR.Core
{
    public static class PDFHelper
    {
        #region Save PDF To Server

        public static PdfDocument SavePDFByImageText(string dataPath, MemoryStream imageStream, string savePath, string? PDFtitle = null)
        {
            try
            {

                //string text = "";
                List<string> textParagraphs = new List<string>();

                // Extract text from single image
                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Image image = Image.LoadFromMemory(imageStream))
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
        #endregion

        #region Save PDF To Stream
        public static PdfDocument SavePDFByImageText(string dataPath, MemoryStream imageStream, Stream streamToSave, bool closeStream, string? PDFtitle = null)
        {
            try
            {

                //string text = "";
                List<string> textParagraphs = new List<string>();

                // Extract text from single image
                using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                {
                    using (Image image = Image.LoadFromMemory(imageStream))
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
                renderer.Save(streamToSave, closeStream);
                //pdfDocument.Save(savePath);
                return renderer.PdfDocument;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }

        public static PdfDocument SavePDFByImageText(string dataPath, string imagePath, Stream streamToSave, bool closeStream, string? PDFtitle = null)
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
                renderer.Save(streamToSave, closeStream);
                //pdfDocument.Save(savePath);
                return renderer.PdfDocument;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }
        #endregion


        #region Save PDF To Server From Multiple Files
        public static PdfDocument SavePDFByFiles(string dataPath, List<Stream> fileStreams, string savePath, string? PDFtitle = null)
        {
            try
            {
                //PdfReader.TestPdfFile(dataPath);

                // Image text adding

                List<string> textParagraphs = new List<string>();



                foreach (Stream stream in fileStreams)
                {
                    // Bu aşamada dosyanın gerçekten resim dosyası olup olmadığını da kontrol edeceğiz.
                    if (PdfReader.TestPdfFile(stream) == 0)
                    {
                        using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            stream.CopyTo(memoryStream);

                            using (Image image = Image.LoadFromMemory(memoryStream))
                            {
                                // Done with memoryStream
                                memoryStream.Dispose();

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
                    }
                    // PDF ise
                    else
                    {
                        string pdfText = PdfTextExtractor.GetText(stream);
                    }
                }
                // Extract text from single image


                #region PDF Handling and Saving

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                Document document = new Document();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                DefinePageSetup(document);
                DefineStyle(document);

                // Extend string just to see how it look on pages
                //var bla = textParagraphs;
                //for (int i = 0; i < 100; i++)
                //{
                //    textParagraphs.Add(bla[i]);
                //}

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
                return renderer.PdfDocument;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu");
            }

        }

        #endregion

        #region Init PDF Styles

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
        #endregion

    }
}