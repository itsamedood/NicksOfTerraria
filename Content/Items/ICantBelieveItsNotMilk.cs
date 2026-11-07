using CalamityMod;
using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Items
{
	/// <summary>
	/// Clears all negative effects. Started because I drank Nilk Carton ONE TIME... NEVER AGAIN... OW MY EYES...
	/// </summary>
	public class ICantBelieveItsNotMilk : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.rare = ModContent.RarityType<CosmicPurple>();
			Item.value = Item.buyPrice(platinum: 1, gold: 2);
			Item.maxStack = Item.CommonMaxStack;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useTurn = true;
			Item.consumable = true;
			// Item.potion = true;
		}

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 25;
			// Main.RegisterItemAnimation(Type, new DrawAnimationVertical(-1, 3) { NotActuallyAnimating = true });
		}

		public override void AddRecipes()
		{
			Condition postDog = new(
				"Post-DoG",
				() => DownedBossSystem.downedDoG
			);

			CreateRecipe()
				.AddCondition(postDog)
				.AddIngredient(ItemID.DryBomb)
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

		public override bool? UseItem(Player player)
		{
			for (int i = Player.MaxBuffs - 1; i >= 0; i--)
			{
				int buff = player.buffType[i];
				if (buff > 0)
				{
					bool isDebuff = Main.debuff[buff] || BuffID.Sets.NurseCannotRemoveDebuff[buff];

					if (isDebuff)
					{
						player.DelBuff(i);
						player.buffType[i] = 0;
						player.buffTime[i] = 0;

						player.buffImmune[buff] = true;
						// player.buffImmune[buff] = false;
					}
				}
			}

			return true;
		}
	}
}
