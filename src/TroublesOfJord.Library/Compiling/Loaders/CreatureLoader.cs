﻿using Newtonsoft.Json.Linq;
using TroublesOfJord.Core;
using TroublesOfJord.Core.Items;

namespace TroublesOfJord.Compiling.Loaders
{
	public class CreatureLoader: Loader<CreatureInfo>
	{
		public CreatureLoader(): base("CreatureInfos")
		{
		}

		public override BaseObject LoadItem(Module module, string id, ObjectData data)
		{
			var creature = (CreatureInfo)base.LoadItem(module, id, data);

			JToken t;
			if (data.Data.TryGetValue("Inventory", out t))
			{
				JObject obj = (JObject)t;

				var inventory = new Inventory();

				foreach (var pair in obj)
				{
					inventory.Items.Add(new ItemPile
					{
						Item = new Item(module.EnsureItemInfo(pair.Key)),
						Quantity = (int)pair.Value
					});
				}

				creature.Inventory = inventory;
			}

			return creature;
		}
	}
}