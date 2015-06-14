using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DTW_Library
{
    /// <summary>
    /// DTW計算時の経路と比較要素が一致しているかを管理するクラス
    /// </summary>
    public class DTW_Path
    {
        #region フィールド
        private int original;
        private int subject;
        private bool match;
        #endregion
        /// <summary>
        /// DTW_Pathのコンストラクタ
        /// </summary>
        /// <param name="original">比較元のid番号</param>
        /// <param name="subject">比較対象のid番号</param>
        /// <param name="match">比較している要素同士が一致しているか否か</param>
        public DTW_Path(int original, int subject, bool match)
        {
            this.original = original;
            this.subject = subject;
            this.match = match;
        }
        #region プロパティ
        /// <summary>
        /// 比較元のid番号
        /// </summary>
        public int Original
        {
            get
            {
                return original;
            }
        }
        /// <summary>
        /// 比較対象のid番号
        /// </summary>
        public int Subject
        {
            get
            {
                return subject;
            }
        }
        /// <summary>
        /// 比較している要素同士が一致しているか否か
        /// </summary>
        public bool Match
        {
            get
            {
                return match;
            }
        }
        #endregion

        #region メソッド
        #endregion
    }
}
