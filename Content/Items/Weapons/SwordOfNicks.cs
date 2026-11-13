using Microsoft.Xna.Framework;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;
using NoxusBoss.Core.World.WorldSaving;
using NoxusBoss.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Items.Weapons
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class SwordOfNicks : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.NicksOfTerraria.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.DamageType = DamageClass.Melee;
			Item.width = 80;
			Item.height = 80;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.knockBack = 10f;
			Item.useTurn = true;
			Item.autoReuse = false;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ModContent.RarityType<AvatarRarity>();
			Item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				if (Main.GameModeInfo.IsJourneyMode) return true;

				if (!BossDownedSaveSystem.HasDefeated<NamelessDeityBoss>())
				{
					if (player.whoAmI == Main.myPlayer)
						Main.NewText("A deity has cursed this sword... Defeat it to unleash it's power...", Color.Red);

					return false;
				}
			}

			return base.CanUseItem(player);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<CalamityMod.Items.Materials.MiracleMatter>(2)
				.AddIngredient<CalamityMod.Items.Materials.ShadowspecBar>(50)
				.AddIngredient<CalamityMod.Items.Materials.AuricBar>(50)
				.AddIngredient<CalamityMod.Items.Materials.CosmiliteBar>(50)
				.AddIngredient<CalamityMod.Items.Materials.AstralBar>(50)
				.AddIngredient(ItemID.LunarBar, 50)
				.AddTile(ModContent.Find<ModTile>("CalamityMod", "DraedonsForge").Type)
				.Register();
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			hit.Damage = int.MaxValue;
			target.life = 0;
			target.checkDead();
		}
	}
}
