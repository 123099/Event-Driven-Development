using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
	public class GameEventListener : MonoBehaviour
	{
		[SerializeField] private GameEvent gameEvent;
		[SerializeField] private UnityEvent action;

		private void OnEnable()
		{
			gameEvent.Raised += action.Invoke;
		}

		private void OnDisable()
		{
			gameEvent.Raised -= action.Invoke;
		}
	}
}