using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTW;

namespace DTW_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            completeMatching = false;
        }

        #region フィールド
        //MatchingInt matchingInt;
        //MatchingString matchingString;
        BasicDTW<int> dtwInt;
        BasicDTW<string> dtwString;
        BasicDTW<string[]> dtwStringArray;
        BasicDTW<int[]> dtwIntArray;
        //DTW_Library.Distance<int> dpInt;
        //DTW_Library.Distance<string> dpString;
        //
        string[] original;
        string[] subject;
        int[] originalInt;
        int[] subjectInt;
        //
        string[][] originalArray;
        string[][] subjectArray;
        int[][] originalIntArray;
        int[][] subjectIntArray;
        bool completeMatching;
        //bool intMatching;
        
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxArray.Checked)
            {
                //「[」で分割
                string[] originalTemp = textBoxOriginal.Text.Split('[');
                string[] subjectTemp = textBoxSubject.Text.Split('[');
                //分割した数＝配列の０次元目の大きさ
                originalArray = new string[originalTemp.Length][];
                subjectArray = new string[subjectTemp.Length][];
                originalIntArray = new int[originalTemp.Length][];
                subjectIntArray = new int[subjectTemp.Length][];
                //
                //値を入れる
                for (int i = 0; i < originalArray.GetLength(0); i++)
                {
                    originalArray[i] = originalTemp[i].Split(',');
                    originalIntArray[i] = new int[originalArray[i].Length];
                }
                for (int i = 0; i < subjectArray.GetLength(0);i++)
                {
                    subjectArray[i] = subjectTemp[i].Split(',');
                    subjectIntArray[i] = new int[subjectArray[i].Length];
                }
                if (checkBoxIsNumeric.Checked)
                {
                    for (int i = 0; i < originalArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < originalArray[i].Length; j++)
                        {
                            if (int.TryParse(originalArray[i][j], out originalIntArray[i][j]))
                            {
                            }
                            else
                            {
                                MessageBox.Show("数値データ以外が入力されました。\n文字列としてマッチングを行います。", "エラー");
                                dtwStringArray = new BasicDTW<string[]>(originalArray, subjectArray, new DTWDistanceString(), true);
                                textBoxDistance.Text = dtwStringArray.Distance.ToString();
                                checkBoxIsNumeric.Checked = false;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < subjectArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < subjectArray[i].Length; j++)
                        {
                            if (int.TryParse(subjectArray[i][j], out subjectIntArray[i][j]))
                            {
                            }
                        }
                    }
                    dtwIntArray = new BasicDTW<int[]>(originalIntArray, subjectIntArray, new DTWDistanceInt(), true);
                    textBoxDistance.Text = dtwIntArray.Distance.ToString();
                }
                else
                {
                    dtwStringArray = new BasicDTW<string[]>(originalArray, subjectArray, new DTWDistanceString(), true);
                    textBoxDistance.Text = dtwStringArray.Distance.ToString();
                }
            }
            else
            {
                #region 単一系列
                original = textBoxOriginal.Text.Split(',');
                subject = textBoxSubject.Text.Split(',');
                originalInt = new int[original.Length];
                subjectInt = new int[subject.Length];
                //intMatching = false;
                //
                if (checkBoxIsNumeric.Checked)
                {
                    for (int i = 0; i < original.Length; i++)
                    {
                        if (int.TryParse(original[i], out originalInt[i]))
                        {
                        }
                        else
                        {
                            MessageBox.Show("数値データ以外が入力されました。\n文字列としてマッチングを行います。", "エラー");
                            dtwString = new BasicDTW<string>(original, subject, new StringDistance(), true);

                            //matchingString = new MatchingString(original, subject);
                            textBoxDistance.Text = dtwString.Distance.ToString();
                            checkBoxIsNumeric.Checked = false;
                            break;
                        }
                    }
                    for (int i = 0; i < subject.Length; i++)
                    {
                        if (int.TryParse(subject[i], out subjectInt[i]))
                        {
                        }
                        else
                        {
                            MessageBox.Show("数値データ以外が入力されました。\n文字列としてマッチングを行います。", "エラー");
                            dtwString = new BasicDTW<string>(original, subject, new StringDistance(), true);

                            //matchingString = new MatchingString(original, subject);
                            textBoxDistance.Text = dtwString.Distance.ToString();
                            checkBoxIsNumeric.Checked = false;
                            break;
                        }
                    }
                    dtwInt = new BasicDTW<int>(originalInt, subjectInt, new NumericDistance(), true);
                    //matchingInt = new MatchingInt(originalInt, subjectInt);
                    textBoxDistance.Text = dtwInt.Distance.ToString();
                }
                else
                {
                    //matchingString = new MatchingString(original, subject);
                    dtwString = new BasicDTW<string>(original, subject, new StringDistance(), true);
                    textBoxDistance.Text = dtwString.Distance.ToString();
                }
                completeMatching = true;
                #endregion
            }
            
            pictureBoxMatchingFieldView.Invalidate();
            pictureBoxCorrView.Invalidate();
        }

        private void pictureBoxCorrView_Click(object sender, EventArgs e)
        {

        }
        #region 再描画
        private void pictureBoxCorrView_Paint_1(object sender, PaintEventArgs e)
        {
            if (completeMatching)
            {
                if (checkBoxIsNumeric.Checked)
                {
                    DrawCorrespond(e.Graphics, dtwInt.Path, this.IntToString(originalInt), this.IntToString(subjectInt));
                }
                else
                {
                    DrawCorrespond(e.Graphics, dtwString.Path, original, subject);
                }
            }
        }

        private void pictureBoxMatchingFieldView_Paint_1(object sender, PaintEventArgs e)
        {
            if (completeMatching)
            {
                //Graphics g = pictureBoxMatchingFieldView.CreateGraphics();
                if (checkBoxIsNumeric.Checked)
                {

                    DrawGrid(e.Graphics, dtwInt.Cost, this.IntToString(originalInt), this.IntToString(subjectInt));
                }
                else
                {
                    DrawGrid(e.Graphics, dtwString.Cost, original, subject);
                }
            }
        }
        private void pictureBoxCorrView_Paint(object sender, PaintEventArgs e)
        {
            if (completeMatching)
            {
                //if (checkBoxIsNumeric.Checked)
                //{
                //    DrawCorrespond(e.Graphics, matchingInt.Cost, matchingInt.IsPath,
                //        this.IntToString(originalInt), this.IntToString(subjectInt));
                //}
                //else
                //{
                //    DrawCorrespond(e.Graphics, matchingString.Cost, matchingString.IsPath, original, subject);
                //}
            }
        }

        private void pictureBoxMatchingFieldView_Paint(object sender, PaintEventArgs e)
        {
            
        }
        #endregion
        #region 描画
        //matchingFieldView
        //グリッド描画
        private void DrawGrid(Graphics g, double[,] matchingField, string[] original, string[] subject)
        {
            int gridLineWidth = 2;
            Pen p = new Pen(Color.Black, gridLineWidth);
            //int gridWidth = 50;
            int offset = 10;
            int secondOffset = 5;
            Font fnt = new Font("MS ゴシック", 20);
            Size stringSize = TextRenderer.MeasureText(g, "MMM", fnt);
            //gridWidth = Size.Width;
            for (int i = 0; i < matchingField.GetLength(1) + 1; i++)
            {
                for (int j = 0; j < matchingField.GetLength(0) + 1; j++)
                {

                    Point point = new Point(stringSize.Width * j + offset, stringSize.Height * i + offset);
                    string temp = "(" + i.ToString() + "," + j.ToString() + ")";
                    //if (i == 0 && j == 0)
                    //{
                    //    
                    //}
                    //originalの値を描画
                    if (j == 0)
                    {
                        point = new Point(stringSize.Width * j + offset,
                            stringSize.Height * i + offset + secondOffset);
                        if (i > 0)
                        {
                            g.DrawString(subject[i - 1], fnt, Brushes.Black, point);
                        }
                    }
                    //subjectの値を描画
                    if (i == 0)
                    {
                        point = new Point(stringSize.Width * j + offset + secondOffset,
                            stringSize.Height * i + offset);
                        if (j > 0)
                        {
                            g.DrawString(original[j - 1], fnt, Brushes.Black, point);
                        }
                    }
                    //内部の描画
                    if (i != 0 && j != 0)
                    {
                        string path = "";
                        PathPattern tempPath;
                        if (checkBoxIsNumeric.Checked)
                        {
                            tempPath = dtwInt.From[j - 1, i - 1];
                        }
                        else
                        {
                            tempPath = dtwString.From[j - 1, i - 1];
                        }
                        switch(tempPath)
                        {
                            case PathPattern.Both:
                                path = "＼";
                                break;
                            case PathPattern.Original:
                                path = "←";
                                break;
                            case PathPattern.Subject:
                                path="↑";
                                break;
                            default:
                                break;
                        }
                        //if (tempPath == PathPattern.both)
                        //{
                        //    path = "＼";
                        //}
                        //else if (tempPath == PathPattern.original)
                        //{
                        //    path = "←";
                        //}
                        //else
                        //{
                        //    path = "↑";
                        //}
                        point = new Point(stringSize.Width * j + offset + secondOffset,
                            stringSize.Height * i + offset + secondOffset);

                        //(j-1,i-1)が経路かどうかを調べる
                        if (checkBoxIsNumeric.Checked)
                        {
                            if (dtwInt.IsPath(j - 1, i - 1))
                            {
                                g.FillRectangle(Brushes.SkyBlue, new Rectangle(point, stringSize));
                            }
                        }
                        else
                        {
                            if (dtwString.IsPath(j - 1, i - 1))
                            {
                                g.FillRectangle(Brushes.SkyBlue, new Rectangle(point, stringSize));
                            }
                        }
                        g.DrawString(matchingField[j - 1, i - 1].ToString() + path, fnt, Brushes.Black, point);
                    }
                    //最も左上（凡例）
                    if (i == 0 && j == 0)
                    {
                        point = new Point(stringSize.Width * j + offset, stringSize.Height * i + offset);
                        g.DrawString("↓→", fnt, Brushes.Black, point);
                    }
                    Rectangle rect = new Rectangle(point, stringSize);
                    g.DrawRectangle(p, rect);
                    //pictureBoxの調整
                    if (point.X + stringSize.Width > pictureBoxMatchingFieldView.Width)
                    {
                        pictureBoxMatchingFieldView.Width = point.X + stringSize.Width + 10;
                    }
                    if (point.Y + stringSize.Height > pictureBoxMatchingFieldView.Height)
                    {
                        pictureBoxMatchingFieldView.Height = point.Y + stringSize.Height + 10;
                    }
                }
            }
        }

        //対応列描画
        private void DrawCorrespond(Graphics g, List<DTW_Path> diffpath, string[] original, string[] subject)
        {
            //各値の初期化
            int gridLineWidth = 2;
            Pen p = new Pen(Color.Black, gridLineWidth);
            List<Point> originalP = new List<Point>();
            List<Point> subjectP = new List<Point>();
            //int gridWidth = 50;
            //int offset = 10;
            //int secondOffset = 5;
            Font fnt = new Font("MS ゴシック", 20);
            Size stringSize = TextRenderer.MeasureText(g, "MMM", fnt);
            Point offSetPoint = new Point(10, 10);
            //比較元の描画
            for (int i = 0; i < original.Length; i++)
            {
                Point temp = new Point();
                temp.X = i * stringSize.Width + offSetPoint.X;
                temp.Y = offSetPoint.Y;

                originalP.Add(temp);

                g.DrawString(original[i], fnt, Brushes.Black, temp);
                //pictureBoxの調整
                if (temp.X + stringSize.Width > pictureBoxCorrView.Width)
                {
                    pictureBoxCorrView.Width = temp.X + stringSize.Width + 10;
                }
            }
            //比較先の描画
            for (int i = 0; i < subject.Length; i++)
            {
                Point temp = new Point();
                temp.X = i * stringSize.Width + offSetPoint.X;
                temp.Y = offSetPoint.Y + 150;

                subjectP.Add(temp);
                g.DrawString(subject[i], fnt, Brushes.Black, temp);

                //pictureBoxの調整
                if (temp.X + stringSize.Width > pictureBoxCorrView.Width)
                {
                    pictureBoxCorrView.Width = temp.X + stringSize.Width + 10;
                }
            }
            foreach (DTW_Path d in diffpath)
            {
                Point originalTemp = new Point(originalP[d.Original].X + TextRenderer.MeasureText(original[d.Original], fnt).Width / 2
                    , originalP[d.Original].Y + TextRenderer.MeasureText(original[d.Original], fnt).Height);
                Point subjectTemp = new Point(subjectP[d.Subject].X + TextRenderer.MeasureText(subject[d.Subject], fnt).Width / 2
                    , subjectP[d.Subject].Y);

                if (d.Match)
                {
                    g.DrawLine(p, originalTemp, subjectTemp);
                }
                else
                {
                    Pen p2 = new Pen(Color.Red, gridLineWidth);
                    g.DrawLine(p2, originalTemp, subjectTemp);
                }
            }
            
        }
        //private void DrawCorrespond(Graphics g, double[,] matchingField, List<DTW_Path> diffpath, string[] original, string[] subject)
        //{
        //    //各値の初期化
        //    int gridLineWidth = 2;
        //    Pen p = new Pen(Color.Black, gridLineWidth);
        //    List<Point> originalP = new List<Point>();
        //    List<Point> subjectP = new List<Point>();
        //    //int gridWidth = 50;
        //    //int offset = 10;
        //    //int secondOffset = 5;
        //    Font fnt = new Font("MS ゴシック", 20);
        //    Font smal = new Font("MS ゴシック", 10);
        //    Size stringSize = TextRenderer.MeasureText(g, "MMM", fnt);
        //    Point offSetPoint = new Point(10, 10);
        //    //比較元の描画
        //    for (int i = 0; i < original.Length; i++)
        //    {
        //        Point temp = new Point();
        //        temp.X = i * stringSize.Width + offSetPoint.X;
        //        temp.Y = offSetPoint.Y;

        //        originalP.Add(temp);

        //        TextRenderer.DrawText(g, original[i], fnt, temp, Color.Black);
        //        Size s = TextRenderer.MeasureText(original[i], fnt);
                

        //        //g.DrawString(original[i], fnt, Brushes.Black, temp);
        //    }
        //    //比較先の描画
        //    for (int i = 0; i < subject.Length; i++)
        //    {
        //        Point temp = new Point();
        //        temp.X = i * stringSize.Width + offSetPoint.X;
        //        temp.Y = offSetPoint.Y + 150;

        //        subjectP.Add(temp);
        //        TextRenderer.DrawText(g, subject[i], fnt, temp, Color.Black);
        //        //g.DrawString(subject[i], fnt, Brushes.Black, temp);
        //    }
        //    foreach (DTW_Path d in diffpath)
        //    {
        //        Point originalTemp = new Point(originalP[d.correspond.X].X + TextRenderer.MeasureText(original[d.correspond.X], fnt).Width / 2
        //            , originalP[d.correspond.X].Y + TextRenderer.MeasureText(original[d.correspond.X], fnt).Height);
        //        Point subjectTemp = new Point(subjectP[d.correspond.Y].X + TextRenderer.MeasureText(subject[d.correspond.Y], fnt).Width / 2
        //            , subjectP[d.correspond.Y].Y);
        //        if (d.match)
        //        {
        //            g.DrawLine(p, originalTemp, subjectTemp);
        //        }
        //        else
        //        {
        //            Pen p2 = new Pen(Color.Red, gridLineWidth);
        //            g.DrawLine(p2, originalTemp, subjectTemp);
        //        }
        //    }
        //}
        
        #endregion

        
        #region メソッド

        private string[] IntToString(int[] inputArray)
        {
            string[] result = new string[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                result[i] = inputArray[i].ToString();
            }
            return result;
        }
        #endregion
    }
}
