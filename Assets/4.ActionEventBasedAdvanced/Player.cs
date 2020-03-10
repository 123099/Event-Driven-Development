using UnityEngine;

namespace _4.ActionEventBasedAdvanced
{
	// Player is now basically an Actor class that is the main access point for all the components that it depends on and uses.
	// The player is like the puppeteer that combines the logic of all the components it uses.
	// We are basically to where we started! The player now no longer does anything except for hooking its own systems up together and deciding when to enable and disable which one. No more update method!
	public class Player : MonoBehaviour
	{
		private void OnEnable()
		{
			var movement = GetComponent<Movement>();
			
			if (movement)
			{
				movement.StartedMoving += EnableFootSteps;
				movement.StoppedMoving += DisableFootSteps;
			}
			
			DisableFootSteps();
		}

		private void OnDisable()
		{
			var movement = GetComponent<Movement>();
			
			if (movement)
			{
				movement.StartedMoving -= EnableFootSteps;
				movement.StoppedMoving -= DisableFootSteps;
			}
		}

		private void EnableFootSteps()
		{
			var footSteps = GetComponent<FootSteps>();
			
			if (footSteps)
			{
				footSteps.enabled = true;
			}
		}

		private void DisableFootSteps()
		{
			var footSteps = GetComponent<FootSteps>();
			
			if (footSteps)
			{
				footSteps.enabled = false;
			}
		}
	}
}