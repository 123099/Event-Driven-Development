using System;

namespace _7.ScriptableObjectEvents.Events
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