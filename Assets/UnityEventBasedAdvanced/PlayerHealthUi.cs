using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEventBasedAdvanced
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

		public void SetHealth(int health)
		{
			healthUiText.text = health.ToString(CultureInfo.InvariantCulture);
		}
	}
}