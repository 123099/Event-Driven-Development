using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace ActionEventBasedAdvanced
{
	public class PlayerDamageUi : MonoBehaviour
	{
		[SerializeField] private Text damageUiTextPrefab;
		[SerializeField] private Player player;

		private void OnEnable()
		{
			var playerAttack = player.GetComponent<Attack>();

			if (playerAttack)
			{
				playerAttack.DealtDamage += OnPlayerDealtDamage;
			}
		}

		private void OnDisable()
		{
			var playerAttack = player.GetComponent<Attack>();

			if (playerAttack)
			{
				playerAttack.DealtDamage -= OnPlayerDealtDamage;
			}
		}

		// No longer have to poll the player's state or manage player state variables to tell us when damage was dealt and how much.
		private void OnPlayerDealtDamage(int damage)
		{
			StartCoroutine(DisplayDamageCoroutine(damage));
		}
		
		private IEnumerator DisplayDamageCoroutine(float damage)
		{
			var damageUiTextClone = Instantiate(damageUiTextPrefab, transform);
			damageUiTextClone.gameObject.SetActive(true);
			damageUiTextClone.rectTransform.anchoredPosition += Random.Range(-20.0f, 20.0f) * Vector2.right;
			
			damageUiTextClone.text = damage.ToString(CultureInfo.InvariantCulture);

			var accumulatedTime = 0.0f;

			while (accumulatedTime < 1.0f)
			{
				accumulatedTime += Time.deltaTime;
				damageUiTextClone.rectTransform.anchoredPosition += 50.0f * Time.deltaTime * Vector2.up;
				yield return null;
			}
			
			Destroy(damageUiTextClone.gameObject);
		}
	}
}