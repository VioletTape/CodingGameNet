using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string temps = Console.ReadLine(); // the n temperatures expressed as integers ranging from -273 to 5526

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        var numbers = temps.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Take(n).ToList().ConvertAll(int.Parse);

        var positives = numbers.Where(i => i >= 0).ToList();
        int? min = null;
        if (positives.Any()) {
            min = positives.Min();
        }

        var negaties = numbers.Where(i => i < 0);
        int? max = null;
        if(negaties.Any())
            max = negaties.Max();

        int? res = 0;
        if (min.HasValue && max.HasValue) {
           res = min.Value > Math.Abs(max.Value) ? max : min;
        }

        if (min.HasValue && !max.HasValue) {
            res = min;
        }
        if (!min.HasValue && max.HasValue) {
            res = max;
        }

        Console.WriteLine(res.HasValue ? res.Value.ToString() : "");
    }
}