using System;

namespace Hammurabi
{
    class program
    {
        static int yearNumber;
        static int population;
        static int land;
        static int food;
        static int priceOfLand;
        static int starvingNumber;
        static int immigrantNumber;
        static int plagueNumber;
        static int harvestNumber;
        static int ratNumber;
        public static int PopulationDefault { get; set; }
        public static int LandDefault { get; set; }
        public static int FoodDefault { get; set; }
        public static int PriceDefault { get; set; }
        //public class Player
        //{
        public static void Main(string[] args)
        {
          

            PopulationDefault = 100; // 100
            LandDefault = 1000; // 1000
            FoodDefault = 2800; // 2800
            PriceDefault = 20; // 20

            yearNumber = 1;
            population = PopulationDefault;
            land = LandDefault;
            food = FoodDefault;
            priceOfLand = PriceDefault;

            PrintPlayer();

            bool winner = true;

            // loop for 10 years
            for (int i = 0; i < 10; i++)
            {
                GetInput();
                if (CheckEndGame())
                {
                    winner = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("GAME OVER");
                    Console.ResetColor();

                    break;
                }
                else
                {
                    PrintReport();
                    PrintPlayer();
                }
            }
            if (winner)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine("You have kept your people alive and 'well' for 10 years!");
                Console.WriteLine("You are truly great!");
                Console.WriteLine("CONGRATULATIONS");
                Console.ResetColor();
            }
        } // ===================================== Main ends here =========================================

        // Methods to do shit

        public static int GetYearNumber() { return yearNumber; }
        public static int GetPopulation() { return population; }
        public static int GetLand() { return land; }
        public static int GetFood() { return food; }
        public static int GetPriceOfLand() { return priceOfLand; }

        public static int GetStarvingNumber() { return starvingNumber; }
        public static int GetImmigrantNumber() { return immigrantNumber; }
        public static int GetPlagueNumber() { return plagueNumber; }
        public static int GetHarvestNumber() { return harvestNumber; }
        public static int GetRatNumber() { return ratNumber; }

        public static void SetYearNumber(int n) { yearNumber = n; }
        public static void SetPopulation(int n) { population = n; }
        public static void SetLand(int n) { land = n; }
        public static void SetFood(int n) { food = n; }
        public static void SetPriceOfLand(int n) { priceOfLand = n; }


        public static void PrintPlayer() //print player's variables
        {
            Console.WriteLine("-------------------------------------------"
                + "\nYear:                      " + yearNumber
                + "\nAcres of land:             " + land
                + "\nPopulation:                " + population
                + "\nStored grain:              " + food
                + "\nPrice of land:             " + priceOfLand
                + "\n");
        }

        public static void PrintReport() //Print the report after each turn
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

        public static int GetPlagueDeath()    //calculate how many people die by plague
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

        public static int GetStarvingDeath(int nFood)  //calculate how many people die by starving
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

        public static bool IsUprising()  //return true if people want to uprising
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

        public static int GetImmigrant()  //calculate how many new people enter
        {
            if (GetStarvingNumber() > 0)
            {
                return 0;
            }
            return (20 * land + food) / (100 * population) + 1;
        }

        //Choose a random integer between 1 and 6, inclusive.
        //Each acre that was planted with seed will yield this many bushels of grain.
        //(Example: if you planted 50 acres, and your number is 3, you harvest 150 bushels of grain).
        //Return the number of bushels harvested.

        public static int GetHarvest(int nSeed)    //calculate how much we harvest
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

        public static int GetEatenByRat()
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

        public static int GetNewPrice()   //calculate new price for land
        {
            var rnd = new Random();
            int chance = (rnd.Next(0, 6));
            return chance + 17;
        }
        // Update on every turn
        public static bool UpdatePlayer(int nLand, int nFood, int nSeed)
        {
            //update the remaining stats first, then calculate all the random elements
            food -= nFood;
            //seed -= nSeed;
            land += nLand;

            food -= nLand * priceOfLand;

            starvingNumber = GetStarvingDeath(nFood);
            immigrantNumber = GetImmigrant();
            plagueNumber = GetPlagueDeath();
            harvestNumber = GetHarvest(nSeed);

            population -= plagueNumber;
            population -= starvingNumber;

            population += immigrantNumber;

            food += harvestNumber;
            ratNumber = GetEatenByRat();
            food -= ratNumber;

            priceOfLand = GetNewPrice();

            yearNumber++;

            if (population < 0)
                population = 0;

            return true;
        }

        public static int ValidateIntegerInput()
        {
            while (true)
            {
                string _input = Console.ReadLine();
                // insert REAL validity check here
                int input = Int32.Parse(_input);
                return input;
            }
        }
        public static void GetInput()
        {
            int nland;
            int nfood;
            int nseed;
            bool invalid = false;
            string doubleCheck;
            string inputLine;
            do
            {
                int foodRemaining = GetFood();
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("How many acres do you wish to buy (Negative to sell)? ");
                    nland = ValidateIntegerInput();
                    if (GetFood() < (nland * GetPriceOfLand()))
                    {
                        Console.WriteLine($"You can't buy {nland} acres, you only have {GetFood()} bushels!");
                    }
                } while (GetFood() < (nland * GetPriceOfLand()));


                foodRemaining -= nland * GetPriceOfLand();
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.Write("How many bushels do you wish to feed your people? ");
                    nfood = ValidateIntegerInput();
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
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("How many acres do you wish to plant with seed? ");
                    nseed = ValidateIntegerInput();
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

                Console.ResetColor(); 
                foodRemaining -= nseed;
                Console.WriteLine($"You wish to buy {nland} acres, feed your people {nfood} bushels, and plant {nseed} seeds.");
                Console.Write($"This will leave you with {foodRemaining} bushels, is that correct? Enter N to retry. ");
                doubleCheck = Console.ReadLine();
            } while (doubleCheck == "N");

            UpdatePlayer(nland, nfood, nseed);
        }
        public static bool CheckEndGame()
        {
            if (IsUprising() || GetPopulation() == 0)
            {
                Console.WriteLine($"Due to EXTREME mismanagement, {GetStarvingNumber()} have starved!");
                if (GetPopulation() == 0)
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



