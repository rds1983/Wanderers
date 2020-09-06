﻿using System.Collections.Generic;
using TroublesOfJord.Core.Items;

namespace TroublesOfJord.Core.Abilities
{
	public enum BonusType
	{
		Attacks
	}

	public enum AbilityType
	{
		Automatic,
		Instant
	}

	public class AbilityInfo: BaseObject
	{
		public string Name;
		public int Energy;
		public AbilityType Type;
		public string Description;
		
		public AbilityRequirement[] Requirements;
		public BaseItemInfo Manual;

		public Dictionary<BonusType, int> Bonuses { get; } = new Dictionary<BonusType, int>();
	}
}