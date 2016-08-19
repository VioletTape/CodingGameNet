using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    private static int width;
    private static int height;

    static void Main(string[] args) {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        width = int.Parse(inputs[0]);
        height = int.Parse(inputs[1]);


//        var debug = new List<string> {"0 3 0", "0 3 0", "0 3 0"};
//        width = 3;
//        height = 3;

//        var debug = new List<string> {
//                                         "0 3 0 0 0 0 0 0", //8
//                                         "0 11 2 2 2 2 13 0",
//                                         "0 0 0 0 0 0 3 0",
//                                         "0 12 2 2 2 2 10 0"
//                                     };
//        width = 8;
//        height = debug.Count;

        var debug = new List<string> {
                                         "0 0 0 0 0 3", // 6
                                         "8 2 2 2 2 10",
                                         "3 0 0 0 12 13",
                                         "11 2 2 2 1 10",
                                         "2 13 0 0 3 0",
                                         "0 7 2 2 4 13",
                                         "0 3 0 12 4 10",
                                         "0 11 2 5 10 0"
                                     };

        debug = new List<string> {
                                     "3 12 8 6 2 2 8 2 9 0 0 0 0", //13
                                     "11 5 10 0 0 0 3 0 3 0 0 0 0",
                                     "0 11 2 2 2 2 6 2 1 2 2 13 0",
                                     "0 0 0 0 0 12 8 2 1 2 2 9 0",
                                     "0 0 12 2 2 1 4 2 10 0 0 11 13",
                                     "0 0 3 0 0 7 9 0 0 0 0 0 3",
                                     "0 0 11 2 2 10 11 2 2 2 2 2 9",
                                     "0 12 8 2 2 2 2 8 2 2 2 2 10",
                                     "0 11 4 2 2 2 2 10 12 13 12 13 0",
                                     "0 0 3 12 8 8 13 12 4 5 5 10 0"
                                 };

//        width = 13;
//        height = debug.Count;

        var map = new Cell[width, height];
        var factory = new CellFactory();


        for (var row = 0; row < height; row++) {
            var line = Console.ReadLine();
            // var line = debug[row];
            var col = 0;
            foreach (var type in line.Split(' ').ToList().ConvertAll(int.Parse)) {
                map[col, row] = factory.Get(type).SetCoord(col, row);
                col++;
            }

            Console.Error.WriteLine(line);
        }
        var EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        BuildPaths(map);


        Cell indie = null;
        // game loop
        while (true) {
            inputs = Console.ReadLine().Split(' ');
            var XI = int.Parse(inputs[0]);
            var YI = int.Parse(inputs[1]);
            var POS = inputs[2];
            Console.Error.WriteLine(XI + " " + YI + " " + POS);

            if (indie == null)
                indie = map[XI, YI].Nexts.First();

            Console.WriteLine("{0} {1}", indie.X, indie.Y); // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn.
            indie = indie.Nexts.First();
        }
    }

    private static void BuildPaths(Cell[,] map) {
        var starts = new List<Cell>();
        for (var col = 0; col < width; col++) {
            var cell = map[col, 0];
            if (cell.Passages.Any(p => p.From == Side.T)) {
                cell.Passages.RemoveAll(p => p.From != Side.T);
                cell.Processed = true;
                starts.Add(cell);
            }
        }

        foreach (var start in starts) {
            Process(start, map);
        }
    }

    private static bool Process(Cell cell, Cell[,] fromMap) {
        cell.Processed = true;

        var cells = GetClosestTo(cell, fromMap);
        if (cells.Count == 0) {
            return cell.Y == height - 1 && cell.Passages.Any(p => p.To == Side.T);
        }

        foreach (var passage in cell.Passages.ToList()) {
            var fits = cells.Where(c => c.Passages.Any(p => p.FitsTo(passage))).Select(c => c).ToList();
            if (fits.Any()) {
                foreach (var next in fits) {
                    if (fits.Count > 1) {
                        next.Passages.RemoveAll(p => p.From != next.From);
                        cell.Passages.RemoveAll(p => p.To != next.From);
                    }
                    else {
                        next.Passages.RemoveAll(p => p.From != next.From);
                    }

                    if (Process(next, fromMap)) {
                        cell.Nexts.Add(next);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private static List<Cell> GetClosestTo(Cell cell, Cell[,] fromMap) {
        var x = cell.X;
        var y = cell.Y;
        var cells = new List<Cell>();
        if (x > 0 && cell.Passages.Any(p => p.To == Side.L)) {
            var @from = fromMap[x - 1, y];
            if (@from.Processed) {
                cell.Nexts.Add(@from);
            }
            else {
                @from.From = Side.L;
                cells.Add(@from);
            }
        }

        if (y + 1 < height && cell.Passages.Any(p => p.To == Side.T)) {
            var @from = fromMap[x, y + 1];
            if (@from.Processed) {
                cell.Nexts.Add(@from);
            }
            else {
                @from.From = Side.T;
                cells.Add(@from);
            }
        }
        
        if (x + 1 < width && cell.Passages.Any(p => p.To == Side.R)) {
            var @from = fromMap[x + 1, y];
            if (@from.Processed) {
                cell.Nexts.Add(@from);
            }
            else {
                @from.From = Side.R;
                cells.Add(@from);
            }
        }
        return cells;
    }

    public class CellFactory {
        private readonly List<Cell> map;

        public CellFactory() {
            var cell0 = new Cell();

            var cell1 = new Cell()
                .Add(Side.L, Side.T)
                .Add(Side.R, Side.T)
                .Add(Side.T, Side.T);

            var cell2 = new Cell()
                .Add(Side.L, Side.L)
                .Add(Side.R, Side.R);

            var cell3 = new Cell()
                .Add(Side.T, Side.T);

            var cell4 = new Cell()
                .Add(Side.T, Side.L)
                .Add(Side.L, Side.T);

            var cell5 = new Cell()
                .Add(Side.T, Side.R)
                .Add(Side.R, Side.T);

            var cell6 = new Cell()
                .Add(Side.T, Side.L)
                .Add(Side.T, Side.R)
                .Add(Side.L, Side.L)
                .Add(Side.R, Side.R);

            var cell7 = new Cell()
                .Add(Side.T, Side.T)
                .Add(Side.L, Side.T);

            var cell8 = new Cell()
                .Add(Side.L, Side.T)
                .Add(Side.R, Side.T);

            var cell9 = new Cell()
                .Add(Side.T, Side.T)
                .Add(Side.R, Side.T);

            var cell10 = new Cell()
                .Add(Side.T, Side.L);

            var cell11 = new Cell()
                .Add(Side.T, Side.R);

            var cell12 = new Cell()
                .Add(Side.L, Side.T);

            var cell13 = new Cell()
                .Add(Side.R, Side.T);

            map = new List<Cell> {cell0, cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13};
        }

        public Cell Get(int type) {
            return map[type].Clone();
        }
    }

    public class Cell {
        public List<Passage> Passages;
        public Side From;
        public bool Processed;
        public List<Cell> Nexts;
        public int X;
        public int Y;

        public Cell() {
            Passages = new List<Passage>();
            Nexts = new List<Cell>();
        }

        public Cell Add(Side @from, Side to) {
            Passages.Add(new Passage(@from, to));
            return this;
        }

        public Cell SetCoord(int x, int y) {
            X = x;
            Y = y;
            return this;
        }


        public Cell Clone() {
            return new Cell {
                                Passages = Passages.ToList()
                            };
        }

        public override string ToString() {
            return string.Format("{0} {1}", X, Y);
        }
    }

    public class Passage {
        public Side From;
        public Side To;

        public Passage(Side @from, Side to) {
            From = @from;
            To = to;
        }

        public bool FitsTo(Passage other) {
            return From == other.To;
        }
    }

    public enum Side {
        T,
        L,
        R
    }
}