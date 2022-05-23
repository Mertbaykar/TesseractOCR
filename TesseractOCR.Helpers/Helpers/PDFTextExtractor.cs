﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.Content;
//using PdfSharp.Pdf.Content.Objects;
//using PdfSharp.Pdf.IO;


using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TesseractOCR.Helpers
{
    public static class PdfTextExtractor
    {

        public static void GetPageText(Stream pdfStream)
        {
            using (PdfDocument pdfDocument = PdfDocument.Open(pdfStream))
            {
                foreach (Page page in pdfDocument.GetPages())
                {
                    string pageText = page.Layout

                    foreach (Word word in page.GetWords())
                    {
                        Console.WriteLine(word.Text);
                    }
                }
            }
        }



        //public static string GetText(string pdfFileName)
        //{
        //    using (var _document = PdfReader.Open(pdfFileName, PdfDocumentOpenMode.ReadOnly))
        //    {
        //        var result = new StringBuilder();
        //        foreach (var page in _document.Pages)
        //        {
        //            ExtractText(ContentReader.ReadContent(page), result);
        //            result.AppendLine();
        //        }
        //        return result.ToString();
        //    }
        //}

        //public static string GetText(Stream pdfStream)
        //{
        //    using (var _document = PdfReader.Open(pdfStream, PdfDocumentOpenMode.ReadOnly))
        //    {
        //        var result = new StringBuilder();
        //        foreach (var page in _document.Pages)
        //        {
        //            ExtractText(ContentReader.ReadContent(page), result);
        //            result.AppendLine();
        //        }
        //        return result.ToString();
        //    }
        //}

        //#region CObject Visitor
        //private static void ExtractText(CObject obj, StringBuilder target)
        //{
        //    if (obj is CArray)
        //        ExtractText((CArray)obj, target);
        //    else if (obj is CComment)
        //        ExtractText((CComment)obj, target);
        //    else if (obj is CInteger)
        //        ExtractText((CInteger)obj, target);
        //    else if (obj is CName)
        //        ExtractText((CName)obj, target);
        //    else if (obj is CNumber)
        //        ExtractText((CNumber)obj, target);
        //    else if (obj is COperator)
        //        ExtractText((COperator)obj, target);
        //    else if (obj is CReal)
        //        ExtractText((CReal)obj, target);
        //    else if (obj is CSequence)
        //        ExtractText((CSequence)obj, target);
        //    else if (obj is CString)
        //        ExtractText((CString)obj, target);
        //    else
        //        throw new NotImplementedException(obj.GetType().AssemblyQualifiedName);
        //}

        //private static void ExtractText(CArray obj, StringBuilder target)
        //{
        //    foreach (var element in obj)
        //    {
        //        ExtractText(element, target);
        //    }
        //}

        //private static void ExtractText(COperator obj, StringBuilder target)
        //{
        //    if (obj.OpCode.OpCodeName == OpCodeName.Tj || obj.OpCode.OpCodeName == OpCodeName.TJ)
        //    {
        //        foreach (var element in obj.Operands)
        //        {
        //            ExtractText(element, target);
        //        }
        //        target.Append(" ");
        //    }
        //}
        //private static void ExtractText(CSequence obj, StringBuilder target)
        //{
        //    foreach (var element in obj)
        //    {
        //        ExtractText(element, target);
        //    }
        //}
        //private static void ExtractText(CString obj, StringBuilder target)
        //{
        //    target.Append(obj.Value);
        //}
        //#endregion
    }
}
