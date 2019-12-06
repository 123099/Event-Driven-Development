using UnityEngine;

namespace ActionEventBasedAdvanced
{
	[RequireComponent(typeof(Player), typeof(AudioSource))]
	public class PlayerSounds : MonoBehaviour
	{
		private AudioSource audioSource;
		[SerializeField] private AudioClip[] footStepSounds;

		private Player player;

		private void Awake()
		{
			player = GetComponent<Player>();
			audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			var playerFootSteps = player.GetComponent<FootSteps>();
			
			if (playerFootSteps)
			{
				playerFootSteps.Performed += OnPlayerFootStepPerformed;
			}
		}

		private void OnDisable()
		{
			var playerFootSteps = player.GetComponent<FootSteps>();
			
			if (playerFootSteps)
			{
				playerFootSteps.Performed -= OnPlayerFootStepPerformed;
			}
		}

		private void OnPlayerFootStepPerformed()
		{
			audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
		}
	}
}