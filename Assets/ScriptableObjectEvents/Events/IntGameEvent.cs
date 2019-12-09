using System;
using UnityEngine;

namespace ScriptableObjectEvents.Events
{
	[CreateAssetMenu(fileName = "New Int Game Event", menuName = "Events/Int Game Event", order = 0)]
	public class IntGameEvent : GenericGameEvent<int> { }
}