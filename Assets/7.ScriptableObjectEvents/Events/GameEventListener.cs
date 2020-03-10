using UnityEngine;
using UnityEngine.Events;

namespace _7.ScriptableObjectEvents.Events
{
	public class GameEventListener : MonoBehaviour
	{
		[SerializeField] private GameEvent gameEvent;
		[SerializeField] private UnityEvent response;

		protected virtual void OnEnable() => gameEvent.Invoked += response.Invoke;

		protected virtual void OnDisable() => gameEvent.Invoked -= response.Invoke;
	}
}