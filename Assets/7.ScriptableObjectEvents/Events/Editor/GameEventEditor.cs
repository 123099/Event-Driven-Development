using UnityEditor;
using UnityEngine;

namespace _7.ScriptableObjectEvents.Events.Editor
{
	[CustomEditor(typeof(GameEvent))]
	public class GameEventEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (GUILayout.Button("Raise") && target is GameEvent gameEvent)
			{
				gameEvent.Invoke();
			}
		}
	}
}