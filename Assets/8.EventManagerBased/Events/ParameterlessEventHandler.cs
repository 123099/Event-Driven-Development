﻿using System;

 namespace _8.EventManagerBased.Events
{
	/// <summary>
	/// Implementation of the <see cref="EventHandler"/> class that handles <see cref="GameEvent"/> events.
	/// The only difference between this and <see cref="EventHandler{TEvent}"/> is that this accepts callbacks that take in no parameters at all.
	/// This is useful for situations where you do not care about the event information, but only about the fact that it happened, and would like to directly hook in a pre-existing method as a callback, such as updating UI.
	/// </summary>
	internal class ParameterlessEventHandler : EventHandler
	{
		/// <summary>
		/// Creates a new parameterless event handler with a callback that takes no parameters and a priority order.
		/// </summary>
		/// <param name="callback">The callback action that takes no parameters</param>
		/// <param name="priorityOrder">The importance of this event handler</param>
		public ParameterlessEventHandler(Action callback, int priorityOrder) : base(callback, priorityOrder) { }

		/// <summary>
		/// Invokes the callback.
		/// </summary>
		public void Invoke()
		{
			(callback as Action)?.Invoke();
		}
	}
}
