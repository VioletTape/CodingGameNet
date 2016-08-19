using System;
using System.Collections.Generic;
using System.Linq;

/**
 * The machines are gaining ground. Time to show them what we're really made of...
 **/

class Player {
    static void Main(string[] args) {
        var width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        var height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        var field = new Field(width, height);

        for (var i = 0; i < height; i++) {
            var line = Console.ReadLine(); // width characters, each either a number or a '.'
            field.Parse(line, i);
        }

        field.BuildTopBottomLinks();
        field.ControlNumber();

        foreach (var res in field.GetLink()) {
            field.ControlNumber();
        }
    }

    class Field {
        private readonly int width;
        private readonly int height;
        private readonly Node[,] field;
        private readonly List<Node> nodes = new List<Node>();

        public Field(int width, int height) {
            this.width = width;
            this.height = height;

            field = new Node[width, height];
        }

        public void Parse(string str, int row) {
            Node prevNode = null;
            for (var i = str.Length - 1; i >= 0; i--) {
                var n = int.Parse(str[i].ToString().Replace(".", "0"));
                if (n > 0) {
                    var node = new Node {
                                            X = i,
                                            Y = row,
                                            N = n
                                        };
                    if (prevNode != null)
                        node.AddLeft(prevNode);
                    prevNode = node;

                    nodes.Add(node);
                    field[i, row] = node;
                }
            }
        }

        public void BuildTopBottomLinks() {
            for (var x = 0; x < width; x++) {
                Node prevNode = null;
                for (var y = 0; y < height; y++) {
                    if (field[x, y] != null) {
                        if (prevNode == null) {
                            prevNode = field[x, y];
                        }
                        else {
                            field[x, y].AddTop(prevNode);
                            prevNode = field[x, y];
                        }
                    }
                }
            }
        }

        public void ControlNumber() {
            Console.Error.WriteLine("Total Nodes " + nodes.Count);
            nodes.ForEach(i => Console.Error.WriteLine("N({2}) at ({0},{1}) Left({3}) Top({4}) Right({5}) Bottom({6})",
                                                       i.X, i.Y, i.N, i.Left ?? new Empty(), i.Top ?? new Empty(), i.Right ?? new Empty(), i.Bottom ?? new Empty()));
        }

        public IEnumerable<string> GetLink() {
            Func<string, bool> answerNotEmpty = answer => !string.IsNullOrEmpty(answer);

            while (nodes.Any()) {
                var skip = false;

                foreach (var answer in FindMaxNodes().Where(answerNotEmpty)) {
                    skip = true;
                    Console.WriteLine(answer);
                }

                if (!skip)
                    foreach (var answer in FindFullConnected().Where(answerNotEmpty)) {
                        skip = true;
                        Console.WriteLine(answer);
                    }

                if (!skip)
                    foreach (var answer in FindSingleConnected().Where(answerNotEmpty)) {
                        skip = true;
                        Console.WriteLine(answer);
                    }

                if (!skip)
                    foreach (var answer in FindSafeConnected().Where(answerNotEmpty)) {
                        skip = true;
                        Console.WriteLine(answer);
                    }

                if(!skip)
                    foreach (var answer in FindAnyNotConnected().Where(answerNotEmpty)) {
                        Console.WriteLine(answer);
                    }


                var enumerable = nodes.Where(n => n.N == 0).Select(n => n).ToList();
                foreach (var sub in enumerable) {
                    nodes.ForEach(n => n.Remove(sub));
                    nodes.Remove(sub);
                }
                ControlNumber();
            }
            yield return "";
        }

        private IEnumerable<string> FindAnyNotConnected() {
            var node = nodes.Single(n => n.Nodes().Count > 0);
            var sub = node.Nodes().First();

            sub.N -= 1;
            node.N -= 1;

            yield return Format(node, sub, 1);
        }

