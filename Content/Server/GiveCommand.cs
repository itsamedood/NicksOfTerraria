using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NicksOfTerraria.Content.Server
{
	/// <summary>
	/// Adds a give command, works pretty much like Minecraft (minus the @).
	/// </summary>
	public class GiveCommand : ModCommand
	{
		public override CommandType Type => CommandType.World;
		public override string Command => "give";
		public override string Usage => "/give <ID or name> [amount] | Items with spaces, use \\ (Ex. Like\\This\\See?).";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply(Usage);
				return;
			}

			string query = args[0];
			int item = 0;
			int amount = 1;

			if (args.Length > 1 && int.TryParse(args[1], out int parsedInt))
				amount = parsedInt;

			if (int.TryParse(query, out int id))
				item = id;
			else
				item = FindItemByString(query);

			if (item > 0)
			{
				caller.Player.QuickSpawnItem(caller.Player.GetSource_FromThis(), item, amount);
				caller.Reply($"Gave {amount} {item} to you.", Color.Green);
			}
			else
				caller.Reply($"Could not find {query}.", Color.Red);
		}

		private static int FindItemByString(string query)
		{
			query = query.Replace('\\', ' ');

			if (ItemID.Search.TryGetId(query, out int vanillaId))
				return vanillaId;

			if (query.Contains('/'))
			{
				string[] split = query.Split('/');
				if (split.Length == 2 && ModContent.TryFind(split[0], split[1], out ModItem modItem))
					return modItem.Type;
			}

			foreach (var pair in ContentSamples.ItemsByType)
			{
				Item item = pair.Value;

				if (item.ModItem != null && item.ModItem.Name.Equals(query, System.StringComparison.OrdinalIgnoreCase))
					return item.type;

				if (Lang.GetItemNameValue(item.type).Equals(query, System.StringComparison.OrdinalIgnoreCase))
					return item.type;
			}

			return 0; // Not found.
		}
	}
}
