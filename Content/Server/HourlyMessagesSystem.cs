using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NicksOfTerraria.Content.Server
{
	public class HourlyMessagesSystem : ModSystem
	{
		private readonly string[] messages = [
			// Deli related.
			"Can I get 5 pounds of honey maple turkey sliced thin?",
			"I want the veggie salad with oil vinegar mixed in.",
			"Gimme liverwurst with cajun turkey on wheat bread.",
			"Can you cut my sub into 4ths?",
			"Can I get the works on the side?",
			"Can I get ham please? This one. (pointing to one that you can't see)",

  			// Meat related.
			"What species is your chicken?", // Yes a customer actually asked me this...
			"Are your beef back ribs pork?",
			"Whole new york strip, quarter inch.",
			"I want a whole salmon, skinned, and cut please.",
			"Can you cut it just a little less than quarter inch please?",
			"I want a steak please. No I don't know which one I want."
		];
		private int timer = 0;
		private readonly int intervalInSeconds = 1800;

		public override void PreUpdateEntities()
		{
			if (Main.dedServ || Main.netMode == NetmodeID.Server)
			{
				timer++;

				// Check if our interval elapsed.
				if (timer >= intervalInSeconds * 60)
				{
					timer = 0;
					BroadcastMessage();
				}
			}
		}

		private void BroadcastMessage()
		{
			string chosenMessage = messages[Main.rand.Next(messages.Length)];
			string prefixedMessage = $"[Dumb Customer]: {chosenMessage}";

			ChatHelper.BroadcastChatMessage(
				NetworkText.FromLiteral(prefixedMessage),
				Microsoft.Xna.Framework.Color.Yellow
			);
		}
	}
}
