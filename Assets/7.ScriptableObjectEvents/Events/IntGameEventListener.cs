using ScriptableObjectEvents.Utils.CustomUnityEvents;
using UnityEngine;

namespace _7.ScriptableObjectEvents.Events
{
	public class IntGameEventListener : MonoBehaviour
	{
		[SerializeField] private IntGameEvent gameEvent;
		[SerializeField] private IntUnityEvent response;

		private void OnEnable() => gameEvent.InvokedWithData += response.Invoke;

		private void OnDisable() => gameEvent.InvokedWithData -= response.Invoke;
	}
}