// See https://aka.ms/new-console-template for more information
using CBF;

var text = "";
//text = "День был свежий — свежестью травы, что тянулась вверх, облаков, что плыли в небесах, бабочек, что опускались на траву. День был соткан из тишины, но она вовсе не была немой, ее создавали пчелы и цветы, суша и океан, все, что двигалось, порхало, трепетало, вздымалось и падало, подчиняясь своему течению времени, своему неповторимому ритму. Край был недвижим, и все двигалось. Море было неспокойно, и море молчало. Парадокс, сплошной парадокс, безмолвие срасталось с безмолвием, звук со звуком. Цветы качались, и пчелы маленькими каскадами золотого дождя падали на клевер. Волны холмов и волны океана, два рода движения, были разделены железной дорогой, пустынной, сложенной из ржавчины и стальной сердцевины, дорогой, по которой, сразу видно, много лет не ходили поезда. На тридцать миль к северу она тянулась, петляя, потом терялась в мглистых далях; на тридцать миль к югу пронизывала острова летучих теней, которые на глазах смещались и меняли свои очертания на склонах далеких гор.";
using (var reader = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/text/Vacation.txt"))
    text = reader.ReadToEnd();
var separators = new char[] { ' ', '\n', ',', '.', '-', ':', ';', '?', '!',')','(' };
var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
words = words.Distinct().ToArray();
var cbf = new CBFilter(words.Length, 0.05);
foreach (var word in words)
{
    cbf.Add(word);
}
foreach (var cell in cbf.filter)
    Console.Write(cell);
Console.WriteLine();
Console.WriteLine(cbf.FindString("гор"));
Console.WriteLine(cbf.FindString("травы"));
Console.WriteLine(cbf.FindString("облаков"));
Console.WriteLine(cbf.FindString("running"));
Console.WriteLine(cbf.FindString("word"));
Console.WriteLine(cbf.FindString("rrrrr"));