using System;
using static System.Console;
using CustomLinkedList;

namespace ChooChoo
{
    class Program
    {
        //These are class-level arrays.  Usually you don't want to define variables at the class level.  It's okay to define constants at the class
        //level, though.
        //
        //Arrays cannot be defined as constants, though.  Using "static readonly", though, makes them behave like constants.  So this is okay.
        //
        //BTW, these are parallel arrays that define characteristics of train cars.

        static readonly string[] carTypePrompt = new string[8] { "1", "2", "3", "4", "5", "6", "7", "9" };
        static readonly string[] carTypeAbbreviations = new string[8] { "LO", "BX", "CC", "TC", "GC", "PC", "DC", "CA" };
        static readonly string[] carTypes = new string[8] { "Locomotive", "Boxcar", "Cattle car", "Tank car", "Gondola car", "Passenger car", "Dining car", "Caboose" };

        //static int carCount = 100;    //Global variable

        static void Main(string[] args)
        {
            bool done = false;
            string command = "";
            CustomLinkedList<string> railcars = new CustomLinkedList<string>();
            int carCount = 100;


            WriteLine("ChooChooToo Railcar Assembler\n");
            while (!done)
            {
                switch (command = GetCommand().ToUpper())
                {
                    case "Q":
                        WriteLine("\nOkay, we're done.");
                        done = true;
                        break;
                    case "1":
                    case "A":
                        AddRailCar(railcars, ref carCount); //plug in InsertLast();
                        break;
                    case "2":
                    case "M":
                        MoveRailCar(railcars);
                        break;
                    case "3":
                    case "R":
                        RemoveRailCar(railcars);
                        break;
                    case "4":
                    case "L":
                        ListCars(railcars);
                        break;
                    case "5":
                    case "RL":
                        ListCarsReverse(railcars);
                        break;
                    case "6":
                        railcars.Clear();
                        WriteLine("They're gone.  Every last one of them.");
                        break;

                }

            }
        }

