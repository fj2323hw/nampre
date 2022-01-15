using System;
using System.Text;

namespace SudokuNanpure
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 数独の表を作る
            Sudoku board = new Sudoku();

            for (int x = 0; x < 9; ++x)
            {
                string line = Console.ReadLine();
                
                for (int y = 0; y < 9; ++y)
                {
                    // 空マスの場合は何もしない
                    if (string.Equals(line.Substring(y,1), "*"))
                    {
                        continue;
                    }

                    // 数値情報に変換する
                    int val = int.Parse(line.Substring(y, 1));
                    board.Put(x, y, val);
                }
            }

            // 数独を解く
            int[,] results = new int[9, 9];
            board.Dfs(ref board, ref results);

            // 解の出力
            for (int x = 0; x < 9; ++x)
            {
                var sb = new StringBuilder();
                for (int y = 0; y < 9; ++y)
                {
                    sb.Append(results[x, y]);
                }

                Console.WriteLine(sb.ToString());
            }

            // 何かボタンを押すまで待つ
            Console.ReadLine();
        }
    }
}
