using System;

namespace ScriptableObjectEvents
{
	[UnityEngine.CreateAssetMenu(fileName = "New Game Event", menuName = "Events/Game Event", order = 0)]
	public class GameEvent : UnityEngine.ScriptableObject
	{
		public Action Raised;

		public void Raise()
		{
			Raised?.Invoke();
		}
	}
}