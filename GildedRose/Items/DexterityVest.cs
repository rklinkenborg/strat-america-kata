using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose.Items
{
    class DexterityVest : Item
    {

        public override void UpdateQuality()
        {
            if (this.Quality > Constants.MinQuality)
                this.Quality -= 1;

            this.SellIn -= 1;

            if (this.SellIn < 0 && this.Quality > Constants.MinQuality)
                this.Quality -= 1;
        }
    }
}
