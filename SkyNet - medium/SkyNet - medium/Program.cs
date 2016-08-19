using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    static void Main(string[] args) {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        var N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        var L = int.Parse(inputs[1]); // the number of links
        var E = int.Parse(inputs[2]); // the number of exit gateways


        var links = new List<Link>();
        var nodes = new Dictionary<int, Node>();


        for (var i = 0; i < L; i++) {
            inputs = Console.ReadLine().Split(' ');
            var N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            var node1 = new Node(N1);
            if (nodes.ContainsKey(N1)) {
                node1 = nodes[N1];
            }
            else {
                nodes[N1] = node1;
            }

            var N2 = int.Parse(inputs[1]);
            var node2 = new Node(N2);
            if (nodes.ContainsKey(N2)) {
                node2 = nodes[N2];
            }
            else {
                nodes[N2] = node2;
            }

            var link = new Link(node1, node2);
            links.Add(link);
            // Console.Error.WriteLine(node1);
            // Console.Error.WriteLine(node2);
        }

        var gateways = new List<int>();
        //var res = new List<Link>();
        //var gates = new List<Node>();

        for (var i = 0; i < E; i++) {
            var EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            gateways.Add(EI);
            nodes[EI].IsGate = true;
            nodes[EI].Links.ForEach(l => l.IsToGateway = true);
            //var collection = links.Where(l => l.A == EI || l.B == EI).Select(l => l).ToList();
            //gates.Add(new Gate(EI,collection));
            //res.AddRange(collection);
        }
        //res.ForEach(l => l.IsToGateway = true);
        var ambush = gateways.Count >= 3;
        // game loop

        var skyMoves = new List<int>();

        var SIPres = -1;
        while (true) {
            var SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

            var max = nodes.Where(n => gateways.Contains(n.Key)).Max(n => n.Value.LinkedNode.Count);
            if (max > 3)
                ambush = true;

            var gate = nodes.FirstOrDefault(g => g.Value.LinkedNode.Count == max && g.Value.IsGate);

            var danger = nodes[SI].Links.FirstOrDefault(i => !i.IsClosed && i.IsToGateway);
            Link first = null;
            if (danger == null && ambush) {
                if (gate.Value.Ambushed && gateways.Count > 1) {
                    gate = nodes.FirstOrDefault(n => n.Value.IsGate && !n.Value.Ambushed);
                    if (gate.Value == null) {
                        gate = nodes.First(n => n.Value.IsGate);
                    }
                }

                var node = gate.Value.LinkedNode.Where(n => n.Links.Count == 3).Select(n => n).ToList();
                do {
                    first = node.First().Links.FirstOrDefault(l => !l.IsCloseTo(gate.Value) && !l.IsClosed);
                    if (first == null) {
                        node.RemoveAt(0);
                    }
                } while (first == null);
                gate.Value.Ambushed = true;
            }
            if (first == null && danger == null) {
                Console.Error.WriteLine("Op1");
                first = nodes.First(n => n.Value.IsGate).Value.Links.FirstOrDefault(l => !l.IsClosed);
            }

            if (first == null) {
                Console.Error.WriteLine("Op2");
                first = danger;
            }


            //var first = gate.Value.Links.FirstOrDefault(l => l.IsCloseTo(skynet) && !l.IsClosed);


            //var warning = links.FirstOrDefault(l => l.IsCloseTo(skynet) && !l.IsClosed && l.IsToGateway);
            // Console.Error.WriteLine(first == null);
            //  Console.Error.WriteLine(warning == null);
            //if (warning != null) {
            //    Console.Error.WriteLine("Warn != null " + warning.Print());
            //    if (nodes[SI].Links.Any(i => i.IsToGateway && !i.IsClosed))
            //        first = warning;
            //}

            //if (first == null) {
            //    if (ambush1) {
            //        if (gate.Value.Ambushed) {
            //            var gg = nodes.Where(i => i.Value.IsGate && !i.Value.Ambushed).Select(i => i);
            //            if (gg.Any()) {
            //                gate = gg.First();
            //                var node = gate.Value.LinkedNode.Where(n => n.Links.Count <= 3).Skip(1).FirstOrDefault();
            //                Console.Error.WriteLine("Node {0}", node.Number);
            //                first = node.Links.Where(l => !l.IsCloseTo(gate.Value) && !l.IsClosed).First();
            //            }
            //        }
            //        else {
            //            var node = gate.Value.LinkedNode.Where(n => n.Links.Count <= 3).FirstOrDefault();
            //            Console.Error.WriteLine("Node {0}", node.Number);
            //            first = node.Links.Where(l => !l.IsCloseTo(gate.Value) && !l.IsClosed).First();
            //        }
            //    }
            //}
            //else {
            //    var count = gate.Value.Links.Count;
            //    Console.Error.WriteLine("Links {0}", count);
            //    first = gate.Value.Links.First(l => !l.IsClosed);
            //}

            if (skyMoves.Contains(SI)) {
                Console.Error.WriteLine("Ambush");
                var amb = nodes[SI].Links.Where(i => !i.IsClosed && (i.A.Number != SIPres || i.B.Number != SIPres)).FirstOrDefault();
                if (amb != null) {
                    first = amb;
                }
            }

            //if(first == null){
            //    first = gate.Value.Links.First(l => !l.IsClosed);
            //}

            first.IsClosed = true;
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            skyMoves.Insert(0, SI);
            skyMoves = skyMoves.Take(5).ToList();
            SIPres = SI;
            Console.WriteLine(first.Print()); // Example: 0 1 are the indices of the nodes you wish to sever the link between
        }
    }

    internal class Node {
        public int Number;
        public List<Node> LinkedNode;
        public List<Link> Links;
        public bool IsGate;

        public Node(int number) {
            Number = number;
            LinkedNode = new List<Node>();
            Links = new List<Link>();
        }

        public bool Ambushed { get; set; }

        public void AddLink(Link link) {
            if (!Links.Contains(link)) {
                Links.Add(link);
            }
        }

        protected bool Equals(Node other) {
            return Number == other.Number;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Node) obj);
        }

        public static bool operator ==(Node a, Node b) {
            var result = false;
            try {
                result = a.Number == b.Number;
            }
            catch (Exception) {}
            return result;
        }

        public static bool operator !=(Node a, Node b) {
            return !(a == b);
        }

        public override int GetHashCode() {
            return Number;
        }

        public override string ToString() {
            return string.Format("{2} = Linked Nodes:{0} Links:{1}", LinkedNode.Count, Links.Count, Number);
        }
    }

    internal class Link {
        public Node A;
        public Node B;
        public bool IsToGateway;
        public bool IsClosed;

        public Link(Node a, Node b) {
            A = a;
            B = b;

            a.LinkedNode.Add(b);
            b.LinkedNode.Add(a);
            a.AddLink(this);
            b.AddLink(this);
        }


        public void SetGetwayIfApply(Node x) {
            IsToGateway = IsCloseTo(x);
        }

        public bool IsCloseTo(Node x) {
            return A == x || B == x;
        }


        public string Print() {
            return string.Format("{0} {1}", A.Number, B.Number);
        }
    }
}