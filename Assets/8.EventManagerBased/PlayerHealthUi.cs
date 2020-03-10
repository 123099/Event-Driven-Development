using System.Globalization;
using _8.EventManagerBased.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _8.EventManagerBased
{
	// There is still tight coupling between different objects in the editor.
	[RequireComponent(typeof(Text))]
	public class PlayerHealthUi : MonoBehaviour
	{
		private Text healthUiText;

		private void Awake()
		{
			healthUiText = GetComponent<Text>();
		}

		private void OnEnable()
		{
			EventManager.AddListener<HealthChangedGameEvent>(OnHealthChanged);
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<HealthChangedGameEvent>(OnHealthChanged);
		}

		private void OnHealthChanged(HealthChangedGameEvent gameEvent) => SetHealth(gameEvent.CurrentHealth);
		
		public void SetHealth(int health)
		{
			healthUiText.text = health.ToString(CultureInfo.InvariantCulture);
		}
	}
}