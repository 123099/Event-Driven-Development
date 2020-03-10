using System;
using UnityEngine;

namespace _7.ScriptableObjectEvents.Events
{
	[CreateAssetMenu(fileName = "New Game Event", menuName = "Events/Game Event", order = 0)]
	public class GameEvent : ScriptableObject
	{
		public Action Invoked;

		public void Invoke() => Invoked?.Invoke();
	}
}