        private static void MoveRailCar(CustomLinkedList<string> railcars)
        {
            if (railcars.Count == 0)     //Since our LinkedList throws an exception when the caller attempts to access a node in an empty list,
                                         //we COULD have used a try-catch here.
            {
                WriteLine("Train's empty.  Nothing to move.");
                return;
            }
            bool valid = false;
            string inputCommand;
            string inputCar;
            LinkedListNode<string> car = null;
            LinkedListNode<string> targetCar = null;

            while (!valid)  //Find car to move
            {
                WriteLine("\nMove which of these cars? (or 'x' to cancel)");
                ListCars(railcars);
                Write("==> ");
                inputCommand = ReadLine().ToUpper();
                if (inputCommand == "X")
                {
                    valid = true;
                    WriteLine("Move operation cancelled. \n");
                    return;
                }
                else
                {
                    car = railcars.Find(inputCommand);
                    if (car == null)
                    {
                        WriteLine("No such car.  Try again.");
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            valid = false;
            while (!valid)       //Find where the user wants to move the selected car
            {
                WriteLine("\nWhere do you want to move it?  " +
                    "\n1. Front of train" +
                    "\n2. Behind a specific car" +
                    "\n\nor X to cancel the move.");
                Write("==> ");

                inputCommand = ReadLine().ToUpper();
                if (inputCommand == "X")
                {
                    valid = true;
                    WriteLine("Move operation cancelled. \n");
                    return;
                }
                else
                {
                    if (inputCommand == "1") //Move to front
                    {
                        valid = true;
                        railcars.InsertFirst(railcars.Remove(car));        //Remove the node, then reinsert it at the front, all in one statement.

                        //A more verbose form of the above statement is this:

                        //LinkedListNode<string> removedCar = railcars.Remove(car);
                        //railcars.InsertFirst(removedCar);

                        WriteLine($"{car.Data} moved to front of train.\n");
                        ListCars(railcars);

                    }
                    else if (inputCommand == "2")  //Move after car
                    {
                        Write("Enter car name to move after ==> ");
                        inputCar = ReadLine().ToUpper();
                        targetCar = railcars.Find(inputCar);
                        if (targetCar == null)
                        {
                            WriteLine("No such car.  Try again.");
                        }
                        else if (car == targetCar)
                        {
                            WriteLine("Um...you're trying to move a car relative to itself.  Doesn't make sense.");
                        }
                        else
                        {
                            valid = true;
                            railcars.InsertAfter(railcars.Remove(car), targetCar);  //Same principle as for InsertFirst, above.
                            WriteLine($"{car.Data} moved after {targetCar.Data}.\n");
                            ListCars(railcars);
                        }
                    }
                    else
                    {
                        WriteLine("Invalid command.  Try again.");
                    }
                }
            }
        }


        private static void RemoveRailCar(CustomLinkedList<string> railcars)
        {
            bool valid = false;
            string input = "";

            if (railcars.Count == 0)
            {
                WriteLine("Train's empty.  Nothing to remove.");
                return;
            }

            while (!valid)
            {
                WriteLine("\nDelete which of these cars (or 'x' to cancel)?");
                ListCars(railcars);
                Write("==> ");
                input = ReadLine().ToUpper();
                if (input == "X")
                {
                    valid = true;
                    WriteLine("Remove operation cancelled. \n");
                }
                else
                {
                    LinkedListNode<string> car = railcars.Find(input);
                    if (car == null)
                    {
                        WriteLine("No such car.  Try again.");
                    }
                    else
                    {
                        valid = true;
                        railcars.Remove(car);
                        WriteLine($"{car.Data} removed.\n");
                        ListCars(railcars);
                    }
                }
            }
        }

        private static void ListCars(CustomLinkedList<string> railcars)
        {
            LinkedListNode<string> car = railcars.First;
            if (car == null)
            {
                WriteLine("\nAlas, this train has no cars.\n");
            }
            else
            {
                //below turnary operator address the use of car as singular or plural
                WriteLine($"\nThe train currently has {railcars.Count} {(railcars.Count == 1 ? "car." : "cars")}");
                WriteLine("\n***FRONT***\n");
                do
                {
                    Write($"  {car.Data}");
                    WriteLine($"\t({carTypes[Array.FindIndex(carTypeAbbreviations, s => s == car.Data.Substring(0, 2))]})");
                    car = car.Next;
                } while (car != null);
                WriteLine("\n***BACK***\n");
            }
        }

        private static void ListCarsReverse(CustomLinkedList<string> railcars)
        {
            LinkedListNode<string> car = railcars.Last;
            if (car == null)
            {
                WriteLine("\nAlas, this train has no cars.\n");
            }
            else
            {
                //below turnary operator address the use of car as singular or plural
                WriteLine($"\nThe train currently has {railcars.Count} {(railcars.Count == 1 ? "car." : "cars")}");
                WriteLine("\n***BACK***\n");
                do
                {
                    Write($"  {car.Data}");
                    WriteLine($"\t({carTypes[Array.FindIndex(carTypeAbbreviations, s => s == car.Data.Substring(0, 2))]})");
                    car = car.Prev;
                } while (car != null);
                WriteLine("\n***FRONT***\n");
            }
        }

        //private static void PrintNextCar(LinkedListNode<string> car)  //This method prints after it recurses, so on the way in
        //{
        //    if (car == null) return;

        //    Write($"  {car.Data}");
        //    WriteLine($"\t({carTypes[Array.FindIndex(carTypeAbbreviations, s => s == car.Data.Substring(0, 2))]})");

        //    PrintNextCar(car.Next);
        //}

        private static void AddRailCar(CustomLinkedList<string> railcars, ref int carCount)
        {
            bool valid = false;
            string input = "";
            int index;
            LinkedListNode<string> newCar;
            while (!valid)
            {
                WriteLine("\nSelect a type of car to add: ");
                for (int i = 0; i < carTypePrompt.Length; i++)
                {
                    WriteLine($"- {carTypePrompt[i]} {carTypes[i]} ({carTypeAbbreviations[i]})");
                }
                Write("==> ");
                input = ReadLine();
                index = Array.FindIndex(carTypePrompt, s => s == input);    //See the comment on FindIndex in ListCars.
                if (index == -1)
                {
                    valid = false;
                    WriteLine("Invalid car type.  Again, please.");
                }
                else
                {
                    valid = true;
                    newCar = railcars.InsertLast(new LinkedListNode<string>(carTypeAbbreviations[index] + carCount++));                        //This generates a name for the railcar:
                    //BTW, the postfix incrementer on carCount means that carCount
                    //is not incremented until AFTER the statement concludes.
                    
                    WriteLine($"\n{carTypes[index]} {newCar.Data} successfully added to the front of the train.\n");
                }
            }
        }

        private static string GetCommand()
        {
            bool valid = false;
            string input = "";
            string[] valids = { "1", "2", "3", "4", "5", "Q", "A", "M", "R", "L" };
            while (!valid)
            {
                WriteLine("\nWhat would you like to do?");
                WriteLine("1 - (A)dd a new car");
                WriteLine("2 - (M)ove a car");
                WriteLine("3 - (R)emove a car");
                WriteLine("4 - (L)ist the cars from front to back");
                WriteLine("5 - (RL) List the cars from back to front");
                WriteLine("6 - Remove all cars (clear the list)");
                WriteLine("Q - Quit");
                Write("==> ");
                input = ReadLine();
                if (Array.Find(valids, s => s == input.ToUpper()) != null)
                {
                    valid = true;
                }
                else
                {
                    WriteLine("Say what?\n");
                }
            }
            return input;
        }
    }
}
