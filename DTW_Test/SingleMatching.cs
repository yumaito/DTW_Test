using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTW_Test
{
    #region int
    /// <summary>
    /// int型の単一系列のマッチング
    /// </summary>
    class MatchingInt : BasicDPMatching<int>
    {
        #region コンストラクタ
        /// <summary>
        /// int型の単一系列のマッチング
        /// </summary>
        /// <param name="original"></param>
        /// <param name="subject"></param>
        public MatchingInt(int[] original, int[] subject)
        {
            this.original = original;
            this.subject = subject;
            //
            this.originalLength = original.Length;
            this.subjectLength = subject.Length;
            //
            this.matchingField = new int[originalLength, subjectLength];
            //
            this.DP_Run();
            //this.isPath = new bool[originalLength, subjectLength];
            //this.MatchingField();
            //this.DpMatching();
            ////this.OriginalCorrPath();
            //this.SubjectCorrPath();
        }
        #endregion
        #region MatchingField
        protected override void MatchingField()
        {
            //throw new NotImplementedException();
            for (int i = 0; i < originalLength; i++)
            {
                for (int j = 0; j < subjectLength; j++)
                {
                    this.matchingField[i, j] = Math.Abs(original[i] - subject[j]);
                }
            }
        }
        #endregion
    }
    #endregion
    #region string
    /// <summary>
    /// string型の単一系列のマッチング
    /// </summary>
    class MatchingString : BasicDPMatching<string>
    {
        #region コンストラクタ
        /// <summary>
        /// string型の単一系列のマッチング
        /// </summary>
        /// <param name="original"></param>
        /// <param name="subject"></param>
        public MatchingString(string[] original, string[] subject)
        {
            this.original = original;
            this.subject = subject;
            //
            this.originalLength = original.Length;
            this.subjectLength = subject.Length;
            //
            this.matchingField = new int[originalLength, subjectLength];
            //this.isPath = new bool[originalLength, subjectLength];
            this.DP_Run();
            //this.MatchingField();
            //this.DpMatching();
            //this.OriginalCorrPath();
            //this.SubjectCorrPath();
        }
        #endregion
        #region MatchingField
        protected override void MatchingField()
        {
            //throw new NotImplementedException();
            for (int i = 0; i < originalLength; i++)
            {
                for (int j = 0; j < subjectLength; j++)
                {
                    if (original[i] == subject[j])
                    {
                        this.matchingField[i, j] = 0;
                    }
                    else
                    {
                        this.matchingField[i, j] = 1;
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
