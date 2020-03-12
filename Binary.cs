using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
namespace WinFrom
{
    class Binary
    {
        private int neighbor = 0;
        private int adaptBlockSize = 0;
        private int adapt_c = 0;
        private int adaptiveTypes = 0;

        public int Neighbor { set { neighbor=value;} }
        public int AdaptBlockSize { set { adaptBlockSize = value; } }
        public int Adapt_c { set { adapt_c = value; } }
        public int AdaptiveTypes { set { adaptiveTypes = value; } }

        public enum OtsuType
        {
            Otsu = 0,
            VE = 1,      //ValleyEmphasis
            NVE = 2,
            WOV = 3,
            Adaptive = 4,
        }

        public int Otsu(Mat Source, OutputArray _Dst, OtsuType Type)
        {
            Mat Dst = _Dst.GetMat();
            int Threshold = 0;
            using (Mat Src = new Mat())
            {
                Source.CopyTo(Src);
                switch((int)Type)
                {
                    case 0:
                        Threshold = (int)Cv2.Threshold(Src, Dst, 0, 255, ThresholdTypes.Otsu);
                        break;
                    case 1:
                        Threshold = BaseOtsuCompute(Src, (int)Type);
                        break;
                    case 2:
                        Threshold = BaseOtsuCompute(Src, (int)Type);
                        break;
                    case 3:
                        Threshold = BaseOtsuCompute(Src, (int)Type);
                        break;
                    case 4:
                        if (adaptiveTypes == 0)
                            Cv2.AdaptiveThreshold(Src, Dst, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, adaptBlockSize, adapt_c);
                        else if (adaptiveTypes == 1)
                            Cv2.AdaptiveThreshold(Src, Dst, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, adaptBlockSize, adapt_c);

                        break;
                    default:
                        break;
                }
            }

            return Threshold;
        }

        public int BaseOtsuCompute(Mat Source,int Type)
        {
            int VE_Threshold = 0;

            using (Mat Src = new Mat())
            {
                Source.CopyTo(Src);

                int[] Hist = new int[256];
                float[] Hist_F = new float[256];
                float[] Vec = new float[256];

                GetHistogram_F(Src, ref Hist_F);

                float MN = Src.Cols * Src.Rows;
                float pi = 0, ui = 0, u = 0;
                List<float> Hist_Pi = new List<float>();
                List<float> p0 = new List<float>();
                List<float> p1 = new List<float>();
                List<float> u0 = new List<float>();
                List<float> u1 = new List<float>();
                for (int k = 0; k < 256; k++)
                {
                    Hist_Pi.Add(Hist_F[k] / MN);

                    pi += Hist_Pi[k];

                    p0.Add(pi);
                    p1.Add(1 - pi);
                }

                for (int k = 0; k < 256; k++)
                {
                    if (p0[k] != 0)
                    {
                        for (int L = 0; L < k + 1; L++)
                        {
                            ui += (float)L * Hist_Pi[L] / p0[k];
                        }
                    }
                    else
                        ui = 0;

                    u0.Add(ui);

                    if (p1[k] != 0)
                    {
                        for (int L = k + 1; L < 256; L++)
                        {
                            u += (float)L * Hist_Pi[L] / p1[k];
                        }
                    }
                    else
                        u = 0;
                    u1.Add(u);

                    u = 0;
                    ui = 0;
                }

                List<float> Delta = new List<float>();

                if (Type == 1)
                {
                    for (int k = 0; k < 256; k++)
                        Delta.Add((1 - Hist_Pi[k]) * (p0[k] * u0[k] * u0[k] + p1[k] * u1[k] * u1[k]));
                }
                else if (Type == 2)
                {
                    float[] NeighborValue = new float[256];
                    NeighborValue = NeighborValleyEmphasis(Hist_Pi.ToArray(), neighbor);
                    for (int k = 0; k < 256; k++)
                    {
                        if (NeighborValue[k] > 0)
                            Delta.Add((1 - NeighborValue[k]) * (p0[k] * u0[k] * u0[k] + p1[k] * u1[k] * u1[k]));
                        else
                            Delta.Add(0);
                    }
                }
                else
                {
                    for (int k = 0; k < 256; k++)
                        Delta.Add(p0[k] * p0[k] * u0[k] * u0[k] + p1[k] * u1[k] * u1[k]);
                }

                float Max = float.MinValue;
                for (int i = 0; i < 256; i++)
                {
                    if (Delta[i] > Max)
                    {
                        Max = Delta[i];
                        VE_Threshold = i;
                    }
                }
            }
            return VE_Threshold;
        }


        public float[] NeighborValleyEmphasis(float[] Hist_Float, int NeighborLen)
        {
            float[] NeighborValue = new float[256];

            for (int Index = 0; Index < 256; Index++)
            {
                int init = Index - NeighborLen;
                int end = Index + NeighborLen;
                if (init < 0)
                    init = 0;
                if (end > 255)
                    end = 255;
                for (int i = init; i <= end; i++)
                {
                    NeighborValue[Index] += Hist_Float[i];
                }
            }

            return NeighborValue;
        }

        public void GetHistogram_F(Mat Source, ref float[] Hist)
        {
            unsafe
            {
                using (Mat Src = new Mat())
                {
                    Source.CopyTo(Src);

                    float Total = Src.Cols * Src.Rows;
                    for (int H = 0; H < Src.Rows; H++)
                    {
                        IntPtr ptr = Src.Ptr(H);
                        byte* p = (byte*)ptr.ToPointer();
                        for (int W = 0; W < Src.Cols; W++)
                        {
                            Hist[p[W]]++;
                        }
                    }

                    //for (int i = 0; i < 256; i++)
                    //{
                    //    Hist[i] /= Total;
                    //}
                }
            }
        }

        public void GetHistogram(Mat Source, ref int[] Hist)
        {
            unsafe
            {
                using (Mat Src = new Mat())
                {
                    Source.CopyTo(Src);

                    for (int H = 0; H < Src.Rows; H++)
                    {
                        IntPtr ptr = Src.Ptr(H);
                        byte* p = (byte*)ptr.ToPointer();
                        for (int W = 0; W < Src.Cols; W++)
                        {
                            Hist[p[W]]++;
                        }
                    }
                }
            }
        }

    }
}
