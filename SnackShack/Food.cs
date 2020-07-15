using System;

namespace SnackShack
{
    class Food
    {
        string name;
        int inventory;
        int numOfOrderItems;

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                this.name = value;
            }
        }

        public int Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        public int NumOfOrderItems
        {
            get
            {
                return this.numOfOrderItems;
            }
            set
            {
                this.numOfOrderItems = value;
            }
        }

        public Food(string name, int inventory)
        {
            this.Name = name;
            this.Inventory = inventory;
        }

        // Collect inventory or use default inventory values.
        public void CollectInventory(int defaultInventory, int maxInventory)
        {
            bool invalidInventory = true;

            while (invalidInventory)
            {
                Console.WriteLine("Please enter your {0} inventory (0 - {1}) or hit return for default inventory ({2}).", this.Name, maxInventory, defaultInventory);
                string enteredInventory = Console.ReadLine();
                int inventory;

                if (enteredInventory.Equals(string.Empty))
                    invalidInventory = false;
                else if (int.TryParse(enteredInventory, out inventory))
                {
                    if (inventory >= 0 && inventory <= maxInventory)
                    {
                        invalidInventory = false;
                        this.Inventory = inventory;
                    }
                }

                if (invalidInventory)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry.");
                    Console.WriteLine("Please enter a value from 0 to {0}." , maxInventory);
                }

                Console.WriteLine();
            }
        }

        // Collect order.
        public void CollectOrder(int maxNumOfOrderItems)
        {
            bool invalidOrder = true;
            int maximumOrder = maxNumOfOrderItems > this.Inventory ? this.Inventory : maxNumOfOrderItems;

            while (invalidOrder)
            {
                Console.WriteLine("Please enter a {0} order from 0 to {1}.", this.Name, maximumOrder);
                string enteredOrder = Console.ReadLine();
                int order;

                if (int.TryParse(enteredOrder, out order))
                {
                    if (order >= 0 && order <= maximumOrder)
                    {
                        invalidOrder = false;
                        this.NumOfOrderItems = this.NumOfOrderItems + order;
                    }
                }

                if (invalidOrder)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry.");
                    Console.WriteLine("Please enter a value from 0 to {0}.", maximumOrder);
                }

                Console.WriteLine();
            }
        }
    }
}
