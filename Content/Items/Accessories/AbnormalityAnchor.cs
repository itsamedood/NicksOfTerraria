using CalamityMod;
using CalamityMod.Rarities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Items.Accessories
{
	public class AbnormalityAnchor : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.rare = ModContent.RarityType<CalamityRed>();
			Item.value = Item.buyPrice(platinum: 5);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			string keybindName = "Unbound";

			if (CalamityKeybinds.NormalityRelocatorHotKey != null)
			{
				var assignedKeys = CalamityKeybinds.NormalityRelocatorHotKey.GetAssignedKeys();
				if (assignedKeys.Count > 0) keybindName = assignedKeys[0]; // [Mouse3] in my case.
			}

			tooltips.Add(new TooltipLine(
				Mod,
				"NormalityKeybind",
				$"Press [{keybindName}] to teleport to the position of the mouse."
			));

			tooltips.Add(new TooltipLine(
				Mod,
				"NoChaosState",
				"Does not cause Chaos State."
			));

			tooltips.Add(new TooltipLine(
				Mod,
				"WorksInInventory",
				"Works while in the inventory or open Void Bag."
			));

			tooltips.Add(new TooltipLine(
				Mod,
				"SlayAssLine",
				"'Before there was time, before there was chaos, there was nothing...'"
			));
		}

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void AddRecipes()
		{
			Condition postCalamitas = new(
				"Post-Calamitas",
				() => DownedBossSystem.downedCalamitas
			);

			CreateRecipe()
				.AddCondition(postCalamitas)
				.AddIngredient<CalamityMod.Items.Tools.NormalityRelocator>()
				.AddIngredient<CalamityMod.Items.Materials.ShadowspecBar>(10)
				.AddTile(ModContent.Find<ModTile>("CalamityMod", "DraedonsForge").Type)
				.Register();
		}
	}
}
