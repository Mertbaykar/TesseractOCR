//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.Content;
//using PdfSharp.Pdf.Content.Objects;
//using PdfSharp.Pdf.IO;


using TesseractOCR.Layout;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TesseractOCR.Helpers
{
    public static class PdfTextExtractor
    {

        public static string GetPDFTextByPages(Stream pdfStream)
        {
            string sectionTexts = string.Empty; 

            using (PdfDocument pdfDocument = PdfDocument.Open(pdfStream))
            {
                List<string> words = new List<string>();

                foreach (UglyToad.PdfPig.Content.Page page in pdfDocument.GetPages())
                {
                    foreach (var word in page.GetWords())
                    {
                        words.Add(word.Text);
                    }
                }
               sectionTexts = string.Join(" ", words);
            }
            return sectionTexts;
        }
    }
}
