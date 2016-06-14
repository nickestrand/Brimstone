﻿namespace Brimstone
{
	public partial class CardBehaviour
	{
		// Flame Juggler
		public static Behaviour AT_094 = new Behaviour {
			Battlecry = Damage(RandomOpponentMinion, 1)
		};

		// Boom Bot
		public static Behaviour GVG_110t = new Behaviour {
			Deathrattle = Damage(RandomOpponentMinion, RandomAmount(1, 4))
		};

		// Whirlwind
		public static Behaviour EX1_400 = new Behaviour {
			Battlecry = Damage(AllMinions, 1)
		};

		// Acolyte of Pain
		// Armorsmith
		// Imp Gang Boss
	}
}