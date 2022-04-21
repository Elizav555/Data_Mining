using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Multihash;

const int supportLevel = 3;
var baskets = new Dictionary<long, List<string>>();
var count = new Dictionary<string, int>();
var doubletons = new List<Tuple<int, int>>();
var prodCodeToIndex = new Dictionary<string, int>();
var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
{
    HasHeaderRecord = true,
    DetectDelimiter = true,
};

using var streamReader = File.OpenText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/transactions.csv");
using var csvReader = new CsvReader(streamReader, csvConfig);

var records = csvReader.GetRecords<Transaction>();
foreach (var record in records)
{
    // form baskets from our data
    if (baskets.ContainsKey(record.BASKET_ID))
        baskets[record.BASKET_ID].Add(record.PROD_CODE);
    else baskets.Add(record.BASKET_ID, new List<string> { record.PROD_CODE });
    // count number of unique products
    if (count.ContainsKey(record.PROD_CODE))
        count[record.PROD_CODE]++;
    else count.Add(record.PROD_CODE, 1);
}
//form dictionary to map products codes to indexes for convenience
foreach (var item in count.Keys.Select((code, index) => new Tuple<string, int>(code, index)))
{
    prodCodeToIndex.Add(item.Item1, item.Item2);
}
//find products which count is less than support level
var productsToExclude = count.Where(prod => prod.Value < supportLevel).Select(prod => prodCodeToIndex[prod.Key]).ToList();
foreach (var basket in baskets.Values)
{
    //form doubletons for each basket
    var basketOfIndexes = basket.Select(code => prodCodeToIndex[code]);
    doubletons.AddRange(basketOfIndexes.SelectMany((item, index) => basketOfIndexes.Skip(index).Where(secondItem => item != secondItem)
    .Select(secondItem => new Tuple<int, int>(item, secondItem))));
}
var n = prodCodeToIndex.Keys.Count;
var firstHash = new HashFunc(1243, 12245, 20 * n);
var secondHash = new HashFunc(2324, 4636, 20 * n);
var fisrtBuckets = new Dictionary<int, List<Tuple<int, int>>>();
var secondBuckets = new Dictionary<int, List<Tuple<int, int>>>();
foreach (var doubleton in doubletons)
{
    //calculate two hashfunc at the same time
    var firstMod = firstHash.Hash(doubleton.Item1, doubleton.Item2);
    var secondMod = secondHash.Hash(doubleton.Item1, doubleton.Item2);
    //put doubletons into hash buckets
    if (fisrtBuckets.ContainsKey(firstMod))
        fisrtBuckets[firstMod].Add(doubleton);
    else fisrtBuckets.Add(firstMod, new List<Tuple<int, int>> { doubleton });
    if (secondBuckets.ContainsKey(secondMod))
        secondBuckets[secondMod].Add(doubleton);
    else secondBuckets.Add(secondMod, new List<Tuple<int, int>> { doubleton });
}
//now remove buckets where support is less
var doubletonsToExclude = new List<Tuple<int, int>>();
foreach (var fromFirst in fisrtBuckets.Values.Where(elems => elems.Count < supportLevel))
{
    doubletonsToExclude.AddRange(fromFirst);
}
foreach (var fromSecond in secondBuckets.Values.Where(elems => elems.Count < supportLevel))
{
    doubletonsToExclude.AddRange(fromSecond);
}
doubletons = doubletons.Except(doubletonsToExclude).ToList();
//now remove doubletons which contains non-frequent products
doubletons = doubletons.Where(doubleton => !productsToExclude.Contains(doubleton.Item1) && !productsToExclude.Contains(doubleton.Item2)).ToList();
//write result
using (var streamWriter = new StreamWriter(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/result.txt"))
{
    prodCodeToIndex.Values.ToList().Where(prod => !productsToExclude.Contains(prod)).ToList().ForEach(prod => streamWriter.Write(prod + " , "));
    streamWriter.WriteLine();
    doubletons.Distinct().ToList().ForEach(doubleton => streamWriter.Write(doubleton + " , "));
}