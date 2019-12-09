﻿using System;

 namespace EventManagerBased.Events
{
	/// <summary>
	/// Base class that allows registering to and handling events.
	/// Event handlers can be ordered by priority, with higher priority event handlers called first.
	/// </summary>
	/// <remarks>
	/// The Event handler wraps a delegate behind the scenes and simply invokes it when the event happens.
	/// The use of delegates means that there is virtually no performance cost.
	/// </remarks>
	internal abstract class EventHandler
	{
		/// <summary>
		/// Determines how important this event handler is. The higher the value, the earlier this event handler gets called when the event it is handling happens.
		/// </summary>
		public int PriorityOrder = 0;

		/// <summary>
		/// The callback delegate that is invoked when the handled event happens.
		/// </summary>
		protected Delegate callback = null;

		/// <summary>
		/// Constructs a new event handler.
		/// </summary>
		/// <param name="callback">The delegate that will act as the callback when the event handled by this handler happens</param>
		/// <param name="priorityOrder">The importance of this event handler. <see cref="PriorityOrder"/> for more information.</param>
		protected EventHandler(Delegate callback, int priorityOrder)
		{
			this.callback = callback;

			PriorityOrder = priorityOrder;
		}

		/// <summary>
		/// Compares this event handler with another object. This only returns true when both objects refer to the same instance.
		/// </summary>
		/// <param name="other">The other object to compare to</param>
		/// <returns>True if the other object is the same instance as this handler</returns>
		public override bool Equals(object other)
		{
			return base.Equals(other);
		}

		/// <summary>
		/// Gets the hashcode for this event handler for use in hash-based containers, such as HashSet or Dictionary.
		/// </summary>
		/// <returns>Hashcode for this object as determined by the base GetHashCode method in C#</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Compares this event handler with a callback delegate.
		/// </summary>
		/// <param name="handler">The event handler whose delegate callback to compare to</param>
		/// <param name="callback">The delegate callback to compare with</param>
		/// <returns>True if the delegate callback of the event handler matches <see cref="callback"/></returns>
		public static bool operator ==(EventHandler handler, Delegate callback)
		{
			return handler.callback == callback;
		}

		/// <summary>
		/// Compares if this event handler and a delegate callback are not equal.
		/// This simply inverts the result of <see cref="operator ==(EventHandler, Delegate)"/>.
		/// </summary>
		/// <param name="handler">THe event handler whose delegate callback to compare to</param>
		/// <param name="callback">The delegate callback to compare with</param>
		/// <returns>True if this handler and <see cref="callback"/> are not equal</returns>
		public static bool operator !=(EventHandler handler, Delegate callback)
		{
			return !(handler == callback);
		}
	}

	/// <summary>
	/// Implementation of the <see cref="EventHandler"/> class that makes the callback delegate act as an <see cref="Action{T}"/> with a <see cref="GameEvent"/> instance as a parameter.
	/// </summary>
	/// <typeparam name="TEvent">The type of event that this handler handles</typeparam>
	internal class EventHandler<TEvent> : EventHandler where TEvent : GameEvent
	{
		/// <summary>
		/// Construct a new EventHandler for GameLabEvents.
		/// </summary>
		/// <param name="callback">The callback action that takes in a <see cref="GameEvent"/> as a parameter</param>
		/// <param name="priorityOrder">The importance of this event handler. <see cref="EventHandler.PriorityOrder"/> for more information</param>
		public EventHandler(Action<TEvent> callback, int priorityOrder) : base(callback, priorityOrder) {}

		/// <summary>
		/// Invokes the callback with the provided event to raise.
		/// </summary>
		/// <param name="eventToRaise">The event to raise</param>
		public void Invoke(TEvent eventToRaise)
		{
			(callback as Action<TEvent>)?.Invoke(eventToRaise);
		}
	}
}
