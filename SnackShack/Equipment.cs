using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackShack
{
    class Equipment
    {
        string name;
        int inventory;

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

        public Equipment(string name, int inventory)
        {
            this.Name = name;
            this.Inventory = inventory;
        }

        // Collect order.
        public void CollectInventory(int defaultInventory, int maxInventory)
        {
            bool invalidInventory = true;

            while (invalidInventory)
            {
                Console.WriteLine("Please enter the number of {0} available for cooking (0 - {1}) or hit return for default inventory ({2}).", this.Name, maxInventory, defaultInventory);
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
                    Console.WriteLine("Please enter a value from 0 to {0}.", maxInventory);
                }

                Console.WriteLine();
            }
        }
    }
}
