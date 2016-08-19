using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var words = new List<Word>();
        var N = int.Parse(Console.ReadLine());
        for (var i = 0; i < N; i++) {
            words.Add(new Word(Console.ReadLine()));
        }

        Console.ReadLine().ToList().ForEach(c => words.ForEach(w => w.Match(c)));

        var result = words.Where(w => w.match.Trim() == "").OrderByDescending(w => w.score);
        Console.WriteLine(result.First().word);
    }

    class Word {
        public int score = 0;
        public readonly string word;
        public string match;

        private readonly Dictionary<int, List<char>> points = new Dictionary<int, List<char>> {
                                            {1, new List<char> {'e', 'a', 'i', 'o', 'n', 'r', 't', 'l', 's', 'u'}},
                                            {2, new List<char> {'d', 'g'}},
                                            {3, new List<char> {'b', 'c', 'm', 'p'}},
                                            {4, new List<char> {'f', 'h', 'v', 'w', 'y'}},
                                            {5, new List<char> {'k'}},
                                            {8, new List<char> {'j', 'x'}},
                                            {10, new List<char> {'q', 'z'}}
                                        };

        public Word(string word) {
            this.word = word;
            match = word;
            score = CalculatePoints();
        }

        private int CalculatePoints() {
            return word.Aggregate(0, (p, c) => p + points.Where(kv => kv.Value.Contains(c)).Select(kv => kv.Key).First());
        }

        public void Match(char c) {
            var idx = match.IndexOf(c);
            if(idx >= 0)
                match = match.Remove(idx, 1);
        }
    }
}