using System;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var n = int.Parse(Console.ReadLine());
        var inputs = Console.ReadLine().Split(' ');

        var max = int.MinValue;
        var min = int.MaxValue;
        var loss = 0;

        for (var i = 0; i < n; i++) {
            var v = int.Parse(inputs[i]);
            if (v >= max) {
                if (i > 1)
                    loss = min - max < loss ? min - max : loss;
                max = v;
                min = v;
            }
            if (min > v) {
                min = v;
            }
        }
        loss = min - max < loss ? min - max : loss;

        Console.WriteLine(loss);
    }
}