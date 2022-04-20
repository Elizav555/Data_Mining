// See https://aka.ms/new-console-template for more information
using BloomFilter;

var text = "";
using (var reader = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/song.txt"))
    text = reader.ReadToEnd();
var separators = new char[] { ' ', '\n', ',', '.', '-', ':', ';', '?', '!', ')', '(', '\r' };
var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
words = words.Distinct().ToArray();
var bloomFilter = new BloomFilterClass(words.Length, 0.1);
foreach (var word in words)
{
    bloomFilter.Add(word);
}
foreach (var cell in bloomFilter.filter)
    Console.Write(cell);
Console.WriteLine();
Console.WriteLine(bloomFilter.FindString("улыбки"));
Console.WriteLine(bloomFilter.FindString("слону"));
Console.WriteLine(bloomFilter.FindString("облака"));
Console.WriteLine(bloomFilter.FindString("running"));
Console.WriteLine(bloomFilter.FindString("word"));
Console.WriteLine(bloomFilter.FindString("rrrrr"));