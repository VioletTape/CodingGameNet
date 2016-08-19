using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

class Player {
    static void Main(string[] args) {
        string[] inputs;
        var lifts = new List<Lift>();

        inputs = Console.ReadLine().Split(' ');
        var nbFloors = int.Parse(inputs[0]); // number of floors
        var width = int.Parse(inputs[1]); // width of the area
        var nbRounds = int.Parse(inputs[2]); // maximum number of rounds
        var exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
        var exitPos = int.Parse(inputs[4]); // position of the exit on its floor
        var nbTotalClones = int.Parse(inputs[5]); // number of generated clones
        var nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
        var nbElevators = int.Parse(inputs[7]); // number of elevators

        Console.Error.WriteLine("number of floors " + nbFloors);
        Console.Error.WriteLine("width of the area " + width);
        Console.Error.WriteLine("maximum number of rounds " + nbRounds);
        Console.Error.WriteLine("floor on which the exit is found " + exitFloor);
        Console.Error.WriteLine("position of the exit on its floor " + exitPos);
        Console.Error.WriteLine("number of generated clones " + nbTotalClones);
        Console.Error.WriteLine("number of elevators " + nbElevators);
        Console.Error.WriteLine(" ");
        lifts.Add(new Lift {
                               Position = exitPos,
                               Floor = exitFloor
                           });


        for (var i = 0; i < nbElevators; i++) {
            inputs = Console.ReadLine().Split(' ');
            var elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
            var elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor

            lifts.Add(new Lift {
                                   Position = elevatorPos,
                                   Floor = elevatorFloor
                               });
        }
        lifts.Sort((x, y) => x.Floor.CompareTo(y.Floor));

        // game loop
        var firstStep = true;
        var block = false;

        while (true) {
            inputs = Console.ReadLine().Split(' ');
            var cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
            var clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
            var direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT

            if (firstStep) {
                firstStep = false;

                var entry = new Lift {
                                         Position = clonePos,
                                         Floor = cloneFloor
                                     };

                lifts = CalcBlockers(lifts, entry, direction.ToUpper().Trim() == "RIGHT");

                Console.WriteLine(lifts.First().ShouldBeBlocked ? "BLOCK" : "WAIT");
                lifts.Remove(lifts.First());
                continue;
            }

            var commandment = block ? "BLOCK" : "WAIT";
            block = false;

            Console.WriteLine(commandment); // action: WAIT or BLOCK

            var blocker = lifts.FirstOrDefault();
            if (blocker != null && blocker.Position == clonePos) {
                block = blocker.ShouldBeBlocked;
                lifts.Remove(blocker);
            }
        }
    }

    public static List<Lift> CalcBlockers(List<Lift> blockers, Lift entry, bool isRight) {
//        var blocker = blockers.FirstOrDefault(b => b.Floor == entry.Floor);
        var result = blockers./*SkipWhile(b => b != blocker)*/Select(n => n).ToList();
        result.Insert(0, entry);

        for (var i = 0; i < result.Count - 1; i++) {
            result[i].ShouldBeBlocked = isRight 
                ? result[i + 1].Position < result[i].Position 
                : result[i + 1].Position > result[i].Position;

            if (result[i].ShouldBeBlocked) {
                isRight = !isRight;
            }
        }
        return result;
    }


    public class Lift {
        public int Floor;
        public int Position;
        public bool ShouldBeBlocked;

        public override string ToString() {
            return Floor + " " + Position + " " + ShouldBeBlocked;
        }
    }
}