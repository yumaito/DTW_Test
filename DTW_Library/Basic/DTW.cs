using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTW
{
    /// <summary>
    /// 経路の移動パターン
    /// </summary>
    public enum PathPattern
    {   
        /// <summary>
        /// 両方（斜め下）
        /// </summary>
        Both,
        /// <summary>
        /// 比較元のみ（右）
        /// </summary>
        Original,
        /// <summary>
        /// 比較対象のみ（下）
        /// </summary>
        Subject
    }
    /// <summary>
    /// 単一の系列を扱うDTWの抽象クラス
    /// </summary>
    public class BasicDTW<Type> : IDTW_base
    {
        #region フィールド

        #region private
        private DateTime startTime;
        private DateTime endTime;
        //DPマッチングのコスト
        private int Shift_Pealty = 10;
        //内部での計算に用いる変数
        private PathPattern[,] from;
        private double[,] cost;
        //外部からプロパティを通じてアクセス可能な変数
        private double distance;
        private List<DTW_Path> path;
        #endregion

        #region protected
        /// <summary>
        /// 比較元の配列の長さ
        /// </summary>
        protected int originalLength;
        /// <summary>
        /// 比較対象の配列の長さ
        /// </summary>
        protected int subjectLength;
        /// <summary>
        /// コストの初期値
        /// </summary>
        protected double[,] costField;
        /// <summary>
        /// 距離関数
        /// </summary>
        protected IDistance<Type> dp;
        //実際に比較する系列
        /// <summary>
        /// 実際に比較する配列（比較元）
        /// </summary>
        protected Type[] original;
        /// <summary>
        /// 実際に比較する配列（比較対象）
        /// </summary>
        protected Type[] subject;
        #endregion

        #region プロパティ
        /// <summary>
        /// DPマッチング時の経路
        /// </summary>
        public List<DTW_Path> Path
        {
            get
            {
                return path;
            }
        }
        /// <summary>
        /// DPマッチング（DTW）距離
        /// </summary>
        public double Distance
        {
            get
            {
                return distance;
            }
        }
        /// <summary>
        /// 最終的なコスト
        /// </summary>
        public double[,] Cost
        {
            get
            {
                return cost;
            }
        }
        /// <summary>
        /// どこからきたかを表す配列
        /// </summary>
        public PathPattern[,] From
        {
            get
            {
                return from;
            }
        }
        /// <summary>
        /// 計算にかかった時間
        /// </summary>
        public TimeSpan CalcTime
        {
            get
            {
                return this.endTime - this.startTime;
            }
        }
        #endregion

        #endregion

        #region コンストラクタ
        /// <summary>
        /// Type型の単一系列のマッチング
        /// </summary>
        /// <param name="original">比較元配列</param>
        /// <param name="subject">比較対象配列</param>
        /// <param name="dp">距離関数の指定</param>
        public BasicDTW(Type[] original, Type[] subject, IDistance<Type> dp, bool isPathCalc)
        {
            
            //計算開始時刻
            this.startTime = DateTime.Now;
            //int[][]のような２次元配列の場合は
            //Typeがint[]で、originalやsubjectはint[]の配列という位置づけ
            this.original = original;
            this.subject = subject;
            this.dp = dp;
            //this.Shift_Pealty = shift;
            //２次元配列の場合も考えて0次元目の大きさを取得
            this.originalLength = original.GetLength(0);
            this.subjectLength = subject.GetLength(0);
            //
            this.costField = new double[originalLength, subjectLength];
            //
            this.DTW_Run(isPathCalc);
            //計算終了時刻
            this.endTime = DateTime.Now;
        }

        /// <summary>
        /// Type型の単一系列のマッチング
        /// </summary>
        /// <param name="original">比較元配列</param>
        /// <param name="subject">比較対象配列</param>
        /// <param name="dp">距離関数の指定</param>
        /// <param name="shift">ずれコスト</param>
        public BasicDTW(Type[] original, Type[] subject, IDistance<Type> dp, int shift, bool isPathCalc)
        {
            //int[][]のような２次元配列の場合は
            //Typeがint[]で、originalやsubjectはint[]の配列という位置づけ
            this.original = original;
            this.subject = subject;
            this.dp = dp;
            this.Shift_Pealty = shift;
            //２次元配列の場合も考えて0次元目の大きさを取得
            this.originalLength = original.GetLength(0);
            this.subjectLength = subject.GetLength(0);
            //
            this.costField = new double[originalLength, subjectLength];
            //
            this.DTW_Run(isPathCalc);
        }
        #endregion

        #region メソッド

        #region private
        /// <summary>
        /// DTW本体
        /// </summary>
        private void DTW()
        {
            cost = new double[originalLength, subjectLength];
            from = new PathPattern[originalLength, subjectLength];
            double temp1, temp2, temp3;
            //コスト計算開始
            //始状態
            cost[0, 0] = costField[0, 0];
            from[0, 0] = PathPattern.Both;
            //i側の縁
            for (int i = 1; i < originalLength; i++)
            {
                cost[i, 0] = cost[i - 1, 0] + Shift_Pealty + costField[i, 0];
                from[i, 0] = PathPattern.Original;
            }
            //j側の縁
            for (int j = 1; j < subjectLength; j++)
            {
                cost[0, j] = cost[0, j - 1] + Shift_Pealty + cost[0, j];
                from[0, j] = PathPattern.Subject;
            }
            //中間部
            for (int i = 1; i < originalLength; i++)
            {
                for (int j = 1; j < subjectLength; j++)
                {
                    //直前3状態からの遷移のコストを計算
                    temp1 = cost[i - 1, j - 1] + costField[i, j];
                    temp2 = cost[i - 1, j] + costField[i, j] + Shift_Pealty;
                    temp3 = cost[i, j - 1] + costField[i, j] + Shift_Pealty;
                    if (temp1 <= temp2 && temp1 <= temp3)
                    {
                        cost[i, j] = temp1;
                        from[i, j] = PathPattern.Both;
                    }
                    else if (temp2 <= temp3)
                    {
                        cost[i, j] = temp2;
                        from[i, j] = PathPattern.Original;
                    }
                    else
                    {
                        cost[i, j] = temp3;
                        from[i, j] = PathPattern.Subject;
                    }
                }
            }
            distance = cost[originalLength - 1, subjectLength - 1];
        }
        /// <summary>
        /// 経路計算
        /// </summary>
        private void CalcPath()
        {
            int length = originalLength + subjectLength;
            int i = originalLength - 1;
            int j = subjectLength - 1;
            path = new List<DTW_Path>();
            //fromの値に応じてゴールからスタートまで戻る
            //その際の経路を保存する
            while (i >= 0 && j >= 0)
            {
                //現在の経路をリストに追加
                if (costField[i, j] == 0)
                {
                    path.Add(new DTW_Path(i, j, true));
                }
                else
                {
                    path.Add(new DTW_Path(i, j, false));
                }
                //経路のパターンに応じてiとjの値を減少させる
                switch (from[i, j])
                {
                    case PathPattern.Both:
                        i--;
                        j--;
                        break;
                    case PathPattern.Original:
                        i--;
                        break;
                    case PathPattern.Subject:
                        j--;
                        break;
                    default:
                        break;
                }
            }
            //リストを逆転させてスタートからゴールまでの経路リストにする
            path.Reverse();
        }
        /// <summary>
        /// 初期状態の距離計算
        /// </summary>
        private void PrimaryCostField()
        {
            for (int i = 0; i < originalLength; i++)
            {
                for (int j = 0; j < subjectLength; j++)
                {
                    //２次元配列の場合はoriginal[i]、subject[j]ともに配列となる
                    costField[i,j] = dp.DP(original[i], subject[j]);
                }
            }
        }
        #endregion

        #region public
        /// <summary>
        /// DTW距離の計算に必要なメソッドを実行するメソッド
        /// コンストラクタで実行済み
        /// 特に呼び出す必要はなし
        /// </summary>
        public void DTW_Run(bool isPathCalc)
        {
            //初期状態
            PrimaryCostField();
            //DTW本体の実行
            this.DTW();
            if (isPathCalc)
            {
                //経路計算
                this.CalcPath();
            }
        }
        /// <summary>
        /// 指定した要素の組合せが経路になっているかどうか
        /// </summary>
        /// <param name="i">比較元（original）系列の要素番号</param>
        /// <param name="j">比較対象（subject）系列の要素番号</param>
        /// <returns>経路になっていればtrue、なっていなければfalse</returns>
        public bool IsPath(int i, int j)
        {
            bool result = false;
            foreach (DTW_Path d in path)
            {
                if ((d.Original == i) && (d.Subject == j))
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// originalの指定した要素と対応している要素番号をリスト形式で返すメソッド
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<int> originalPairNum(int num)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < subjectLength; i++)
            {
                if (this.IsPath(num, i))
                {
                    result.Add(i);
                }
            }
            return result;
        }
        /// <summary>
        /// subjectの指定した要素と対応している要素番号をリスト形式で返すメソッド
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<int> subjectPairNum(int num)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < originalLength; i++)
            {
                if (this.IsPath(i, num))
                {
                    result.Add(i);
                }
            }
            return result;
        }
        #endregion
        #endregion

        #region abstracted
        //protected abstract void PrimaryCostField();
        #endregion
    }
}
