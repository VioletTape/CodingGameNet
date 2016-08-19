using System;
using System.Text;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var message = Console.ReadLine().Trim();

        var result = new StringBuilder();
        var m = new StringBuilder();
        for (var i = 0; i < message.Length; i++) {
            var value = Convert.ToString(char.ConvertToUtf32(message, i), 2);
            while (value.Length < 7) {
                value = "0" + value;
            }
            m.Append(value);
        }

        var binary = m.ToString();
        Console.Error.WriteLine(binary);

        var prev = binary[0];
        var elements = 1;
        var index = 1;
        while (index < binary.Length) {
            if (prev == binary[index]) {
                index++;
                elements++;
            }
            else {
                var s = prev == '0' ? "00 " : "0 ";
                result.Append(s);
                result.Append('0', elements);
                result.Append(" ");

                prev = binary[index];
                index++;
                elements = 1;
            }
        }

        if (elements > 0) {
            var ss = prev == '0' ? "00 " : "0 ";
            result.Append(ss);
            result.Append('0', elements);
            result.Append("");
        }

        Console.WriteLine(result.ToString());
    }
}