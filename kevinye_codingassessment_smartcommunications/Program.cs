using kevinye_codingassessment_smartcommunications;

HaikuParser haikuParser = new HaikuParser();
var inputTextPath = Path.Combine(AppContext.BaseDirectory, "Haiku.txt");
string inputText = System.IO.File.ReadAllText(inputTextPath);
string error;

bool isHaiku = haikuParser.IsHaiku(inputText, out error);

if (isHaiku)
{
    Console.WriteLine("Text is a haiku");
}
else
{
    Console.WriteLine($"Text is not a haiku for reason: {error}");
}
