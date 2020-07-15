using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackShack.EquipmentItems;

namespace SnackShack.FoodItems
{
    class JacketPotato : Food
    {
        int cookTime;
        int topTime;
        int serveTime;
        int orderNumber;

        public int CookTime
        {
            get
            {
                return this.cookTime;
            }
            private set
            {
                this.cookTime = value;
            }
        }

        public int TopTime
        {
            get
            {
                return this.topTime;
            }
            private set
            {
                this.topTime = value;
            }
        }

        public int ServeTime
        {
            get
            {
                return this.serveTime;
            }
            private set
            {
                this.serveTime = value;
            }
        }

        public int OrderNumber
        {
            get
            {
                return this.orderNumber;
            }
            set
            {
                this.orderNumber = value;
            }
        }

        public JacketPotato(string name, int inventory, int cookTime, int topTime, int serveTime)
            : base(name, inventory)
        {
            this.CookTime = cookTime;
            this.TopTime = topTime;
            this.ServeTime = serveTime;
            this.OrderNumber = 1;
        }

        public int GetTotalPrepTime()
        {
            return this.CookTime + this.TopTime + this.ServeTime;
        }

        public void ResetOrderInfo()
        {
            this.OrderNumber = 1;
            this.NumOfOrderItems = 0;
        }
    }
}
