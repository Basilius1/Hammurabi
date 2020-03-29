using System;

namespace Hammurabi
{
    class program
    {

        public class Player
        {
            public static void Main(string[] args)
            {
                // How should I make this better?
                //
                // Only few headlines into Main method. For debugging it's easy to comment out method.
                //
                // set up things up. Maybe use static methods. no need to instantiate class?
                //
                // for loop - replace it with CheckOutTenYears
                //
                // Every method inside class Program.
                // variables under class Program
                // Properties inside method. (and methods are inside class Program)
                //
                Player myPlayer = new Player();

                myPlayer.PopulationDefault = 100; // 100
                myPlayer.LandDefault = 1000; // 1000
                myPlayer.FoodDefault = 2800; // 2800
                myPlayer.PriceDefault = 20; // 20

                myPlayer = new Player(1, myPlayer.PopulationDefault, myPlayer.LandDefault, myPlayer.FoodDefault, myPlayer.PriceDefault);

                myPlayer.printPlayer();

                bool winner = true;

                // loop for 10 years
                for (int i = 0; i < 10; i++)
                {
                    myPlayer.getInput(myPlayer);
                    if (myPlayer.checkEndGame(myPlayer))
                    {
                        winner = false;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("GAME OVER");
                        Console.ResetColor();

                        break;
                    }
                    else
                    {
                        myPlayer.printReport();
                        myPlayer.printPlayer();
                    }
                }
                if (winner)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; /////////////////////////////////////////////////////////////////////color
                    Console.WriteLine("You have kept your people alive and 'well' for 10 years!");
                    Console.WriteLine("You are truly great!");
                    Console.WriteLine("CONGRATULATIONS");
                    Console.ResetColor();
                }
            }

            int yearNumber;
            int population;
            int land;
            int food;
            int priceOfLand;
            int starvingNumber;
            int immigrantNumber;
            int plagueNumber;
            int harvestNumber;
            int ratNumber;
            public int PopulationDefault { get; set; }
            public int LandDefault { get; set; }
            public int FoodDefault { get; set; }
            public int PriceDefault { get; set; }

            public Player()
            {

            }
            public Player(int nyearNumber, int npopulation, int nland, int nfood, int npriceOfLand)
            {
                yearNumber = nyearNumber;
                population = npopulation;
                land = nland;
                food = nfood;
                priceOfLand = npriceOfLand;
            }
            //Getter for variables
            public int getYearNumber() { return yearNumber; }
            public int getPopulation() { return population; }
            public int getLand() { return land; }
            public int getFood() { return food; }
            public int getPriceOfLand() { return priceOfLand; }

            public int getStarvingNumber() { return starvingNumber; }
            public int getImmigrantNumber() { return immigrantNumber; }
            public int getPlagueNumber() { return plagueNumber; }
            public int getHarvestNumber() { return harvestNumber; }
            public int getRatNumber() { return ratNumber; }

            //Setter for variables
            public void setYearNumber(int n) { yearNumber = n; }
            public void setPopulation(int n) { population = n; }
            public void setLand(int n) { land = n; }
            public void setFood(int n) { food = n; }
            public void setPriceOfLand(int n) { priceOfLand = n; }

            // Methods to do shit

            public void printPlayer() //print player's variables
            {
                Console.WriteLine("-------------------------------------------"
                    + "\nYear:                      " + yearNumber
                    + "\nAcres of land:             " + land
                    + "\nPopulation:                " + population
                    + "\nStored grain:              " + food
                    + "\nPrice of land:             " + priceOfLand
                    + "\n");
            }

