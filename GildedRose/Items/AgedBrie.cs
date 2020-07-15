using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GildedRose.Items
{
    class AgedBrie : Item
	{
		public override void UpdateQuality()
		{			
			if (this.Quality < Constants.MaxQuality)
				this.Quality += 1;

			this.SellIn -= 1;

			if (this.SellIn < 0 && this.Quality < Constants.MaxQuality)
				this.Quality += 1;
		}

	}
}
