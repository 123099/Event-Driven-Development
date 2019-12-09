using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjectEvents.Events
{
	public abstract class GenericGameEvent<TEventData> : GameEvent
	{
		public Action<TEventData> InvokedWithData;
		
		public void Invoke(TEventData eventData = default)
		{
			base.Invoke();
			InvokedWithData?.Invoke(eventData);
		}
	}
}