using System;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/

class Player {
    static void Main(string[] args) {
        var width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        var height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis

        var field = new Field(width, height);

        for (var i = 0; i < height; i++) {
            var line = Console.ReadLine(); // width characters, each either 0 or .
            field.ParseLine(line, i);
        }

        foreach (var node in field.GetNextNode()) {
            Console.WriteLine(node.Neigboors());
        }
    }

    class Field {
        private readonly int width;
        private readonly int height;
        private readonly Node[,] nodes;


        public Field(int width, int height) {
            this.width = width;
            this.height = height;

            nodes = new Node[width + 1, height + 1];
        }

        public void ParseLine(string line, int row) {
            var prevRight = new Node();
            for (var i = line.Length - 1; i >= 0; i--) {
                if (line[i] == '0') {
                    var node = new Node(i, row);
                    nodes[i, row] = node;

                    if (!prevRight.IsEmpty) {
                        node.Left = prevRight;
                    }

                    for (var j = row-1; j >= 0; j--) {
                        if (nodes[i, j] != null) {
                            nodes[i, j].Bottom = node;
                            break;
                        }
                    }

                    node.IsEmpty = false;
                    prevRight = node;
                }
            }
        }

        public IEnumerable<Node> GetNextNode() {
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    if (nodes[i, j] != null)
                        yield return nodes[i, j];
                }
            }
        }
    }

    class Node {
        public int X;
        public int Y;
        public bool IsEmpty = true;

        public Node Left;
        public Node Bottom;

        public Node(int x = -1, int y = -1) {
            X = x;
            Y = y;
        }

        public virtual string Neigboors() {
            Console.Error.WriteLine("this {0} left {1} bottom {2}", this, Left ?? new Node(), Bottom ?? new Node());
            return string.Format("{0} {1} {2}", this, Left ?? new Node(), Bottom ?? new Node());
        }

        public override string ToString() {
            return string.Format("{0} {1}", X, Y);
        }
    }
}