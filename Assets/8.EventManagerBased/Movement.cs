using _8.EventManagerBased.Events;
using UnityEngine;

namespace _8.EventManagerBased
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float movementSpeed = 10.0f;
		
		private bool wasMovingLastFrame = false;
		
		private void Update()
		{
			var movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

			if (Mathf.Abs(movementInput.sqrMagnitude) < 0.1f)
			{
				if (wasMovingLastFrame)
				{
					// We no longer have footsteps, ui, sound or particle logic in here. All we do and care about is strictly movement.
					// We then let anyone who MAY be listening that we are moving or not, and they can decide what to do.
					EventManager.RaiseEvent(new StoppedMovingGameEvent());
					wasMovingLastFrame = false;
				}
				
				return;
			}

			if (!wasMovingLastFrame)
			{
				EventManager.RaiseEvent(new StartedMovingGameEvent());
				wasMovingLastFrame = true;
			}
			
			transform.Translate(Time.deltaTime * movementSpeed * movementInput.normalized);
		}
	}
}