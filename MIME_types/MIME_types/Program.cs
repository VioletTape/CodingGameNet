using System;
using System.Collections.Generic;
using System.IO;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var mime = new Dictionary<string, string>();

        var N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        var Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        for (var i = 0; i < N; i++) {
            var inputs = Console.ReadLine().Split(' ');
            var EXT = inputs[0].ToLower(); // file extension
            var MT = inputs[1]; // MIME type.   

            mime.Add("."+EXT, MT);
        }

        for (var i = 0; i < Q; i++) {
            var FNAME = Console.ReadLine(); // One file name per line.
            var extension = Path.GetExtension(FNAME).ToLower();
            Console.WriteLine(mime.ContainsKey(extension) ? mime[extension] : "UNKNOWN");
        }
    }
}