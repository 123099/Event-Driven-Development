using UnityEngine;

namespace _5.ActionEventBasedAdvancedWithSendMessage
{
	[RequireComponent(typeof(Player), typeof(AudioSource))]
	public class PlayerSounds : MonoBehaviour
	{
		private AudioSource audioSource;
		[SerializeField] private AudioClip[] footStepSounds;

		private void Awake()
		{
			audioSource = GetComponent<AudioSource>();
		}

		private void OnFootStepPerformed()
		{
			audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
		}
	}
}