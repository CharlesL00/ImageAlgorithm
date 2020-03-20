using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
namespace WinFrom
{
    class ObjectDetect
    {
        private int _MaxArea = int.MinValue;
        private int _MinArea = int.MaxValue;
        private int _ObjCount = 0;
        private int _RemoveObjCount = 0;

        private Mat _Label = new Mat();
        private Mat _Stats = new Mat();
        private Mat _Centroids = new Mat();

        public int MaxArea { get { return _MaxArea; } }
        public int MinArea { get { return _MinArea; } }
        public int ObjCount { get { return _ObjCount; } }
        public int RemoveObjCount { get { return _RemoveObjCount; } }

        public enum FindObjType
        {
            ConnectedComponents = 0,
            FindContours = 1,
        }

        public enum RemoveType
        {
            Inside = 0,
            UnderOver = 1,
        }

        public void FindObject(Mat Source, OutputArray _Dst, FindObjType Type)
        {
            Mat Dst = _Dst.GetMat();

            using (Mat Src = new Mat())
            {

                Source.CopyTo(Src);
                switch ((int)Type)
                {
                    case 0:
                        Run_ConnectedComponents(Src,Dst);
                        break;
                    case 1:
                        Run_FindContours(Src, Dst);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Run_ConnectedComponents(Mat Source, OutputArray _Dst)
        {
            Mat Dst = _Dst.GetMat();

            using (Mat Src = new Mat())
            {
                Source.CopyTo(Src);

                int nLabels = Cv2.ConnectedComponentsWithStats(Src, _Label, _Stats, _Centroids, PixelConnectivity.Connectivity8, MatType.CV_32SC1);

                unsafe
                {
                    _MaxArea = int.MinValue;
                    _MinArea = int.MaxValue;
                    _ObjCount = _Stats.Rows - 1;

                    for (int H = 1,ObjArea=0; H < _Stats.Rows; H++)
                    {
                        IntPtr ptr = _Stats.Ptr(H);
                        int* p = (int*)ptr.ToPointer();
                        ObjArea = p[4];
                        if (ObjArea > _MaxArea)
                            _MaxArea = ObjArea;
                        if (ObjArea < _MinArea)
                            _MinArea = ObjArea;
                    }
                }
            }
        }

        private void Run_FindContours(Mat Source, OutputArray _Dst)
        {

        }

        public bool ToRemove(int Under, int Over)
        {
            bool IsRemove = false;

            return IsRemove;
        }

        public void RemoveObject(OutputArray _Dst, int Under, int Over, RemoveType Type)
        {
            Mat Dst = _Dst.GetMat();
            unsafe
            {
                bool[] SaveLabel = new bool[_Stats.Rows];
                SaveLabel[0] = false;

                for (int H = 1, ObjArea = 0; H < _Stats.Rows; H++)
                {
                    IntPtr ptr = _Stats.Ptr(H);
                    int* p = (int*)ptr.ToPointer();
                    ObjArea = p[4];

                    switch ((int)Type)
                    {
                        case 0:
                            if (Under <= ObjArea && ObjArea <= Over)
                                SaveLabel[H] = true;
                            break;
                        case 1:
                            if (Under > ObjArea || ObjArea > Over)
                                SaveLabel[H] = true;
                            break;
                        default:
                            break;
                    }
                }
                for (int H = 0; H < _Label.Rows; H++)
                {
                    IntPtr ptr = _Label.Ptr(H);
                    IntPtr Dst_ptr = Dst.Ptr(H);
                    int* p = (int*)ptr.ToPointer();
                    byte* Dst_p = (byte*)Dst_ptr.ToPointer();
                    for (int W = 0; W < _Label.Cols; W++)
                    {
                        if (SaveLabel[p[W]])
                        {
                            Dst_p[W] = 255;
                        }
                        else
                        {
                            Dst_p[W] = 0;
                        }

                    }
                }

                foreach (bool IsSave in SaveLabel)
                {
                    if (!IsSave)
                        _RemoveObjCount++;
                }
            }
        }
        


    }
}
