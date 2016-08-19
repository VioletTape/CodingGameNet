using System;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var N = int.Parse(Console.ReadLine());
        var horses = new List<int>();
        for (var i = 0; i < N; i++) {
            var pi = int.Parse(Console.ReadLine());
            horses.Add(pi);
        }

        horses.Sort();
        var min = int.MaxValue;

        for (var i = 0; i < horses.Count - 1; i++) {
            min = Math.Min(min, (horses[i + 1] - horses[i]));
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(min);
    }
}