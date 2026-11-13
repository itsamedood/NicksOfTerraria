using CalamityMod;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Items.Consumables
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
			Item.UseSound = SoundID.Item3; // *Gulp*
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useTurn = true;
			Item.consumable = true;
			// Item.potion = true;
		}

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 20;
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
					bool isDebuff = (Main.debuff[buff]
					|| BuffID.Sets.NurseCannotRemoveDebuff[buff])
					&& buff != BuffID.PotionSickness && buff != BuffID.ManaSickness;

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
