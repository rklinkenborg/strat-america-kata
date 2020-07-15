using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose.Items
{
    class BackstagePass : Item
    {
		public override void UpdateQuality()
		{
			if (this.Quality < Constants.MaxQuality)
			{
				this.Quality += 1;

                if (this.SellIn < 11 && this.Quality < Constants.MaxQuality)
                {
                    this.Quality += 1;
                }

                if (this.SellIn < 6 && this.Quality < Constants.MaxQuality)
                {
                        this.Quality += 1;
                }
            }

            this.SellIn -= 1;

            if (this.SellIn < 0)
                this.Quality = Constants.MinQuality;
        }
	}
}
