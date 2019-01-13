using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    static public class Utils
    {
        static public int[,] Board = new int[10, 10];
        static public Cell[,] arrCellScripts = new Cell[10, 10];

        static public bool isCanSelect = false;

        static public bool isSelected = false;

        static public int RowMove = -1;
        static public int ColMove = -1;

        static public void Swap(int r1, int c1, int r2, int c2)
        {
            int temp = Board[r1, c1];
            Board[r1, c1] = Board[r2, c2];
            Board[r2, c2] = temp;
            arrCellScripts[r1, c1].Reload(Board[r1, c1]);
            arrCellScripts[r2, c2].Reload(Board[r2, c2]);
        }

        /// <summary>
        /// Check if have match 3 or more
        /// </summary>
        /// <returns></returns>
        static public bool CheckMatch()
        {
            bool isMatch = false;

            //Check row
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (Utils.Board[r, c] == Utils.Board[r, c + 1] && Utils.Board[r, c] == Utils.Board[r, c + 2] && Utils.Board[r, c] != -2)
                    {
                        Utils.arrCellScripts[r, c].IsReload = true;
                        Utils.arrCellScripts[r, c + 1].IsReload = true;
                        Utils.arrCellScripts[r, c + 2].IsReload = true;
                        isMatch = true;
                    }
                }
            }

            //Check col
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 8; r++)
                {
                    if (Utils.Board[r, c] == Utils.Board[r + 1, c] && Utils.Board[r, c] == Utils.Board[r + 2, c] && Utils.Board[r, c] != -2)
                    {
                        Utils.arrCellScripts[r, c].IsReload = true;
                        Utils.arrCellScripts[r + 1, c].IsReload = true;
                        Utils.arrCellScripts[r + 2, c].IsReload = true;
                        isMatch = true;
                    }
                }
            }

            return isMatch;
        }

        //Disappear match row or column and random new cell
        static public void Reload()
        {
            foreach (var item in Utils.arrCellScripts)
            {
                if (item.IsReload)
                {
                    item.ResetValue();
                }
            }

            List<int> temp = new List<int>();
            for (int c = 0; c < 10; c++)
            {
                temp.Clear();
                for (int r = 9; r >= 0; r--)
                {
                    if (Utils.Board[r, c] != -1 && Utils.Board[r, c] != -2)
                    {
                        temp.Add(Utils.Board[r, c]);
                    }
                }

                var cnt = 0;
                var rowContinue = 0;
                for (int r = 9; r >= 0; r--)
                {
                    if (Utils.Board[r, c] != -2)
                    {

                        if (Utils.Board[r, c] != temp[cnt])
                        {
                            Utils.Board[r, c] = temp[cnt];
                            Utils.arrCellScripts[r, c].Reload(temp[cnt]);
                        }
                        cnt++;

                        if (cnt == temp.Count)
                        {
                            rowContinue = r;
                            break;
                        }
                    }

                }

                for (int r = rowContinue - 1; r >= 0; r--)
                {
                    if (Utils.Board[r, c] != -2)
                    {
                        var rnd = UnityEngine.Random.Range(0, 5);
                        Utils.Board[r, c] = rnd;
                        Utils.arrCellScripts[r, c].Reload(rnd);
                    }
                }
            }

            foreach (var item in Utils.arrCellScripts)
            {
                item.IsReload = false;
            }
        }
    }
}
