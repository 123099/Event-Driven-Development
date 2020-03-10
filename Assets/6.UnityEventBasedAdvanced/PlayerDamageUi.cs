using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace _6.UnityEventBasedAdvanced
{
	public class PlayerDamageUi : MonoBehaviour
	{
		[SerializeField] private Text damageUiTextPrefab;

		// This can now be called directly from the editor.
		// UnityEvent calls are serialized and saved with the scene. This results in slower performance, and forces us to keep the functions public, but it is more convenient.
		// Use UnityEvents for things that do not happen very often -> we are having a trade-off between convenience and performance.
		// One other downside is that renaming the functions will break all the references we set up in the editor. This is fixed by using the Rider IDE (does not work for dynamic functions), but anything else has trouble renaming functions that are used by UnityEvents.
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