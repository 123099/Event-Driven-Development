using UnityEngine;

namespace ActionEventBased
{
	// Can now easily disable and enable the effects that we want or don't want.
	// Player no longer depends on all of the "extra features" to function and exist. We just add on things that we need.
	[RequireComponent(typeof(Player))]
	public class PlayerEffects : MonoBehaviour
	{
		[SerializeField] private ParticleSystem footStepParticleEffect;

		private Player player;

		private void Awake()
		{
			player = GetComponent<Player>();
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
			Instantiate(footStepParticleEffect, transform.position, transform.rotation);
		}
	}
}