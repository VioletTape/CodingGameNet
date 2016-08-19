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
        int N = int.Parse(Console.ReadLine());
        var root = new Number();    
        for (int i = 0; i < N; i++) {
            Console.ReadLine().Aggregate(root, (current, c) => current.TryAdd(c));
        }

        Console.WriteLine(root.Elements()); // The number of elements (referencing a number) stored in the structure.
    }

    class Number {
        private readonly List<Number> next = new List<Number>();
        private char c = '-';

        public Number TryAdd(char c) {
            var number = next.SingleOrDefault(i => i.c == c);
            if (number != null)
                return number;

            number = new Number {c = c};
                next.Add(number);
                return number;
        }

        public int Elements() {
            return next.Count + next.Sum(n => n.Elements());
        }
    }
}