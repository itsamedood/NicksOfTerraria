using CalamityMod;
using Microsoft.Xna.Framework;
using NicksOfTerraria.Content.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Entities
{
	/// <summary>
	/// Represents a player in Nicks Of Terraria.
	/// </summary>
	public class Employee : ModPlayer
	{
		public override void PostUpdate()
		{
			AbnormalityRelocatorTP();
		}

		public void AbnormalityRelocatorTP()
		{
			if (CalamityKeybinds.NormalityRelocatorHotKey != null && CalamityKeybinds.NormalityRelocatorHotKey.JustPressed)
			{
				// Checks main inventory & open void bag.
				if (Player.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<AbnormalityRelocator>()))
				{
					if (Player.dead || Player.frozen || Player.stoned) return;

					Vector2 ogPos = Player.position;
					Vector2 newPos = Main.MouseWorld;
					SoundEngine.PlaySound(SoundID.Item8, Player.position);

					Player.Teleport(newPos, TeleportationStyleID.RodOfDiscord);

					if (Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.SendData(
							MessageID.TeleportEntity,
							-1,
							-1,
							null,
							0,
							Player.whoAmI,
							newPos.X,
							newPos.Y,
							TeleportationStyleID.RodOfDiscord
						);
					}

					if (Player.HasBuff(BuffID.ChaosState))
						Player.ClearBuff(BuffID.ChaosState);

					SpawnQuickDust(ogPos);
					SpawnQuickDust(Player.position);
				}
			}
		}

		private void SpawnQuickDust(Vector2 pos)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust d = Dust.NewDustDirect(pos, Player.width, Player.height, DustID.LifeDrain, 0f, 0f, 100, default, 1.2f);
				d.noGravity = true;
				d.velocity *= 1.5f;
				d.fadeIn = 0.2f;
			}
		}
	}
}
