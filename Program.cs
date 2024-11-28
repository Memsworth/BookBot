// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
var filePath = Path.Combine(solutionDirectory, "book.txt");

if (!File.Exists(filePath))
{
    throw new FileNotFoundException(filePath);
}

var fileText= await File.ReadAllLinesAsync(filePath);
var trackerDictionary = new Dictionary<char, int>();
var wordTracker = new Dictionary<string, int>();


foreach (var line in fileText)
{
    if(!string.IsNullOrEmpty(line))
    {
        var wordsInLine = line.Split(' ');

        foreach (var word in wordsInLine)
        {
            var wordWithNoSpecialChar = Regex.Replace(word, @"(\s+|@|&|\*|\(|\)|<|>|#|\""|,|\.|;|:)", string.Empty).ToLower();
      
            if(wordTracker.ContainsKey(wordWithNoSpecialChar))
            {
                wordTracker[wordWithNoSpecialChar]++;
            }
            else
            {
                wordTracker.Add(wordWithNoSpecialChar, 1);
            }
        }

        foreach (var item in line)
        {
            if (trackerDictionary.ContainsKey(item))
            {
                trackerDictionary[item]++;
            }
            else
            {
                trackerDictionary.Add(item, 1);
            }
        }
    }
}

foreach (var item in wordTracker.OrderByDescending(key => key.Value))
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}
Console.ReadKey();