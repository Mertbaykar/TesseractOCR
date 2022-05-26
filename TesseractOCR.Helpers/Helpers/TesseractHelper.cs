//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Drawing.Layout;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.IO;
//using MigraDoc.Rendering;
using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;
//using MigraDoc.DocumentObjectModel;
using TesseractOCR.Layout;
//using PdfSharp.Pdf.Content;
//using PdfSharp.Pdf.Content.Objects;
//using PdfSharp.Pdf.Advanced;
//using Ghostscript.NET.Interpreter;
//using Ghostscript.NET.Processor;
//using Ghostscript.NET.Viewer;
using Ghostscript.NET.Rasterizer;
using Ghostscript.NET;
using Ghostscript.NET.Processor;

namespace TesseractOCR.Helpers.Helpers
{
    public static class TesseractHelper
    {
        public static List<string> GetPDFPageText(Stream pdfStream, string dataPath)
        {

            try
            {
                int dpi = 100;
                GhostscriptVersionInfo lastInstalledVersion =
               GhostscriptVersionInfo.GetLastInstalledVersion(
                       GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                       GhostscriptLicense.GPL);
                List<string> textParagraphs = new List<string>();

                using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.Open(pdfStream, lastInstalledVersion,false);

                    for (int i = 1; i <= rasterizer.PageCount; i++)
                    {
                        // pdf page as image
                        System.Drawing.Image pageImage = rasterizer.GetPage(dpi, i);

                        using (Engine engine = new Engine(dataPath, Language.English, EngineMode.TesseractAndLstm))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            //pdfStream.CopyTo(memoryStream);
                            pageImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

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
                }

                return textParagraphs;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.");
            }
            
        }
    }
}
