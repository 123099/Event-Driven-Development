using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace _2.CleanedUp
{
    public class PlayerDamageUi : MonoBehaviour
    {
        [SerializeField] private Text damageUiTextPrefab;
        [SerializeField] private Player player;

        private void Update()
        {
            // Polling for data is a great candidate for events
            if (player.HasDealtDamage) StartCoroutine(DisplayDamageCoroutine(player.DamageDealt));
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