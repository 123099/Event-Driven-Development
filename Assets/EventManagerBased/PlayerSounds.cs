using System;
using EventManagerBased.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EventManagerBased
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

		private void OnEnable()
		{
			EventManager.AddListener<FootStepPerformedGameEvent>(PlayRandomFootStepSound);
		}

		private void OnDisable()
		{
			EventManager.AddListener<FootStepPerformedGameEvent>(PlayRandomFootStepSound);
		}

		public void PlayRandomFootStepSound()
		{
			audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
		}
	}
}