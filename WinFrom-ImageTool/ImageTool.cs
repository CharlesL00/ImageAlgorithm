using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
namespace WinFrom
{
    class ImageTool
    {
        private int[] ColorSet = new int[3] { 0, 0, 0 };

        /// <summary>
        /// 對比拉伸，以影像最小與最大值進行拉伸
        /// </summary>
        /// <param name="SourceMat"> 原圖輸入 (灰) </param>
        /// <param name="_Dst"> 結果輸出 (灰) </param>
        /// <param name="Min"> 影像最小亮度 </param>
        /// <param name="Max"> 影像最大亮度 </param>
        public void Contrast_Enhance(Mat SourceMat, OutputArray _Dst, int Min, int Max)
        {
            using (Mat Src = new Mat())
            {
                SourceMat.CopyTo(Src);

                Mat Dst = _Dst.GetMat();
                unsafe
                {
                    double MaxSubMin = Max - Min;
                    for (int y = 0; y < Src.Rows; y++)
                    {
                        IntPtr Src_Ptr = Src.Ptr(y);
                        IntPtr Dst_Ptr = Dst.Ptr(y);
                        byte* Src_b = (byte*)Src_Ptr.ToPointer();
                        byte* Dst_b = (byte*)Dst_Ptr.ToPointer();

                        for (int x = 0; x < Src.Cols; x++)
                        {
                            if (Src_b[x] <= Min)
                                Dst_b[x] = 0;
                            else if (Src_b[x] >= Max)
                                Dst_b[x] = 255;
                            else
                            {
                                Dst_b[x] = (byte)(255.0 / MaxSubMin * Src_b[x] - 255.0 * Min / MaxSubMin);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 設定 OutColor 顏色
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void SetColor(int R, int G, int B)
        {
            ColorSet[0] = R;
            ColorSet[1] = G;
            ColorSet[2] = B;
        }

        /// <summary>
        /// Binary 結果影像著色
        /// </summary>
        /// <param name="Source"> 原圖(灰) </param>
        /// <param name="BinaryMat"> 欲著色結果影像(Binary) </param>
        /// <param name="_Dst"> 結果(Drawing 為 True: 彩, False: 灰) </param>
        /// <param name="Drawing"> 是否著色 </param>
        public void OutColor(Mat Source, Mat BinaryMat, OutputArray _Dst, bool Drawing)
        {
            Mat Dst = _Dst.GetMat();
            using (Mat Src = new Mat())
            {
                Source.CopyTo(Src);
                
                if (Drawing && Src.Channels() == 1)
                {
                    unsafe
                    {
                        using (Mat ColorMat = new Mat())
                        {
                            Cv2.CvtColor(Src, ColorMat, ColorConversionCodes.GRAY2BGR);
                            Mat[] MatSplit = Cv2.Split(ColorMat);

                            for (int H = 0; H < BinaryMat.Rows; H++)
                            {
                                IntPtr ptr = BinaryMat.Ptr(H);
                                IntPtr ptr_R = MatSplit[2].Ptr(H);
                                IntPtr ptr_G = MatSplit[1].Ptr(H);
                                IntPtr ptr_B = MatSplit[0].Ptr(H);
                                byte* p = (byte*)ptr.ToPointer();
                                byte* p_R = (byte*)ptr_R.ToPointer();
                                byte* p_G = (byte*)ptr_G.ToPointer();
                                byte* p_B = (byte*)ptr_B.ToPointer();
                                for (int W = 0; W < BinaryMat.Cols; W++)
                                {
                                    if (p[W] == 255)
                                    {
                                        p_R[W] = (byte)ColorSet[0];
                                        p_G[W] = (byte)ColorSet[1];
                                        p_B[W] = (byte)ColorSet[2];
                                    }
                                }
                            }
                            Cv2.Merge(MatSplit, ColorMat);
                            ColorMat.CopyTo(Dst);
                        }
                    }
                }
                else
                {
                    Src.CopyTo(Dst);
                }
            }
        }

        /// <summary>
        /// 計算值方圖分佈
        /// </summary>
        /// <param name="Source">  計算值方圖影像(灰) </param>
        /// <param name="Hist"> 值方圖 </param>
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

        /// <summary>
        /// 負片
        /// </summary>
        /// <param name="Source"> 原圖(灰) </param>
        /// <param name="_Dst"> 結果(灰) </param>
        public void Negative(Mat Source, OutputArray _Dst)
        {
            Mat Dst = _Dst.GetMat();

            using (Mat Src = new Mat())
            using (Mat WhiteMat = new Mat(Source.Size(), Source.Type(),new Scalar(255)))
            {
                Source.CopyTo(Src);
                Cv2.AddWeighted(WhiteMat, 1, Src, -1, 0, Dst);
            }
        }
    }
}