        private IEnumerable<string> FindSingleConnected() {
            var singles = nodes.Where(n => n.Connections() == 1).Select(n => n).ToList();
            if (!singles.Any()) {
                yield return "";
            }

            foreach (var node in singles) {
                if (node.N == 0) {
                    continue;
                }
                var nextNode = node.Nodes().SingleOrDefault();
                if (nextNode == null)
                    continue;

                var res = Format(node, nextNode, node.N);

                nextNode.N -= node.N;
                node.N = 0;
                nextNode.Remove(node);
                nodes.Remove(node);
                yield return res;
            }
        }

        private IEnumerable<string> FindFullConnected() {
            var fulls = nodes.Where(n => n.N/2 == n.Connections()).Select(n => n).ToList();
            if (!fulls.Any()) {
                yield return "";
            }

            foreach (var node in fulls) {
                if (node.N == 0) {
                    continue;
                }
                foreach (var sub  in node.Nodes()) {
                    if (sub.N == 0)
                        continue;

                    sub.N -= 2;
                    yield return Format(node, sub, 2);
                }

                foreach (var sub in node.Nodes().Where(n => n.N == 0)) {
                    nodes.ForEach(n => n.Remove(sub));
                    nodes.Remove(sub);
                }
                node.N = 0;
                nodes.ForEach(n => n.Remove(node));
                nodes.Remove(node);
            }
        }

        private IEnumerable<string> FindSafeConnected() {
            var safes = nodes.Where(n => n.Connections() == n.N).Select(n => n).ToList();
            if (!safes.Any()) {
                yield return "";
            }

            foreach (var node in safes) {
                if (node.N == 0) {
                    continue;
                }

                foreach (var sub in node.Nodes()) {
                    sub.N -= 1;
                    node.N -= 1;
                    yield return Format(node, sub, 1);
                }
                if (node.N == 0) {
                    nodes.ForEach(n => n.Remove(node));
                    nodes.Remove(node);
                }
            }
        }

        private IEnumerable<string> FindMaxNodes() {
            var list = nodes.Where(n => n.N >= 7).Select(n => n).ToList();
            if (!list.Any())
                yield return "";

            foreach (var node in list) {
                if (node.N == 0) {
                    continue;
                }
                foreach (var sub in node.Nodes()) {
                    var subN = sub.N == 1 ? 1 : 2;
                    var nodeN = node.N == 1 ? 1 : 2;
                    var N = Math.Min(subN, nodeN);
                    sub.N -= N;
                    node.N -= N;
                    yield return Format(node, sub, N);
                }

                node.N = 0;
                nodes.ForEach(n => n.Remove(node));
                nodes.Remove(node);
            }
        }

        private static string Format(Node node, Node nextNode, int n) {
            return string.Format("{0} {1} {2}",
                                 node.Coord(),
                                 nextNode.Coord(),
                                 n);
        }
    }

    class Empty : Node {
        public override string ToString() {
            return "-";
        }
    }

    class Node {
        public Node Left;
        public Node Right;
        public Node Top;
        public Node Bottom;

        public int X;
        public int Y;

        public int N;

        public int Connections() {
            return Nodes().Count(i => i != null && i.N > 0);
        }

        public int NodeSum() {
            return Nodes().Sum(n => n.N);
        }

        public List<Node> Nodes() {
            return new List<Node> {Left, Right, Top, Bottom}.Where(n => n != null && n.N > 0).Select(n => n).ToList();
        }

        public void AddLeft(Node node) {
            Right = node;
            node.Left = this;
        }

        public void AddTop(Node node) {
            Top = node;
            node.Bottom = this;
        }

        public override string ToString() {
            return N.ToString();
        }

        public string Coord() {
            return X + " " + Y;
        }

        public void Remove(Node node) {
            if (node == Left)
                Left = null;
            if (node == Right)
                Right = null;
            if (node == Top)
                Top = null;
            if (node == Bottom)
                Bottom = null;
        }
    }
}