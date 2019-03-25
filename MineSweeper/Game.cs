using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper {

    public static class ListExtensions {
        private static Random rng = new Random();
        // From https://stackoverflow.com/a/1262619/5652483

        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

    enum SquareState {
        Covered,
        Uncovered,
        Marked
    }

    class Square {
        public Square(int row, int col) {
            Row = row;
            Col = col;
            State = SquareState.Covered;
            AdjacentMines = 0;
            isMined = false;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public SquareState State { get; set; }
        public int AdjacentMines { get; set; }
        public bool isMined { get; set; }
    }

    class Game {
        public Game(int rows, int cols, int mines) {
            Rows = rows;
            Cols = cols;
            Squares = new List<Square>();
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Squares.Add(new Square(i, j));
                }
            }
            // Randomly select the specified number of squares that are mined.
            var indices = new List<int>(Enumerable.Range(0, Squares.Count));
            indices.Shuffle();
            for (int i = 0; i < mines; i++) {
                Squares[indices[i]].isMined = true;
            }
            // Count the number of adjacent mines for each square.
            foreach (var sq in Squares) {
                sq.AdjacentMines = AdjacentSquares(sq).Where(s => s.isMined).Count();
            }
        }

        public int Rows { get; set; }
        public int Cols { get; set; }

        public Square SquareAt(int row, int col) {
            return Squares.FirstOrDefault(s => s.Row == row && s.Col == col);
        }

        // From https://github.com/improvingjef/Minesweeper
        private List<Square> AdjacentSquares(Square sq) {
            var minX = sq.Row == 0 ? 0 : sq.Row - 1;
            var minY = sq.Col == 0 ? 0 : sq.Col - 1;
            var maxX = sq.Row == Cols ? Cols : sq.Row + 1;
            var maxY = sq.Col == Rows ? Cols : sq.Col + 1;

            return Squares.Where(
                s => s != sq
                    && s.Row >= minX && s.Row <= maxX
                    && s.Col >= minY && s.Col <= maxY
                ).ToList();
        }

        public List<Square> Squares { get; set; }

    } // Game
} // namespace MineSweeper
