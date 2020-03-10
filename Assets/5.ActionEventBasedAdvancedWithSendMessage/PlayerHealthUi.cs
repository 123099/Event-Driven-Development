using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace _5.ActionEventBasedAdvancedWithSendMessage
{
	[RequireComponent(typeof(Text))]
	public class PlayerHealthUi : MonoBehaviour
	{
		private Text healthUiText;
		[SerializeField] private Player player;

		private void Awake()
		{
			healthUiText = GetComponent<Text>();
		}

		private void OnEnable()
		{
			if (!player)
			{
				return;
			}
			
			// We start seeing problems about still some existing dependencies, checks everywhere, etc.
			// Doing this check is unreliable, because OnEnable is called right after the Awake of this object. Sometimes this Awake is called before the Awake of the player, and therefore, the Health is not initialized.
//			if (player.Health)
//			{
//				player.Health.Changed += OnPlayerHealthChanged;
//			}

			// So, we have to get the component manually, which is still okay.
			var playerHealth = player.GetComponent<Health>();

			if (playerHealth)
			{
				playerHealth.Changed += OnPlayerHealthChanged;
			}
		}

		private void OnDisable()
		{
			if (!player)
			{
				return;
			}
			
			// Because we are getting the component manually on enable, keep it symmetric and get it manually here as well.
//			if (player.Health)
//			{
//				player.Health.Changed -= OnPlayerHealthChanged;
//			}

			var playerHealth = player.GetComponent<Health>();

			if (playerHealth)
			{
				playerHealth.Changed -= OnPlayerHealthChanged;
			}
		}

		// SendMessage only sends the message to the components on the same object. There is no way to listen to that message from another game object.
		private void OnPlayerHealthChanged(int health)
		{
			healthUiText.text = health.ToString(CultureInfo.InvariantCulture);
		}
	}
}