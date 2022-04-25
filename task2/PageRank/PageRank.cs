using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRank
{
    internal class PageRank
    {
        const double betta = 0.85;
        private double[,] e;
        private double[,] secondMatrix = new double[,] { { betta }, { 0.15 } };
        private int n;
        private double[,] M;
        public PageRank(double[,] M)
        {
            this.M = M;
            n = M.GetLength(0);
            e = new double[1,n];
            for (int i = 0; i < n; i++)
            {
                e[0,i] = 1.0;
            }
        }

        public double[,] CalculatePageRank()
        {
            double[,] firstStep = Multiplication(e, M);
            var res = NextStep(firstStep, 0);
            return res;
        }
        private double[,] NextStep(double[,] prevV, int stepNumb)
        {
            var firstMatrix = new double[n,2]; 
            for(int i =0;i<n;i++)
            {
                firstMatrix[i, 0] = stepNumb!=0 ? prevV[i,0]: prevV[0, i];
                firstMatrix[i, 1] = 1/n;
            }
            var stepRes = Multiplication(firstMatrix, secondMatrix);
            if (stepNumb > 31)
                return stepRes;
            else return NextStep(stepRes, stepNumb + 1);
        }
        private double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            var r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        if (b[k, j] == 0 || b[k, j] is double.NaN)
                            continue;
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
    }
}
