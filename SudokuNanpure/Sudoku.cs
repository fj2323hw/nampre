using System;
using System.Collections.Generic;

namespace SudokuNanpure
{
    public class Sudoku
    {
        private int[,] field = new int[, ]
        {
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 },
            {-1,-1,-1,-1,-1,-1,-1,-1,-1 }
        };

        /// <summary>
        /// 数独ソルバーを返す
        /// </summary>
        public int[,] Field => field;

        /// <summary>
        /// 空きマスを探す
        /// </summary>
        /// <param name="x">空いているx</param>
        /// <param name="y">空いてるy</param>
        /// <returns>空いているものがない場合はfalse</returns>
        public bool FindEmpty(ref int x, ref int y)
        {
            for (x = 0; x < 9; ++x)
            {
                for (y = 0; y < 9; ++y)
                {
                    if (field[x, y] == -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 指定マスに入る候補を返す
        /// </summary>
        /// <param name="x">指定のx座標</param>
        /// <param name="y">指定のy座標</param>
        /// <returns>選択肢</returns>
        public List<int> FindChoices(int x, int y)
        {
            List<int> canUse = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // 行を見て選択肢を絞る
            for (int i = 0; i < 9; ++i) 
            {
                if (field[x, i] != -1) 
                {
                    canUse.Remove(field[x, i]); 
                }
            }

            // 列を見て選択肢を絞る
            for (int i = 0; i < 9; ++i)
            {
                if (field[i, y] != -1)
                {
                    canUse.Remove(field[i, y]);
                }
            }

            // 3*3 ブロックの中で選択肢を絞る
            // cx,cyは中央マスとなるよう計算
            int cx = x / 3 * 3 + 1, cy = y / 3 * 3 + 1;
            for (int i = cx - 1; i <= cx + 1; ++i)
            {
                for (int j = cy - 1; j <= cy + 1; ++j)
                {
                    if(field[i, j] != -1)
                    {
                        canUse.Remove(field[i, j]);
                    }
                }
            }

            return canUse;
        }

        /// <summary>
        /// 数値valを指定座標(x,y)に入れる
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="val">数値</param>
        public void Put(int x,int y, int val)
        {
            field[x, y] = val;
        }

        /// <summary>
        /// 指定座標(x,y)を空白(-1)にする
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        public void Reset(int x, int y)
        {
            field[x, y] = -1;
        }

        public void Dfs(ref Sudoku board, ref int[,] res)
        {
            // 空きマスの座標を表す
            int x = 0, y = 0;

            // 空白を確認　なければ解としてリターン
            if (!board.FindEmpty(ref x, ref y))
            {
                // 解に追加
                Array.Copy(board.field, res, board.field.Length);
                return;
            }

            // 選択肢を確認
            var canUse = board.FindChoices(x, y);

            // バックトラッキング
            foreach(var val in canUse)
            {
                board.Put(x, y, val);
                Dfs(ref board, ref res);
                board.Reset(x, y);
            }
        }
    }
}
