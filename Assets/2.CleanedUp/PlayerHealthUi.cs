using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace _2.CleanedUp
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

        private void Update()
        {
            // Okay, but this is polling the value and updating the UI EVERY SINGLE FRAME, while the health doesn't change that often.
            healthUiText.text = player.CurrentHealth.ToString(CultureInfo.InvariantCulture);
        }
    }
}