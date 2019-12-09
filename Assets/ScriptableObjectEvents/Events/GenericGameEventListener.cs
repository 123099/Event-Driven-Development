using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents.Events
{
	// Could do something like generics, but this does not serialize properly and does not allow for the dynamic feature of Unity Events.
	// It is also very complicated to neatly integrate events that do not care about data and only about the fact that they happened.
	// One way of doing it is having ALL events always pass in data, and for events that don't care about data, have a special Void event type.
	// But this solution starts becoming more and more complicated without much extra benefit.
	// It is perfect for very simple situations where no dynamic data passing is required, or with some repeated code.
	public abstract class GenericGameEventListener<TEvent, TUnityEventResponse> : MonoBehaviour
		where TEvent : GameEvent where TUnityEventResponse : UnityEventBase
	{
		[SerializeField] private TEvent gameEvent;
		[SerializeField] private TUnityEventResponse response;

		
	}
}