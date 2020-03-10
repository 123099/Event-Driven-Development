using UnityEngine;

namespace _7.ScriptableObjectEvents
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

		public void PlayRandomFootStepSound()
		{
			audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
		}
	}
}