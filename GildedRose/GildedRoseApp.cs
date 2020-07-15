using GildedRose.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose
{
    class GildedRoseApp2
    {
        readonly IList<Item> Items;

        public GildedRoseApp2(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i].UpdateQuality();
            }
        }
    }
}