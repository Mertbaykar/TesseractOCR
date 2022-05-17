using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Pix;

namespace Tesseract.ConsoleDemo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.CreateSubdirectory("Images");
            string fileName = "phototest.tif";
            string imagePath = Path.Combine(currentProjectDirectory, fileName);

            string tessData = Directory.GetParent(workingDirectory).Parent.Parent.EnumerateDirectories("tessdata").FirstOrDefault().FullName;

            #region Images Folder Handling

            DirectoryInfo[] presentImageDirectory = null;
            DirectoryInfo createdImageDirectory = null;
            string imagePathToSave = "";
            //var bla = Directory.GetParent(workingDirectory).Parent.Parent.GetDirectories();
            // Check if Images folder exists
            presentImageDirectory = Directory.GetParent(workingDirectory).Parent.Parent.GetDirectories("Images");

            if (presentImageDirectory.Length != 0)
            {
                // Bir Images klasörü olacağından bir kere foreach'te dönecek (eğer dönerse)
                foreach (var directory in (IEnumerable<dynamic>)presentImageDirectory)
                {
                    imagePathToSave += directory.FullName;
                }
            }

            else
            {
                // Create if Images folder does not exist
                createdImageDirectory = Directory.GetParent(workingDirectory).Parent.Parent.CreateSubdirectory("Images");
                imagePathToSave = createdImageDirectory.FullName;
            }

            #endregion

            using (var engine = new Engine(tessData, Language.English, EngineMode.LstmOnly))
            {

                using (Image img = Image.LoadFromFile(imagePath))
                {


                    using (Page page = engine.Process(img))
                    {
                        Console.WriteLine("Mean confidence: {0}", page.MeanConfidence);
                        Console.WriteLine("Text: \r\n{0}", page.Text);
                        StringBuilder result = new StringBuilder();

                        foreach (var block in page.Layout)
                        {
                            result.AppendLine($"Block confidence: {block.Confidence}");
                            if (block.BoundingBox != null)
                            {
                                var boundingBox = block.BoundingBox.Value;
                                result.AppendLine($"Block bounding box X1 '{boundingBox.X1}', Y1 '{boundingBox.Y2}', X2 " +
                                                  $"'{boundingBox.X2}', Y2 '{boundingBox.Y2}', width '{boundingBox.Width}', height '{boundingBox.Height}'");
                            }
                            result.AppendLine($"Block text: {block.Text}");

                            foreach (var paragraph in block.Paragraphs)
                            {
                                result.AppendLine($"Paragraph confidence: {paragraph.Confidence}");
                                if (paragraph.BoundingBox != null)
                                {
                                    var boundingBox = paragraph.BoundingBox.Value;
                                    result.AppendLine($"Paragraph bounding box X1 '{boundingBox.X1}', Y1 '{boundingBox.Y2}', X2 " +
                                                      $"'{boundingBox.X2}', Y2 '{boundingBox.Y2}', width '{boundingBox.Width}', height '{boundingBox.Height}'");
                                }
                                var info = paragraph.Info;
                                result.AppendLine($"Paragraph info justification: {info.Justification}");
                                result.AppendLine($"Paragraph info is list item: {info.IsListItem}");
                                result.AppendLine($"Paragraph info is crown: {info.IsCrown}");
                                result.AppendLine($"Paragraph info first line ident: {info.FirstLineIdent}");
                                result.AppendLine($"Paragraph text: {paragraph.Text}");

                                int lineCount = 1;

                                foreach (var textLine in paragraph.TextLines)
                                {
                                    #region Save line images to Images Folder

                                    Image lineImage = textLine.Image.Item1;
                                    string fileNameToSave = "Line" + lineCount.ToString() + ".jpg";
                                    string lineImagePathToSave = Path.Combine(imagePathToSave, fileNameToSave);
                                    //imagePathToSave = Path.Combine(imagePathToSave, fileNameToSave);
                                    Console.WriteLine("Path of image:{0}", lineImagePathToSave);
                                    lineImage.Deskew().Save(lineImagePathToSave);
                                    lineCount++;
                                    #endregion

                                    if (textLine.BoundingBox != null)
                                    {
                                        var boundingBox = textLine.BoundingBox.Value;
                                        result.AppendLine($"Text line bounding box X1 '{boundingBox.X1}', Y1 '{boundingBox.Y2}', X2 " +
                                                          $"'{boundingBox.X2}', Y2 '{boundingBox.Y2}', width '{boundingBox.Width}', height '{boundingBox.Height}'");
                                    }
                                    result.AppendLine($"Text line confidence: {textLine.Confidence}");
                                    result.AppendLine($"Text line text: {textLine.Text}");


                                    foreach (var word in textLine.Words)
                                    {
                                        result.AppendLine($"Word confidence: {word.Confidence}");
                                        if (word.BoundingBox != null)
                                        {
                                            var boundingBox = word.BoundingBox.Value;
                                            result.AppendLine($"Word bounding box X1 '{boundingBox.X1}', Y1 '{boundingBox.Y2}', X2 " +
                                                              $"'{boundingBox.X2}', Y2 '{boundingBox.Y2}', width '{boundingBox.Width}', height '{boundingBox.Height}'");
                                        }
                                        result.AppendLine($"Word is from dictionary: {word.IsFromDictionary}");
                                        result.AppendLine($"Word is numeric: {word.IsNumeric}");
                                        result.AppendLine($"Word language: {word.Language}");
                                        result.AppendLine($"Word text: {word.Text}");

                                        foreach (var symbol in word.Symbols)
                                        {
                                            result.AppendLine($"Symbol confidence: {symbol.Confidence}");
                                            if (symbol.BoundingBox != null)
                                            {
                                                var boundingBox = symbol.BoundingBox.Value;
                                                result.AppendLine($"Symbol bounding box X1 '{boundingBox.X1}', Y1 '{boundingBox.Y2}', X2 " +
                                                                  $"'{boundingBox.X2}', Y2 '{boundingBox.Y2}', width '{boundingBox.Width}', height '{boundingBox.Height}'");
                                            }
                                            result.AppendLine($"Symbol is superscript: {symbol.IsSuperscript}");
                                            result.AppendLine($"Symbol is dropcap: {symbol.IsDropcap}");
                                            result.AppendLine($"Symbol text: {symbol.Text}");
                                        }
                                    }
                                }
                            }
                        }

                        Console.Write(result.ToString());
                    }
                }
            }


        }
    }
}

//https://www.codeproject.com/Questions/1089974/How-to-save-image-into-folder-or-directory