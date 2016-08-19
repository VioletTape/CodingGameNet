using System;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    static void Main(string[] args) {
        string[] inputs;
        var surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        for (var i = 0; i < surfaceN; i++) {
            inputs = Console.ReadLine().Split(' ');
            var landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            var landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
        }

        // game loop
        while (true) {
            inputs = Console.ReadLine().Split(' ');
            var X = int.Parse(inputs[0]);
            var Y = int.Parse(inputs[1]);
            var hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            var vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            var fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            var rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            var power = int.Parse(inputs[6]); // the thrust power (0 to 4).

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            // if (-10 < vSpeed && Y > 101) {
            //     power = 0;
            // }
            // if (-19 < vSpeed && vSpeed < -11 && Y > 101) {
            //     power = 1;
            // }
            // if (-29 < vSpeed && vSpeed < -20 && Y > 101) {
            //     power = 2;
            // }
            // if (-35 < vSpeed && vSpeed < -30 && Y > 101) {
            //     power = 3;
            // }
            // if (-999 < vSpeed && vSpeed < -39 && Y > 101) {
            //     power = 4;
            // }
            // if (Y < 101)
            //     power = 0;
        
            if(vSpeed <= -45)
                power = 4;

        Console.WriteLine("{0} {1}", 0, power); // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
    }
}

}