using System;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var L = int.Parse(Console.ReadLine());
        var H = int.Parse(Console.ReadLine());
        var T = Console.ReadLine().ToUpper();

        var ascii = new char[L*27, H];

        for (var i = 0; i < H; i++) {
            var ROW = Console.ReadLine();
            Console.Error.WriteLine(ROW);

            for (var j = 0; j < L*27; j++) {
                ascii[j, i] = ROW[j];
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        for (var i = 0; i < H; i++) {
            for (var ci = 0; ci < T.Length; ci++) {
                var c = T[ci] - 65;

                if (c < 0 || c > 26) {
                    c = 26;
                }

                for (var j = c*L; j < (c + 1)*L; j++) {
                    Console.Write(ascii[j, i]);
                }
            }
            Console.WriteLine();
        }
    }
}