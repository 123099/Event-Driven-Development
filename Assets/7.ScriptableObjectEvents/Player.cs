using UnityEngine;

namespace _7.ScriptableObjectEvents
{
	// Player is now basically an Actor class that is the main access point for all the components that it depends on and uses.
	// The player is like the puppeteer that combines the logic of all the components it uses.
	// We are basically to where we started! The player now no longer does anything except for hooking its own systems up together and deciding when to enable and disable which one. No more update method!
	public class Player : MonoBehaviour
	{
		private void OnEnable()
		{
			DisableFootSteps();
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