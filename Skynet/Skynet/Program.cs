using System;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    static void Main(string[] args) {
        var road = int.Parse(Console.ReadLine()); // the length of the road before the gap.
        var gap = int.Parse(Console.ReadLine()); // the length of the gap.
        var platform = int.Parse(Console.ReadLine()); // the length of the landing platform.

        var minSpeed = gap + 1;
        var slowAt = road + gap + (platform - Sum(minSpeed, 0) + 1);

        var instruction = Instruction.WAIT;
        // game loop
        var jumped = false;
        while (true) {
            var speed = int.Parse(Console.ReadLine()); // the motorbike's speed.
            var coordX = int.Parse(Console.ReadLine()); // the position on the road of the motorbike.


            if (speed < minSpeed && !jumped) {
                instruction = Instruction.SPEED;
            }
            if (speed > minSpeed && !jumped) {
                instruction = Instruction.SLOW;
            }
            if (speed == minSpeed && !jumped) {
                instruction = Instruction.WAIT;
            }
            if (jumped) {
                instruction = Instruction.WAIT;
            }
            if (coordX >= slowAt) {
                instruction = Instruction.SLOW;
            }
            if (road - 1 == coordX && !jumped) {
                instruction = Instruction.JUMP;
                jumped = true;
            }

            Console.WriteLine(instruction.ToString()); // A single line containing one of 4 keywords: SPEED, SLOW, JUMP, WAIT.
        }
    }

    public enum Instruction {
        SPEED,
        SLOW,
        JUMP,
        WAIT
    }

    static int Sum(int initalSpeed, int reqSpeed) {
        var sum = 0;
        if (initalSpeed > reqSpeed) {
            for (var i = initalSpeed; i > reqSpeed; i--) {
                sum += i;
            }
        }
        if (initalSpeed <= reqSpeed) {
            for (var i = initalSpeed; i < reqSpeed; i++) {
                sum += i;
            }
        }
        return sum;
    }
}