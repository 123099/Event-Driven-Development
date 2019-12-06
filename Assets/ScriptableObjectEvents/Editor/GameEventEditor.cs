using UnityEditor;
using UnityEngine;

namespace ScriptableObjectEvents.Editor
{
	[CustomEditor(typeof(GameEvent))]
	[CanEditMultipleObjects]
	public class GameEventEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (GUILayout.Button("Raise Event"))
			{
				foreach (var gameEvent in targets)
				{
					(gameEvent as GameEvent)?.Raise();
				}
			}
		}
	}
}