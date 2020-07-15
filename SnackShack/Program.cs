using System;
using System.Collections.Generic;
using System.Linq;
using SnackShack.EquipmentItems;
using SnackShack.FoodItems;

namespace SnackShack
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueProgram = false;
            List<string> orderTimeEstimate = new List<string>();

            Console.WriteLine("Welcome to the Snack Shack!");
            Console.WriteLine();
            
            Console.WriteLine("COLLECT INVENTORY:");
            Microwave microwave = new Microwave("Microwave", Constants.DefaultMicrowaveInventory, Constants.MicrowaveInsertTime, Constants.MicrowaveRemoveTime);
            microwave.CollectInventory(Constants.DefaultMicrowaveInventory, Constants.MaxMicrowaveInventory);
            
            Sandwich sandwich = new Sandwich("Sandwich", Constants.MaxSandwichInventory, Constants.SandwichMakeTime, Constants.SandwichServeTime);
            sandwich.CollectInventory(Constants.DefaultSandwichInventory, Constants.MaxSandwichInventory);
            
            JacketPotato potato = new JacketPotato("Jacket Potato", Constants.MaxPotatoInventory, Constants.PotatoMakeTime, Constants.PotatoTopTime, Constants.PotatoServeTime);
            potato.CollectInventory(Constants.DefaultPotatoInventory, Constants.MaxPotatoInventory);

            List<Food> foods = new List<Food>();
            foods.Add(sandwich);
            foods.Add(potato);

            // Initializations
            int currentTime = 0;
            int currentFoodPrepTime = 0;
            int totalSandwichPrepTime = sandwich.GetTotalPrepTime();
            int totalPotatoPrepTime = potato.GetTotalPrepTime();
            int maxSandwichOrderNum = Constants.MaxSandwichWait/totalSandwichPrepTime;
            int maxPotatoOrderNum = (Constants.MaxPotatoWait - potato.CookTime)/(potato.TopTime + potato.ServeTime + microwave.InsertTime + microwave.RemoveTime);
            int potatoCookTime = potato.CookTime > totalSandwichPrepTime ? potato.CookTime : totalSandwichPrepTime;

            if (sandwich.Inventory > 0 || potato.Inventory > 0)
                continueProgram = true;

            while(continueProgram)
            {
                // Initialize
                currentTime = 0;
                currentFoodPrepTime = 0;                
                sandwich.ResetOrderInfo();                
                potato.ResetOrderInfo();
                orderTimeEstimate.Clear();

                Console.WriteLine();
                Console.WriteLine("COLLECT ORDERS:");
                // Collect Sandwich Order
                if (sandwich.Inventory == 0)
                {
                    Console.WriteLine("We apologize, but we are out of {0}es.", sandwich.Name);
                    Console.WriteLine("Would you like to place an order for {0}es? (Y/N)", potato.Name);
                    string enteredInventory = Console.ReadLine();
                    Console.WriteLine();
                    if (enteredInventory.ToUpper().Equals("Y"))
                        potato.CollectOrder(maxPotatoOrderNum);
                }
                else
                    sandwich.CollectOrder(maxSandwichOrderNum);

                // Collect Potato Order                    
                if (potato.Inventory == 0)
                {
                    Console.WriteLine("We apologize, but we are out of {0}es.", potato.Name);
                    Console.WriteLine("Would you like to place an order for {0}es? (Y/N)", sandwich.Name);
                    string enteredInventory = Console.ReadLine();
                    Console.WriteLine();
                    if (enteredInventory.ToUpper().Equals("Y"))
                        sandwich.CollectOrder(maxSandwichOrderNum);                
                }
                else
                {
                    totalSandwichPrepTime = sandwich.NumOfOrderItems * sandwich.GetTotalPrepTime();
                    potatoCookTime = potato.CookTime > totalSandwichPrepTime ? potato.CookTime : totalSandwichPrepTime;
                    maxPotatoOrderNum = (Constants.MaxPotatoWait - potatoCookTime) / (potato.TopTime + potato.ServeTime + microwave.InsertTime + microwave.RemoveTime);
                    potato.CollectOrder(maxPotatoOrderNum);
                }

                // List order items
                Console.WriteLine();
                Console.WriteLine("ORDERS:");
                foreach(Food foodItemOrdered in foods)
                {
                    if (foodItemOrdered.NumOfOrderItems > 0)
                        Console.WriteLine("{0} {1} orders placed.", foodItemOrdered.NumOfOrderItems, foodItemOrdered.Name);
                }

                // Schedule
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("SCHEDULE:");

                // If necessary, put jacket potato(es) in the microwave(s).
                for (int i = 1; i <= potato.NumOfOrderItems; i++)
                {
                    Console.WriteLine("{0} put {1} {2} in {3}", Utility.ConvertSecToTimeString(currentTime), potato.Name, i, microwave.Name);
                    currentTime = currentTime + microwave.InsertTime;
                }
                
                // Sanwdich Schedule
                while (sandwich.OrderNumber <= sandwich.NumOfOrderItems && sandwich.Inventory > 0)
                {
                    if (currentFoodPrepTime == 0)
                    {
                        Console.WriteLine("{0} make {1} {2}", Utility.ConvertSecToTimeString(currentTime), sandwich.Name, sandwich.OrderNumber);
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;
                    }
                    else if (currentFoodPrepTime == sandwich.MakeTime)
                    {
                        Console.WriteLine("{0} serve {1} {2}", Utility.ConvertSecToTimeString(currentTime), sandwich.Name, sandwich.OrderNumber);
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;

                    }
                    else if (currentFoodPrepTime == sandwich.GetTotalPrepTime())
                    {
                        orderTimeEstimate.Add(sandwich.Name + " " + sandwich.OrderNumber + ": " + Utility.ConvertSecToTimeString(currentTime));
                        sandwich.OrderNumber++;
                        sandwich.Inventory--;
                        currentFoodPrepTime = 0;                       
                    }
                    else
                    {
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;
                    }
                }

                // If necessary, remove jacket potato(es) from the microwave(s).
                for (int i = 1; i <= potato.NumOfOrderItems; i++)                    
                {
                    Console.WriteLine("{0} take {1} {2} out of {3}", Utility.ConvertSecToTimeString(currentTime), potato.Name, i, microwave.Name);
                    currentTime = currentTime + microwave.RemoveTime;
                }

                // Advance currentTime until we have reached requirements for potato cook time.
                while (currentTime < potato.CookTime)
                    currentTime = currentTime + 30;

                // Potato Schedule
                while (potato.OrderNumber <= potato.NumOfOrderItems && potato.Inventory > 0)
                {
                    if (currentFoodPrepTime == 0)
                    {
                        Console.WriteLine("{0} top {1} {2}", Utility.ConvertSecToTimeString(currentTime), potato.Name, potato.OrderNumber);
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;

                    }
                    else if (currentFoodPrepTime == potato.TopTime)
                    {
                        Console.WriteLine("{0} serve {1} {2}", Utility.ConvertSecToTimeString(currentTime), potato.Name, potato.OrderNumber);
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;

                    }
                    else if (currentFoodPrepTime == potato.TopTime + potato.ServeTime)
                    {
                        orderTimeEstimate.Add(potato.Name + " " + potato.OrderNumber + ": " + Utility.ConvertSecToTimeString(currentTime));
                        potato.OrderNumber++;
                        potato.Inventory--;
                        currentFoodPrepTime = 0;                        
                    }
                    else
                    {
                        currentFoodPrepTime = currentFoodPrepTime + 30;
                        currentTime = currentTime + 30;
                    }
                }

                Console.WriteLine("{0} take a break!", Utility.ConvertSecToTimeString(currentTime));
                Console.WriteLine();

                // Time estimate
                Console.WriteLine();
                Console.WriteLine("TIME ESTIMATE:");
                foreach(string t in orderTimeEstimate)
                {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine();
                
                if (sandwich.Inventory == 0 && potato.Inventory == 0)
                    continueProgram = false;
                
            }

            Console.WriteLine();
            Console.WriteLine("You have depleted your Sandwich and Jacket Potato inventory.");
            Console.WriteLine("Please order more inventory and restart the program.");
            Console.WriteLine("Please enter return to exit program.");
            Console.ReadLine();
        }
    }
}
