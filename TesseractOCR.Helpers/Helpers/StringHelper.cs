
namespace TesseractOCR.Helpers
{
    public static class TextHelper
    {

        public static int GetWordCount(string text)
        {
            var splitChars = new List<string>()
            {
                " ",
                "?",
                ",",
                "!",
                ":",
                ";",
                ".",
            };
            string[] splitters = splitChars.ToArray();

            //      var exampleText2 = @"Every person dreams of lots of things in their life. These dreams are usually about richness and healthness. I consider them as important concepts but there is a forgotten one: Peace. I myself always dreamed of peace since my childhood. Because all dreams are actually originated from it.
            //I believe that any other dream cannot live by itself unless there is peace.Imagine there is a hostile situation going on around a person’s environment, would there be any chance to dream of anything other than peace? I guess not. That’s why i dreamed of it and still dreaming of it all the time. If there was not any war and fight in the World and people knew to share resources fairly among them, there would be much more to dream.
            //To sum up, if the ultimate dream of mine called “Peace” became real, it would be a true dream to dream other things than peace.";
            //      var exampleText = "Hello merdo! naber,fenasın! heheyt?selam";
            string[] result = text.Trim().ReplaceLineEndings(" ").Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            return result.Length;
        }
    }
}
