using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRank
{
    internal class MatrixGenerator
    {
        public int[,] adjMatrix;
        public double[,] probabilityMatrix;
        Dictionary<string, List<string>> linksDict = new Dictionary<string, List<string>>();
        Dictionary<string, int> sitesIndexDict = new Dictionary<string, int>();
        Dictionary<int, List<int>> indexesDict = new Dictionary<int, List<int>>();
        public MatrixGenerator()
        {
            var N = ReadDictionary();
            using (var writer = new StreamWriter(@"D:\DataMining\task2\linksRes.txt"))
            {
                foreach (var link in sitesIndexDict.Keys)
                    writer.WriteLine(link);
            }
            ConvertToIndexes();
            adjMatrix = GenerateAdjacencyMatrix(N);
            probabilityMatrix = GenerateProbabilityMatrix(N);
        }

        private int ReadDictionary()
        {
            using (var reader = new StreamReader(@"D:\DataMining\task2\link-extractor\developer.android.com_links_dictt.txt"))
            {
                var text = reader.ReadToEnd();
                linksDict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(text);
            }
            int index = 0;
            foreach (var site in linksDict.Keys)
            {
                if (!sitesIndexDict.ContainsKey(site))
                {
                    sitesIndexDict.Add(site, index);
                    index++;
                }
            }
            foreach (var sites in linksDict.Values)
            {
                sites.Where(link => !sitesIndexDict.ContainsKey(link))
                .ToList()
                .ForEach(link => { sitesIndexDict.Add(link, index); index++; });
            }
            return index;
        }

        private void ConvertToIndexes()
        {
            foreach (var link in linksDict.Keys)
            {
                var index = sitesIndexDict[link];
                indexesDict.Add(index, new List<int>());
                foreach (var site in linksDict[link])
                {
                    indexesDict[index].Add(sitesIndexDict[site]);
                }
            }
        }

        private int[,] GenerateAdjacencyMatrix(int N)
        {
            var matrixRes = new int[N, N];
            for (int i = 0; i < linksDict.Keys.Count; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrixRes[i, j] = indexesDict[i].Contains(j) ? 1 : 0;
                }
            }
            return matrixRes;
        }

        private double[,] GenerateProbabilityMatrix(int N)
        {
            var matrixRes = new double[N, N];
            for (int i = 0; i < linksDict.Keys.Count; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    double isPresent = indexesDict[i].Contains(j) ? 1.0 : 0.0;
                    matrixRes[i, j] = isPresent /indexesDict[i].Count;
                }
            }
            return matrixRes;
        }

        public void WriteMatrixToTxt(double[,] matrix)
        {
            string filePath = @"D:\DataMining\task2\pageRankRes.txt";
            string delimiter = ",";
            var lines = matrix.GetLength(0);
            var col = matrix.GetLength(1);
            using (var streamWriter = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                {
                    var line = new double[col];
                    for (int j = 0; j < col; j++)
                    {
                        line[j] = matrix[i, j];
                    }
                    streamWriter.WriteLine(String.Join(delimiter, line));
                }
            }
        }
    }
}
