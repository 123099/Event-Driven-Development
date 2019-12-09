﻿using ScriptableObjectEvents.Events;
using ScriptableObjectEvents.Utils.CustomUnityEvents;
using UnityEngine;

namespace ScriptableObjectEvents
{
	public class Attack : MonoBehaviour
	{
		public IntGameEvent DealtDamageEvent;

		[SerializeField] private int damage = 5;
		
		private void Update()
		{
			if (!Input.GetButtonDown("Fire1"))
			{
				return;
			}

			// We can now check if the target has a health component, and only then attack it. Anything that has a health, can take damage. Everything else is ignored.
			// We are no longer limited to only the player having health.
			// One example would be to raycast forward, and check if the hit target has the health component. To keep it simple, we will just use the health component on this object.
			var health = GetComponent<Health>();
			
			if (!health)
			{
				return;
			}
			
			health.Current -= damage;
			DealtDamageEvent.Invoke(damage);
		}
	}
}