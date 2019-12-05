using UnityEngine;
using UnityEngine.UI;

namespace Normal
{
	[RequireComponent(typeof(Text))]
	public class PlayerHealthUi : MonoBehaviour
	{
		[SerializeField] private Player player = null;

		private Text healthUiText = null;

		private void Awake()
		{
			healthUiText = GetComponent<Text>();
		}

		private void Update()
		{
			// Okay, but this is polling the value and updating the UI EVERY SINGLE FRAME, while the health doesn't change that often.
			healthUiText.text = player.Health.ToString();
		}
	}
}
