using System;

namespace SnackShack.EquipmentItems
{
    class Microwave : Equipment
    {
        int insertTime;
        int removeTime;

        public int InsertTime
        {
            get
            {
                return this.insertTime;
            }
            private set
            {
                this.insertTime = value;
            }
        }

        public int RemoveTime
        {
            get
            {
                return this.removeTime;
            }
            private set
            {
                this.removeTime = value;
            }
        }

        public Microwave(string name, int inventory, int insertTime, int removeTime)
            : base(name, inventory)
        {
            this.InsertTime = insertTime;
            this.RemoveTime = removeTime;
        }
    }
}
