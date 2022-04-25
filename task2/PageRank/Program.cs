// See https://aka.ms/new-console-template for more information
using PageRank;

var generator = new MatrixGenerator();
var pageRank = new PageRank.PageRank(generator.probabilityMatrix);
var pageRankRes = pageRank.CalculatePageRank();
generator.WriteMatrixToTxt(pageRankRes);