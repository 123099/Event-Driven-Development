using System;
using System.Collections;
using System.Globalization;
using EventManagerBased.Events;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace EventManagerBased
{
	public class PlayerDamageUi : MonoBehaviour
	{
		[SerializeField] private Text damageUiTextPrefab;

		private void OnEnable()
		{
			// We lose some of the dynamic Unity event functionality, as we are working with GameEvent based classes instead of primitives or data types directly.
			EventManager.AddListener<DamageGameEvent>(OnDamageDealt);
		}

		private void OnDisable()
		{
			// Always requires clean up, which can sometimes be forgotten. No cleanup could prevent the garbage collection from working properly, null pointer exceptions, etc.
			EventManager.RemoveListener<DamageGameEvent>(OnDamageDealt);
		}

		private void OnDamageDealt(DamageGameEvent gameEvent) => DisplayDamage(gameEvent.DamageDealt);
		
		public void DisplayDamage(int damage)
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