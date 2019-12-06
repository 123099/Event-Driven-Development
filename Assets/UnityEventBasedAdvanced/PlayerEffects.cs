﻿using UnityEngine;

namespace UnityEventBasedAdvanced
{
	// Can now easily disable and enable the effects that we want or don't want.
	// Player no longer depends on all of the "extra features" to function and exist. We just add on things that we need.
	[RequireComponent(typeof(Player))]
	public class PlayerEffects : MonoBehaviour
	{
		[SerializeField] private ParticleSystem footStepParticleEffect;

		public void SpawnFootStepParticleEffect()
		{
			Instantiate(footStepParticleEffect, transform.position, transform.rotation);
		}
	}
}