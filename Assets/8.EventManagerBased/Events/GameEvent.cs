﻿namespace _8.EventManagerBased.Events
{
	/// <summary>
	/// Base class for events handled by <see cref="EventHandler{TEvent}"/> and <see cref="ParameterlessEventHandler"/>
	/// This class represents the information and type of event that was raised, and allows event consumption.
	/// </summary>
	public abstract class GameEvent
	{
		/// <summary>
		/// Whether this event was consumed.
		/// When an event is consumed, the <see cref="EventManager"/> immediately stops raising this event to any remaining <see cref="EventHandler"/>s.
		/// </summary>
		/// <remarks>
		/// This is great for situations such as UI that should handle an input event but prevent it from propagating further into the game world.
		/// </remarks>
		public bool Consumed { get; private set; }

		/// <summary>
		/// Consumes this event.
		/// See <see cref="Consumed"/> for more information.
		/// </summary>
		public void Consume()
		{
			Consumed = true;
		}
	}
}