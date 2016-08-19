using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Solution {
    static void Main(string[] args) {
        var LON = double.Parse(Console.ReadLine().Replace(",","."));
        var LAT = double.Parse(Console.ReadLine().Replace(",", "."));
        var N = int.Parse(Console.ReadLine());

        var defibs = new List<Defib>(N);
        for (var i = 0; i < N; i++) {
            var defib = new Defib(Console.ReadLine());
            defib.CalcDistance(LON, LAT);
            defibs.Add(defib);
        }

        defibs.Sort((x, y) => x.Distance.CompareTo(y.Distance));
        Console.WriteLine(defibs.First().Name);
    }

    public class Defib {
        public string Number;

        public Defib(string str) {
            var strings = str.Split(new[] {";"}, StringSplitOptions.None);
            Number = strings[0];
            Name = strings[1];
            Address = strings[2];
            Phone = strings[3];
            Long = double.Parse(strings[4].Replace(",", "."));
            Lat = double.Parse(strings[5].Replace(",","."));
        }

        public double Lat { get; set; }

        public double Long { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public void CalcDistance(double lon, double lat) {
            var dlon = lon - Long;
            var dlat = lat - Lat;
            var s = Math.Sin(dlat/2) * Math.Sin(dlat / 2);
            var dd = Math.Sin(dlon/2) * Math.Sin(dlon / 2);
            var a = s + Math.Cos(lat) * Math.Cos(Lat) * dd;
            var c = 2*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            Distance = 6371*c;
        }

        public double Distance;
    }
}