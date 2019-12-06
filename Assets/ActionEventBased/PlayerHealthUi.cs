using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEventBased
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
			player.HealthChanged += OnPlayerHealthChanged;
		}

		private void OnDisable()
		{
			if (!player)
			{
				return;
			}
			player.HealthChanged -= OnPlayerHealthChanged;
		}

		private void OnPlayerHealthChanged(int health)
		{
			healthUiText.text = health.ToString(CultureInfo.InvariantCulture);
		}
	}
}