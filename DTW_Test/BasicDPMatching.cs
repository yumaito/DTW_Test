using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DTW_Test
{
    public enum PathPattern
    {
        both, original, subject
    }
    /// <summary>
    /// DPマッチングの基本となる抽象クラス（継承でのみ使用可とする、コンストラクタを必ず実装すること）
    /// </summary>
    abstract class BasicDPMatching<Type>// where Type : IComparable
    {
        #region フィールド
        //DPマッチングコスト
        private const int shiftPenalty = 1;
        private const int nonMatchPenalty = 5;
        //
        //内部で計算用に用いる変数
        protected int originalLength;
        protected int subjectLength;
        private PathPattern[,] from;
        private int[,] cost;
        protected int[,] matchingField;
        //protected int[,] outMatchingField;
        //protected DiffPath[,][] matchingDiffFieldOriginal;
        //protected DiffPath[,][] matchingDiffFieldSubject;
        //実際に比較する系列
        protected Type[] original;
        protected Type[] subject;
        #region 相違点構造体

        public struct DiffPath
        {
            /// <summary>
            /// 対応を表す
            /// </summary>
            public Point correspond;
            /// <summary>
            /// マッチしているか否か
            /// </summary>
            public bool match;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="p"></param>
            /// <param name="b"></param>
            public DiffPath(Point p, bool b)
            {
                this.correspond = p;
                this.match = b;
            }
        }
        #endregion

        //外部からアクセス可能な変数（プロパティを通じて）
        private double distance;
        //private DiffPath[] originalPath;
        //private DiffPath[] subjectPath;
        private List<DiffPath> cor;
        //private bool shiftOnly;
        protected bool[,] isPath;

        #endregion

        #region プロパティ
        public List<DiffPath> Cor
        {
            get
            {
                return cor;
            }
        }
        public bool[,] IsPath
        {
            get
            {
                return isPath;
            }
        }
        public PathPattern[,] From
        {
            get
            {
                return from;
            }
        }
        public int[,] Cost
        {
            get
            {
                return cost;
            }
        }
        /// <summary>
        /// DPマッチングの結果
        /// </summary>
        public double Distance
        {
            get
            {
                return distance;
            }
        }
        ///// <summary>
        ///// 対応要素配列（比較元から比較対象を見た場合）
        ///// </summary>
        //public DiffPath[] OriginalPath
        //{
        //    get
        //    {
        //        return originalPath;
        //    }
        //}
        ///// <summary>
        ///// 対応要素配列（比較対象から比較元を見た場合）
        ///// </summary>
        //public DiffPath[] SubjectPath
        //{
        //    get
        //    {
        //        return subjectPath;
        //    }
        //}
        ///// <summary>
        ///// 2つの系列が単純にずれただけであるか否か
        ///// </summary>
        //public bool ShiftOnly
        //{
        //    get
        //    {
        //        return shiftOnly;
        //    }
        //}
        //public int[,] OutMatchingField
        //{
        //    get
        //    {
        //        return outMatchingField;
        //    }
        //}
        #endregion

        #region コンストラクタ

        #endregion

        #region マッチング表作成用メソッド（継承される）
        protected abstract void MatchingField();
        #endregion

        #region メンバ関数（protected）
        protected void DP_Run()
        {
            MatchingField();
            this.DpMatching();
            this.MatchingLength();
        }
        private void DpMatching()
        {
            //shiftOnly = true;
            cost = new int[originalLength, subjectLength];
            from = new PathPattern[originalLength, subjectLength];
            int temp1, temp2, temp3;
            //コスト計算開始
            cost[0, 0] = matchingField[0, 0] * nonMatchPenalty;
            from[0, 0] = PathPattern.both;
            //i側の縁
            for (int i = 1; i < originalLength; i++)
            {
                cost[i, 0] = cost[i - 1, 0] + shiftPenalty + matchingField[i, 0] * nonMatchPenalty;
                from[i, 0] = PathPattern.original;
            }
            //j側の縁
            for (int j = 1; j < subjectLength; j++)
            {
                cost[0, j] = cost[0, j - 1] + shiftPenalty + matchingField[0, j] * nonMatchPenalty;
                from[0, j] = PathPattern.subject;
            }
            //中間部
            for (int i = 1; i < originalLength; i++)
            {
                for (int j = 1; j < subjectLength; j++)
                {
                    //直前３状態からの遷移コストを計算
                    temp1 = cost[i - 1, j - 1] + matchingField[i, j] * nonMatchPenalty;
                    temp2 = cost[i - 1, j] + matchingField[i, j] * nonMatchPenalty + shiftPenalty;
                    temp3 = cost[i, j - 1] + matchingField[i, j] * nonMatchPenalty + shiftPenalty;
                    if (temp1 <= temp2 && temp1 <= temp3)
                    {
                        cost[i, j] = temp1;
                        from[i, j] = PathPattern.both;
                    }
                    else if (temp2 <= temp3)
                    {
                        cost[i, j] = temp2;
                        from[i, j] = PathPattern.original;
                    }
                    else
                    {
                        cost[i, j] = temp3;
                        from[i, j] = PathPattern.subject;
                    }
                }
            }
            distance = cost[originalLength - 1, subjectLength - 1];
        }
        //protected void OriginalCorrPath()
        //{
        //    PathPattern[] path = Path();
        //    int length = path.Length;
        //    int i = 0;
        //    int j = 0;
        //    DiffPath[] diffPath = new DiffPath[originalLength];
        //    for (int k = 0; k < length; k++)
        //    {
        //        switch (path[k])
        //        {
        //            case PathPattern.both:
        //                diffPath[i].corr = j;
        //                if (matchingField[i, j] != 0)
        //                {
        //                    diffPath[i].match = false;
        //                }
        //                else
        //                {
        //                    diffPath[i].match = true;
        //                }
        //                diffPath[i].shift = false;
        //                shiftOnly = false;
        //                i++;
        //                j++;
        //                break;
        //            case PathPattern.original:
        //                diffPath[i].corr = j;
        //                if (matchingField[i, j] != 0)
        //                {
        //                    diffPath[i].match = false;
        //                }
        //                else
        //                {
        //                    diffPath[i].match = true;
        //                }
        //                diffPath[i].shift = true;
        //                i++;
        //                break;
        //            case PathPattern.subject:
        //                j++;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    originalPath = diffPath;
        //}
        //protected void SubjectCorrPath()
        //{
        //    PathPattern[] path = Path();
        //    int length = path.Length;
        //    int i = 0;
        //    int j = 0;
        //    DiffPath[] diffPath = new DiffPath[subjectLength];
        //    for (int k = 0; k < length; k++)
        //    {
        //        switch (path[k])
        //        {
        //            case PathPattern.both:
        //                diffPath[j].corr = i;
        //                if (matchingField[i, j] != 0)
        //                {
        //                    diffPath[j].match = false;
        //                }
        //                else
        //                {
        //                    diffPath[j].match = true;
        //                }
        //                diffPath[j].shift = false;
        //                shiftOnly = false;
        //                i++;
        //                j++;
        //                break;
        //            case PathPattern.original:
        //                i++;
        //                break;
        //            case PathPattern.subject:
        //                diffPath[j].corr = i;
        //                if (matchingField[i, j] != 0)
        //                {
        //                    diffPath[j].match = false;
        //                }
        //                else
        //                {
        //                    diffPath[j].match = true;
        //                }
        //                diffPath[j].shift = true;
        //                j++;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    subjectPath = diffPath;
        //}
        //
        protected DiffPath[] Return1DArray(DiffPath[][] array)
        {
            List<DiffPath> temp = new List<DiffPath>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    temp.Add(array[i][j]);
                }
            }
            return temp.ToArray();
        }

        //public bool IsPath(int i, int j)
        //{
        //    bool result = false;
        //    foreach (DiffPath temp in cor)
        //    {
        //        if (temp.correspond.X == i && temp.correspond.Y == j)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
        #endregion
        #region メンバ関数（private）
        private int MatchingLength()
        {
            int length = originalLength + subjectLength;
            int i = originalLength - 1;
            int j = subjectLength - 1;
            cor = new List<DiffPath>();
            //int k;
            int result = 0;
            isPath = new bool[originalLength, subjectLength];
            //fromの値に応じてゴールからスタートへ戻る
            //その際のマッチング結果の文字列の長さを測る
            while (i >= 0 && j >= 0)
            {
                switch (from[i, j])
                {
                    case PathPattern.both:
                        isPath[i, j] = true;
                        Point p = new Point(i, j);
                        bool temp;
                        if (matchingField[i, j] == 0)
                        {
                            temp = true;
                        }
                        else
                        {
                            temp = false;
                        }
                        cor.Add(new DiffPath(p, temp));
                        i--;
                        j--;
                        break;
                    case PathPattern.original:
                        isPath[i, j] = true;
                        p = new Point(i, j);
                        if (matchingField[i, j] == 0)
                        {
                            temp = true;
                        }
                        else
                        {
                            temp = false;
                        }
                        cor.Add(new DiffPath(p, temp));
                        i--;
                        break;
                    case PathPattern.subject:
                        isPath[i, j] = true;
                        p = new Point(i, j);
                        if (matchingField[i, j] == 0)
                        {
                            temp = true;
                        }
                        else
                        {
                            temp = false;
                        }
                        cor.Add(new DiffPath(p, temp));
                        j--;
                        break;
                }
                result++;
            }
            return result;
        }
        private PathPattern[] Path()
        {
            int length = MatchingLength();
            PathPattern[] result = new PathPattern[length];
            int i = originalLength - 1;
            int j = subjectLength - 1;
            result[length - 1] = PathPattern.both;
            for (int k = length - 2; k >= 0; k--)
            {
                switch (from[i, j])
                {
                    case PathPattern.both:
                        result[k] = from[i, j];
                        i--;
                        j--;
                        break;
                    case PathPattern.original:
                        result[k] = from[i, j];
                        i--;
                        break;
                    case PathPattern.subject:
                        result[k] = from[i, j];
                        j--;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        #endregion
    }
}
