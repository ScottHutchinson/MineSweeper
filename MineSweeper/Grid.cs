using System;
using System.Text;

namespace MineSweeper {

    public static class StringExtensions {
        public static string Repeat(this string s, int n)
            => new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
    }

    class Grid {
        public Grid(Game game) {
            Game = game;
        }

        public Game Game { get; set; }

        private string GetSymbol(Square sq) {
            string ret = " ";
            switch (sq.State) {
                case SquareState.Covered:
                    ret = " ";
                    break;
                case SquareState.Uncovered:
                    ret = $"{sq.AdjacentMines}";
                    break;
                case SquareState.Marked:
                    ret = ">";
                    break;
            }
            return ret;
        }

        public void Draw() {
            string heading = "   ";
            for (int j = 0; j < Game.Cols; j++) {
                heading += $" {j + 1}  ";
            }
            Console.WriteLine(heading);
            for (int i = 0; i < Game.Rows; i++) {
                string rowText = $"{i + 1} |";
                for (int j = 0; j < Game.Cols; j++) {
                    string symbol = GetSymbol(Game.SquareAt(i, j));
                    rowText += " " + symbol + " |";
                }
                Console.WriteLine(rowText);
            }
        }
    }
}
