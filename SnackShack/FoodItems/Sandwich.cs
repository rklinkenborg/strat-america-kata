using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackShack;

namespace SnackShack.FoodItems
{
    class Sandwich : Food
    {
        int makeTime;
        int serveTime;
        int orderNumber;

        public int MakeTime 
        {
            get
            {
                return this.makeTime;
            }
            private set
            {
                this.makeTime = value;
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

        public Sandwich(string name, int inventory, int makeTime, int serveTime)
            : base(name, inventory)
        {
            this.MakeTime = makeTime;
            this.ServeTime = serveTime;
            this.OrderNumber = 1;
        }

        public int GetTotalPrepTime()
        {
            return this.MakeTime + this.ServeTime;
        }

        public void ResetOrderInfo()
        {
            this.OrderNumber = 1;
            this.NumOfOrderItems = 0;
        }
    }
}
