using ScriptableObjectEvents.Events;
using ScriptableObjectEvents.Utils.CustomUnityEvents;
using UnityEngine;

namespace ScriptableObjectEvents
{
	public class Health : MonoBehaviour
	{
		public IntGameEvent Changed;
		
		[SerializeField] private int maximum = 100;

		private int current;
		// You don't have to worry about calling extra functions that should be updated when the health changes, like UI, death, spawning, level completion, etc.
		// This is all handled automatically when the event is called. All you need to worry about is actually changing the health :)
		public int Current
		{
			get => current;
			set
			{
				current = value;
				Changed.Invoke(value);
			}
		}

		private void Start()
		{
			Current = maximum;
		}
	}
}