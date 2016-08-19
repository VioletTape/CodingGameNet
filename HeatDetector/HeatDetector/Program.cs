using System;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    static void Main(string[] args) {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        var W = int.Parse(inputs[0]); // width of the building.
        var H = int.Parse(inputs[1]); // height of the building.
        var N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        var X0 = int.Parse(inputs[0]);
        var Y0 = int.Parse(inputs[1]);

        Console.Error.WriteLine("Building w:{0}  h:{1}", W, H);
        Console.Error.WriteLine("Number of turns {0}", N);
        Console.Error.WriteLine("Start at w:{0}  h:{1}", X0, Y0);

        var x = X0;
        var y = Y0;

        var right = W;
        var left = 0;
        var bottom = H;
        var top = 0;
        // game loop
        while (true) {
            var bombdir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
            Console.Error.WriteLine("Bomb direction {0}", bombdir);

            for (var i = 0; i < bombdir.Length; i++) {
                switch (bombdir[i].ToString()) {
                    case "D":
                        if (y > top)
                            top = (y);
                        y += (int) Math.Round((bottom - y)/2.0f, MidpointRounding.AwayFromZero);

                        break;
                    case "R":
                        if (x > left)
                            left = (x);
                        x += (int) Math.Round((right - x)/2.0f, MidpointRounding.AwayFromZero);

                        break;
                    case "U":
                        if (y < bottom)
                            bottom -= bottom - y;
                        y -= (int) Math.Round((y - top)/2.0f, MidpointRounding.AwayFromZero);

                        break;
                    case "L":
                        if (x < right)
                            right -= right - x;
                        x -= (int) Math.Round((x - left)/2.0f, MidpointRounding.AwayFromZero);

                        break;
                }
            }
            Console.Error.WriteLine("top:{1} bottom:{0} left:{2} right:{3}", bottom, top, left, right);
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine("{0} {1}", x, y); // the location of the next window Batman should jump to.
        }
    }
}