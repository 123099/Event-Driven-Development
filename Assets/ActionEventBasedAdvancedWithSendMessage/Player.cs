using System;
using UnityEngine;

namespace ActionEventBasedAdvancedWithSendMessage
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

		// There is no way to tell whether this message exists, if we spelled it properly, what arguments it takes, etc. without finding the EXACT location where it is sent and looking at the code.
		// What if multiple sources send a message with the exact same name but require different behaviour?
		// There is no reference count, and it is impossible to debug where the call came from or who uses this method.
		// If the SendMessage doesn't require a receiver, it is very hard to find out why a function is not being called.
		// SendMessage is also very expensive because it uses a lot of reflection to accomplish its task.
		private void OnStartedMoving() => EnableFootSteps();
		private void OnStoppedMoving() => DisableFootSteps();

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