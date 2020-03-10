using UnityEngine;

namespace _3.ActionEventBased
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
			player.FootStepPerformed += OnPlayerFootStepPerformed;
		}

		private void OnDisable()
		{
			player.FootStepPerformed -= OnPlayerFootStepPerformed;
		}

		private void OnPlayerFootStepPerformed()
		{
			audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
		}
	}
}