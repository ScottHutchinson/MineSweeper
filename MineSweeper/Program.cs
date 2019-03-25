using System;

namespace MineSweeper {

    class Program {
        static void Main(string[] args) { // TODO: parse args for rows, columns, mines.
            int rows = 3;
            int cols = 3;
            int numMines = 2;
            Game game = new Game(rows, cols, numMines);
            Grid grid = new Grid(game);
            grid.Draw();
            Console.WriteLine("");
            Console.WriteLine("Mark (m), Uncover (u), or Quit (q)");
            Console.ReadLine();
        }
    }
}