            public void printReport() //Print the report after each turn
            {
                Console.WriteLine("\nO Great Hammurabi!");
                Console.WriteLine($"You are in year {yearNumber} of your ten year rule.");
                Console.WriteLine($"In the previous year {starvingNumber} people starved to death");
                Console.WriteLine($"In the previous year {immigrantNumber} people entered the kingdom.");
                if (plagueNumber > 0)
                {
                    Console.WriteLine("The plague killed half the people.");
                }
                Console.WriteLine($"The population is now {population}");
                Console.WriteLine($"We harvested {harvestNumber} bushels.");
                Console.WriteLine($"Rats destroyed {ratNumber} bushels, leaving {food} bushels in storage.");
                Console.WriteLine($"The city owns {land} acres of land.");
                Console.WriteLine($"Land is currently worth {priceOfLand} bushels per acre.");
            }

            //Each year, there is a 15% chance of a horrible plague.
            //When this happens, half your people die.
            //Return the number of plague deaths

            public int getPlagueDeath()    //calculate how many people die by plague
            {
                var change = new Random();
                int v = change.Next(0, 6);
                if (v == 0)
                {
                    return population / 2;
                }
                else return 0;
            }

            //Each person needs 20 bushels of grain to survive.
            //If you feed them more than this, they are happy, but the grain is still gone. You don't get any benefit from having happy subjects.
            //Return the number of deaths from starvation (possibly zero).

            public int getStarvingDeath(int nFood)  //calculate how many people die by starving
            {
                int numberFeedPeople = nFood / 20;
                if (numberFeedPeople > population)
                {
                    return 0;
                }
                else return population - numberFeedPeople;
            }

            //Return true if more than 45% of the people starve.
            //(This will cause you to be immediately thrown out of office, ending the game.)

            public bool isUprising()  //return true if people want to uprising
            {
                double percentStarving = (double)starvingNumber / population;
                if (percentStarving > 0.45)
                    return true;
                else
                    return false;
            }

            //Nobody will come to the city if people are starving.
            //If everyone is well fed, compute how many people come to the city as:
            //(20 * number of acres you have + amount of grain you have in storage) / (100 * population) + 1.

            public int getImmigrant()  //calculate how many new people enter
            {
                if (getStarvingNumber() > 0)
                {
                    return 0;
                }
                return (20 * land + food) / (100 * population) + 1;
            }

            //Choose a random integer between 1 and 6, inclusive.
            //Each acre that was planted with seed will yield this many bushels of grain.
            //(Example: if you planted 50 acres, and your number is 3, you harvest 150 bushels of grain).
            //Return the number of bushels harvested.

            public int getHarvest(int nSeed)    //calculate how much we harvest
            {
                double landChance = land / 1000.0;
                //double chance = (int)(rand() % (6)) + 1;
                var change = new Random();
                double v = change.Next(1, 7);
                v = v * landChance;
                return (int)(nSeed * v);
            }

            //There is a 40% chance that you will have a rat infestation.
            //When this happens, rats will eat somewhere between 10% and 30% of your grain.
            //Return the amount of grain eaten by rats (possibly zero).

            //public int getEatenByRat()   //calculate how much rat has eaten
            //{
            //    int chance = (int)(rand() % 3); // 0...3 randomi
            //    if (chance == 0)
            //    {
            //        int chanceEatAmount = (int)(rand() % 3) + 1;
            //        int foodEaten = food * ((float)chanceEatAmount / (float)10);
            //        return foodEaten;
            //    }
            //    else return 0;
            //}
            public int getEatenByRat()
            {
                var rnd = new Random();
                int change = rnd.Next(0, 3);
                if (change == 0)
                {
                    int chanceEatAmount = rnd.Next(0, 3) + 1;
                    int foodEaten = food * chanceEatAmount / 10;
                    return foodEaten;
                }
                else return 0;
            }

            //The price of land is random, and ranges from 17 to 23 bushels per acre.
            //Return the new price for the next set of decisions the player has to make.

