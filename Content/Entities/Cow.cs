using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Entities
{
	/// <summary>
	/// New passive critter that can be slaughtered for meat cuts.
	/// </summary>
	public class Cow : ModNPC
	{
		// private const int ClonedNPCID = NPCID.Squirrel;

		// public override void Load()
		// {
		// 	base.Load();
		// }

		public override void SetStaticDefaults()
		{
			// Main.npcFrameCount[Type] = Main.npcFrameCount[ClonedNPCID];
		}
	}
}
