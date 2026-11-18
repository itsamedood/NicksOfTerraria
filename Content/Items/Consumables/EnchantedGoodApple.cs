using NoxusBoss.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Items.Consumables
{
	/// <summary>
	/// DOESN'T WORK YET!
	/// </summary>
	public class EnchantedGoodApple : ModItem
	{
		public static readonly int MaxEGAs = 1;
		public static readonly int LifeSet = 2_000_000_000; // lol.

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.LifeFruit);
			Item.rare = ModContent.RarityType<NamelessDeityRarity>();
			Item.value = Item.buyPrice(platinum: 9999);
			Item.maxStack = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return false; // Hard-coded because this item doesn't even work... yet!
			// return player.ConsumedLifeCrystals == Player.LifeCrystalMax && player.ConsumedLifeFruit == Player.LifeFruitMax;
		}

		public override bool? UseItem(Player player)
		{
			int healAmount = LifeSet - player.statLifeMax;

			player.statLifeMax = LifeSet;
			player.statLifeMax2 = LifeSet;
			player.statLife = LifeSet;

			if (Main.myPlayer == player.whoAmI && healAmount > 0) player.HealEffect(healAmount);

			return true;
		}

		public override void AddRecipes()
		{
			// CreateRecipe()
			// 	.AddIngredient<NoxusBoss.Content.Items.GoodApple>(9999) // Good luck with that!
			// 	.Register();
		}
	}
}
