using _8.EventManagerBased.Events;
using UnityEngine;

namespace _8.EventManagerBased
{
	// Player is now basically an Actor class that is the main access point for all the components that it depends on and uses.
	// The player is like the puppeteer that combines the logic of all the components it uses.
	// We are basically to where we started! The player now no longer does anything except for hooking its own systems up together and deciding when to enable and disable which one. No more update method!
	public class Player : MonoBehaviour
	{
		private void OnEnable()
		{
			DisableFootSteps();
			
			// The problem is that this listens to ANY and ALL movement events, and not just of a specific instance.
			// This system is great for global game events like PlayerDeathEvent or LevelClearEvent, EnemySpawnEvent, etc.
			// It is possible to create specific events as well, but then the number of event classes just keeps increasing.
			// For instance specific events, it is best to either pass in the object raising the event as an argument in the event class or to use Actions as instance variables.
			EventManager.AddListener<StartedMovingGameEvent>(EnableFootSteps);
			EventManager.AddListener<StoppedMovingGameEvent>(DisableFootSteps);
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<StartedMovingGameEvent>(EnableFootSteps);
			EventManager.RemoveListener<StoppedMovingGameEvent>(DisableFootSteps);
		}

		public void EnableFootSteps()
		{
			var footSteps = GetComponent<FootSteps>();
			
			if (footSteps)
			{
				footSteps.enabled = true;
			}
		}

		public void DisableFootSteps()
		{
			var footSteps = GetComponent<FootSteps>();
			
			if (footSteps)
			{
				footSteps.enabled = false;
			}
		}
	}
}