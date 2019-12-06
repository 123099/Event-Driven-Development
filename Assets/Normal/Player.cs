using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Normal
{
	// You also have to remember to drag in all the references to hook up the dependencies.
	[RequireComponent(typeof(AudioSource))]
	public class Player : MonoBehaviour
	{
		private AudioSource audioSource;

		[SerializeField] private ParticleSystem footStepParticleEffect;
		[SerializeField] private float footStepRate = 3.0f;

		[SerializeField] private AudioClip[] footStepSounds;

		[SerializeField] private int maxHealth = 100;
		[SerializeField] private float movementSpeed = 10.0f;
		[SerializeField] private Text playerHealthUiText;
		[SerializeField] private Text damageUiText;

		private float timeBetweenFootSteps;
		private float timeSinceLastFootStep;

		private int CurrentHealth { get; set; }

		private void Awake()
		{
			timeBetweenFootSteps = 1.0f / footStepRate;

			audioSource = GetComponent<AudioSource>();
			CurrentHealth = maxHealth;
			
			damageUiText.gameObject.SetActive(false);
		}

		// How much of this code actually relates to player logic, and how much to extra effects?
		// How many things is this single COMPONENT trying to accomplish?
		private void Update()
		{
			var movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

			if (Mathf.Abs(movementInput.sqrMagnitude) < 0.1f)
			{
				timeSinceLastFootStep = timeBetweenFootSteps;
				// Everything is in the same method, and this return can cause trouble.
				return;
			}

			transform.Translate(Time.deltaTime * movementSpeed * movementInput.normalized);

			if (timeSinceLastFootStep >= timeBetweenFootSteps)
			{
				timeSinceLastFootStep -= timeBetweenFootSteps;

				audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
				Instantiate(footStepParticleEffect, transform.position, transform.rotation);
			}

			timeSinceLastFootStep += Time.deltaTime;

			// Add this here. But it doesn't work because of the return above.
			// Fix this by moving it up, but this still requires modifying the player and suddenly adding UI into the mix.
			// Another way is polling for the health from a player UI script.
			playerHealthUiText.text = CurrentHealth.ToString(CultureInfo.InvariantCulture);

			if (Input.GetButtonDown("Fire1"))
			{
				const int damage = 5;
				CurrentHealth -= damage;
				StartCoroutine(DisplayDamage(damage));
			}
		}

		// Handles UI logic AND animations!
		// If someone else needs to work with the UI in the future, it would be really hard to find where the logic actually happens, and it is also very hard to reuse this code!
		private IEnumerator DisplayDamage(int damage)
		{
			var damageUiTextClone = Instantiate(damageUiText, damageUiText.transform.parent);
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