using ScriptableObjectEvents.Events;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
	public class FootSteps : MonoBehaviour
	{
		// Anyone can add and remove listeners from anywhere. BUT, they can also invoke the event from anywhere, which may be undesirable and break encapsulation.
		// Upgrading from Action to UnityEvent is just as simple as switching the variable Type if we are using the Invoke function.
		// Set up is also slow in the editor, as it involves a lot of drag and drop, and many menus.
		public GameEvent Performed;
		
		[SerializeField] private float rate = 3.0f;
		
		private float timeBetweenFootSteps;
		private float timeSinceLastFootStep;

		private void Awake()
		{
			timeBetweenFootSteps = 1.0f / rate;
		}

		// When the foot steps are enabled once again, make it so that the foot step is performed instantly
		private void OnEnable()
		{
			timeSinceLastFootStep = timeBetweenFootSteps;
		}

		// This will not run when the foot steps are disabled
		private void Update()
		{
			if (timeSinceLastFootStep >= timeBetweenFootSteps)
			{
				timeSinceLastFootStep -= timeBetweenFootSteps;
				Performed.Invoke();
			}

			timeSinceLastFootStep += Time.deltaTime;
		}

		private void OnValidate()
		{
			rate = Mathf.Clamp(rate, 0.01f, rate);
		}
	}
}