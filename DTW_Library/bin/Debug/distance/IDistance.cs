using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DTW
{
    /// <summary>
    /// 距離関数の種類
    /// </summary>
    public enum DistanceFunction
    {
        /// <summary>
        /// 数値の距離関数
        /// </summary>
        Numerical,
        /// <summary>
        /// 文字列の距離関数
        /// </summary>
        TextString,
        /// <summary>
        /// ユークリッド距離
        /// </summary>
        EuclidianDistance,
        /// <summary>
        /// マンハッタン距離
        /// </summary>
        ManhattanDistance,
        /// <summary>
        /// コストなし
        /// </summary>
        ZeroCost,
        /// <summary>
        /// ハミング距離
        /// </summary>
        Hamming,
        /// <summary>
        /// 行列のハミング距離
        /// </summary>
        MatrixHamming,
        /// <summary>
        /// DTW距離（int型）
        /// </summary>
        DTW_int,
        /// <summary>
        /// DTW距離（string型）
        /// </summary>
        DTW_string
    }
    /// <summary>
    /// 距離関数インターフェース
    /// このインターフェースを継承することで独自の距離関数を定義可能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDistance<T>
    {
        /// <summary>
        /// 距離関数
        /// </summary>
        /// <param name="x">比較元</param>
        /// <param name="y">比較対象</param>
        /// <returns>xとyの距離</returns>
        double DP(T x, T y);
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        DistanceFunction getDistanceFunction();
    }
    #region 単一データの距離関数
    /// <summary>
    /// 数値データの距離関数
    /// </summary>
    public class NumericDistance : IDistance<int>
    {
        /// <summary>
        /// 距離関数（数直線）
        /// </summary>
        /// <param name="x">int型の数値</param>
        /// <param name="y">int型の数値</param>
        /// <returns>2つの値の差の絶対値</returns>
        public double DP(int x, int y)
        {
            double result = (double)(Math.Abs(x - y));
            return result;
        }
        /// <summary>
        /// 距離関数の種類を返す
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.Numerical;
        }
    }

    /// <summary>
    /// 文字列データの距離関数
    /// </summary>
    public class StringDistance : IDistance<string>
    {
        /// <summary>
        /// 距離関数（文字列）
        /// </summary>
        /// <param name="x">string型の値</param>
        /// <param name="y">string型の値</param>
        /// <returns></returns>
        public double DP(string x, string y)
        {
            double result = 0.0;
            int iCompare = string.Compare(x, y);
            if (iCompare == 0)
            {
                result = 0.0;
            }
            else
            {
                result = 5.0;
            }
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.TextString;
        }
    }
    /// <summary>
    /// 距離関数（ユークリッド距離）
    /// </summary>
    public class EuclidianDistance : IDistance<Point>
    {
        /// <summary>
        /// 距離関数（２次元のユークリッド距離）
        /// </summary>
        /// <param name="x">Point型の値</param>
        /// <param name="y">Point型の値</param>
        /// <returns>2点間の距離</returns>
        public double DP(Point x, Point y)
        {
            double result = Math.Sqrt(Math.Pow(x.X - y.Y, 2) + Math.Pow(x.Y - y.Y, 2));
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.EuclidianDistance;
        }
    }
    /// <summary>
    /// 距離関数（マンハッタン距離）
    /// </summary>
    public class ManhattanDistance : IDistance<Point>
    {
        /// <summary>
        /// 距離関数（マンハッタン距離）
        /// </summary>
        /// <param name="x">Point型の値</param>
        /// <param name="y">Point型の値</param>
        /// <returns></returns>
        public double DP(Point x, Point y)
        {
            double result = Math.Abs(x.X - y.X) + Math.Abs(x.X - y.Y);
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.ManhattanDistance;
        }
    }
    /// <summary>
    /// short型の距離関数クラス
    /// </summary>
    public class ShortDistance : IDistance<short>
    {
        /// <summary>
        /// 距離関数（short型、文字列と同じ扱い）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double DP(short x, short y)
        {
            double result = 0.0;
            if (x == y)
            {
                result = 0.0;
            }
            else
            {
                result = 5.0;
            }
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.TextString;
        }
    }
    /// <summary>
    /// 文字列の一致にかかわらずコストをかけない
    /// </summary>
    public class ZeroCost : IDistance<string>
    {
        /// <summary>
        /// 距離関数（コストなし）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double DP(string x, string y)
        {
            double result = 0.0;
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.ZeroCost;
        }
    }
    #endregion

    #region 系列データの距離関数
    /// <summary>
    /// 距離関数（ハミング距離）
    /// </summary>
    public class HammingDistance : IDistance<string[]>
    {
        /// <summary>
        /// 距離関数（ハミング距離）
        /// </summary>
        /// <param name="x">string型の配列</param>
        /// <param name="y">string型の配列</param>
        /// <returns></returns>
        public double DP(string[] x, string[] y)
        {
            double result = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    result += 1.0;
                }
            }
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.Hamming;
        }
    }
    /// <summary>
    /// 行列のハミング距離（データは必ず多次元配列）
    /// </summary>
    public class MatrixHammingDistance : IDistance<string[,]>
    {
        /// <summary>
        /// ハミング距離（多次元配列、同じ次元数）
        /// </summary>
        public MatrixHammingDistance()
        {
        }
        /// <summary>
        /// 距離関数（行列のハミング距離）
        /// </summary>
        /// <param name="x">2次元配列</param>
        /// <param name="y">2次元配列</param>
        /// <returns></returns>
        public double DP(string[,] x, string[,] y)
        {
            double result = 0.0;
            for (int j = 0; j < x.GetLength(1); j++)
            {
                for (int i = 0; i < x.GetLength(0); i++)
                {
                    if (x[i, j] != y[i, j])
                    {
                        result += 1.0;
                    }
                }
            }
                return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.MatrixHamming;
        }
    }
    /// <summary>
    /// DTW距離（int型）
    /// </summary>
    public class DTWDistanceInt : IDistance<int[]>
    {
        /// <summary>
        /// 距離関数（int型のdtw距離）
        /// </summary>
        /// <param name="x">int型の配列</param>
        /// <param name="y">int型の配列</param>
        /// <returns></returns>
        public double DP(int[] x, int[] y)
        {
            double result = 0.0;
            BasicDTW<int> dtw = new BasicDTW<int>(x, y, new NumericDistance());
            result = dtw.Distance;
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.DTW_int;
        }
    }
    /// <summary>
    /// DTW距離（string型）
    /// </summary>
    public class DTWDistanceString : IDistance<string[]>
    {
        /// <summary>
        /// 距離関数（string型のdtw距離）
        /// </summary>
        /// <param name="x">string型の配列</param>
        /// <param name="y">string型の配列</param>
        /// <returns></returns>
        public double DP(string[] x, string[] y)
        {
            double result = 0.0;
            BasicDTW<string> dtw = new BasicDTW<string>(x, y, new StringDistance());
            result = dtw.Distance;
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.DTW_string;
        }
    }
    /// <summary>
    /// ユークリッド距離
    /// </summary>
    public class EuclidieanSpace : IDistance<short[]>
    {
        /// <summary>
        /// 距離関数（ユークリッド距離）
        /// </summary>
        /// <param name="x">short型の配列</param>
        /// <param name="y">short型の配列</param>
        /// <returns>同じ要素同士の差の２乗の和の累乗根</returns>
        public double DP(short[] x, short[] y)
        {
            double result = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                result += Math.Pow((double)(x[i] - y[i]), 2.0);
            }
            result = Math.Sqrt(result);
            return result;
        }
        /// <summary>
        /// 距離関数の種類
        /// </summary>
        /// <returns></returns>
        public DistanceFunction getDistanceFunction()
        {
            return DistanceFunction.EuclidianDistance;
        }
    }
    #endregion
}
