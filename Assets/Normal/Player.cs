using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Normal
{
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

		private float timeBetweenFootSteps;
		private float timeSinceLastFootStep;

		private int CurrentHealth { get; set; }

		private void Awake()
		{
			timeBetweenFootSteps = 1.0f / footStepRate;

			audioSource = GetComponent<AudioSource>();
			CurrentHealth = maxHealth;
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

			// TODO: Add attack UI
			if (Input.GetButtonDown("Fire1")) CurrentHealth -= 5;
		}
	}
}