            public int getNewPrice()   //calculate new price for land
            {
                var rnd = new Random();
                int chance = (rnd.Next(0, 6));
                return chance + 17;
            }
            // Update on every turn
            public bool updatePlayer(int nLand, int nFood, int nSeed)
            {
                //update the remaining stats first, then calculate all the random elements
                food -= nFood;
                //seed -= nSeed;
                land += nLand;

                food -= nLand * priceOfLand;

                starvingNumber = getStarvingDeath(nFood);
                immigrantNumber = getImmigrant();
                plagueNumber = getPlagueDeath();
                harvestNumber = getHarvest(nSeed);

                population -= plagueNumber;
                population -= starvingNumber;

                population += immigrantNumber;

                food += harvestNumber;
                ratNumber = getEatenByRat();
                food -= ratNumber;

                priceOfLand = getNewPrice();

                yearNumber++;

                if (population < 0)
                    population = 0;

                return true;
            }
            // Helper function to get input and validate
            //public void getInput(Player myPlayer)
            //{

            //}
            //public bool checkEndGame(Player myPlayer)
            //{
            //    return true;
            //}

            public int validateIntegerInput()
            {
                while (true)
                {
                    string _input = Console.ReadLine();
                    // insert REAL validity check here
                    int input = Int32.Parse(_input);
                    return input;
                }
            }
            public void getInput(Player myPlayer)
            {
                int nland;
                int nfood;
                int nseed;
                bool invalid = false;
                string doubleCheck;
                string inputLine;
                do
                {
                    int foodRemaining = myPlayer.getFood();
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; /////////////////////////////////////////////////// color
                        Console.Write("How many acres do you wish to buy (Negative to sell)? ");
                        nland = validateIntegerInput();
                        if (myPlayer.getFood() < (nland * myPlayer.getPriceOfLand()))
                        {
                            Console.WriteLine($"You can't buy {nland} acres, you only have {myPlayer.getFood()} bushels!");
                        }
                    } while (myPlayer.getFood() < (nland * myPlayer.getPriceOfLand()));


                    foodRemaining -= nland * myPlayer.getPriceOfLand();
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Green; /////////////////////////////////////////////////// color
                        Console.Write("How many bushels do you wish to feed your people? ");
                        nfood = validateIntegerInput();
                        if (nfood < 0)
                        {
                            Console.WriteLine("You can't feed people a negative amount of food!");
                            invalid = true;
                        }
                        else if (nfood > foodRemaining)
                        {
                            Console.WriteLine($"You can't use {nfood} bushels, you only have {foodRemaining} left!");
                            invalid = true;
                        }
                        else
                        {
                            invalid = false;
                        }
                    } while (invalid);

                    foodRemaining -= nfood;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow; /////////////////////////////////////////////////// color
                        Console.Write("How many acres do you wish to plant with seed? ");
                        nseed = validateIntegerInput();
                        if (nseed < 0)
                        {
                            Console.WriteLine("You can't plant a negative amount of seeds!");
                            invalid = true;
                        }
                        else if (nseed > foodRemaining)
                        {
                            Console.WriteLine($"You can't plant {nfood} seeds, you only have {foodRemaining} left!");
                            invalid = true;
                        }
                        else
                        {
                            invalid = false;
                        }
                    } while (invalid);

                    Console.ResetColor(); /////////////////////////////////////////////////////////////////////color
                    foodRemaining -= nseed;
                    Console.WriteLine($"You wish to buy {nland} acres, feed your people {nfood} bushels, and plant {nseed} seeds.");
                    Console.Write($"This will leave you with {foodRemaining} bushels, is that correct? Enter N to retry. ");
                    doubleCheck = Console.ReadLine();
                } while (doubleCheck == "N");

                myPlayer.updatePlayer(nland, nfood, nseed);
            }
            public bool checkEndGame(Player myPlayer)
            {
                if (myPlayer.isUprising() || myPlayer.getPopulation() == 0)
                {
                    Console.WriteLine($"Due to EXTREME mismanagement, {myPlayer.getStarvingNumber()} have starved!");
                    if (myPlayer.getPopulation() == 0)
                    {
                        Console.WriteLine("What a terrible ruler; you have no people left to rule!");
                    }
                    else
                    {
                        Console.WriteLine(" The remaining population\nhas overthrown you and you have been declared the worst King in history!");
                    }
                    return true;
                }
                return false;
            }
        }
    }
}

