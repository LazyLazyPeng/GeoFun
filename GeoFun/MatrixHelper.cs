using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class MatrixHelper
    {
        public static void Inv(ref double[] p, int i1)
        {
            double[] pq;
            double p1, pc;
            int i, j, k;
            pq = new double[i1];
            for (i = 0; i < i1; i++)
                pq[i] = p[i * i1 + i];
            for (i = 0; i < i1 - 1; i++)
            {
                for (j = i + 1; j < i1; j++)
                {
                    p1 = -p[j * i1 + i] / p[i * i1 + i];
                    for (k = 0; k < i1; k++)
                    {
                        if (k == i) continue;
                        p[j * i1 + k] = p[j * i1 + k] + p1 * p[i * i1 + k];
                    }
                    p[j * i1 + i] = p1;
                }
            }
            for (i = 0; i < i1; i++)
            {
                for (j = 0; j < i1; j++)
                {
                    if (j == i) continue;
                    p[i * i1 + j] = p[i * i1 + j] / p[i * i1 + i];
                }
                p[i * i1 + i] = 1 / p[i * i1 + i];
            }
            for (i = i1 - 1; i >= 1; i--)
                for (j = i - 1; j >= 0; j--)
                {
                    pc = -p[j * i1 + i];
                    for (k = 0; k <= j; k++)
                        p[j * i1 + k] = p[j * i1 + k] + pc * p[i * i1 + k];
                }
            for (i = 0; i < i1; i++)
                if (p[i * i1 + i] < 0)
                {
                    for (j = 0; j < i1; j++)
                        for (k = 0; k < i1; k++)
                            if (k == j)
                                p[j * i1 + j] = 1.0 / pq[j];
                            else
                                p[i * i1 + j] = 0.0;
                    return;
                }
            for (i = 0; i < i1 - 1; i++)
                for (j = i + 1; j < i1; j++)
                    p[i * i1 + j] = p[j * i1 + i];
            pq = null;
        }
 
    }
}
