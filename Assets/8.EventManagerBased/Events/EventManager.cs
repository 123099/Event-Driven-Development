using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace _8.EventManagerBased.Events
{
	/// <summary>
	/// Manager class that manages all <see cref="EventHandler"/>s.
	/// This class provides methods for raising specific types of events by class types and adding, getting and removing event listeners.
	/// </summary>
	[DisallowMultipleComponent]
	public static class EventManager
	{
		/// <summary>
		/// Cache dictionary for <see cref="EventHandler"/>s that allows quickly getting a list of handlers by the type of event they are handling.
		/// </summary>
		private static Dictionary<Type, List<EventHandler>> eventTypesToHandlers = new Dictionary<Type, List<EventHandler>>();

		private static readonly MethodInfo genericRaiseEventMethodInfo = null;
		private static readonly Dictionary<Type, MethodInfo> specificRaiseEventMethods = new Dictionary<Type, MethodInfo>();

		/// <summary>
		/// Sets up the event manager.
		/// The most important set up here is the detection of the generic <see cref="RaiseEvent{TEvent}(TEvent)"/> method so that it is possible to raise events through a class <see cref="Type"/> instance.
		/// </summary>
		static EventManager()
		{
			MethodInfo[] publicInstanceMethods = typeof(EventManager).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
			genericRaiseEventMethodInfo = publicInstanceMethods.FirstOrDefault(method => method.IsGenericMethod && method.ContainsGenericParameters && method.Name == nameof(RaiseEvent));
		}

		/// <summary>
		/// Raises an event of type <typeparamref name="TEvent"/> with the <paramref name="eventToRaise"/> event information.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to raise</typeparam>
		/// <param name="eventToRaise">The event information</param>
		public static void RaiseEvent<TEvent>(TEvent eventToRaise) where TEvent : GameEvent
		{
			List<EventHandler> handlers = GetEventHandlers<TEvent>();

			if(handlers.Count == 0)
			{
				return;
			}

			// Make a copy of the list to allow adding and removing listeners in event handling functions
			foreach(EventHandler handler in new List<EventHandler>(handlers))
			{
				if(eventToRaise != null && eventToRaise.Consumed)
				{
					break;
				}

				switch (handler)
				{
					case EventHandler<TEvent> eventHandler:
						eventHandler.Invoke(eventToRaise);
						break;
					case ParameterlessEventHandler parameterlessEventHandler:
						parameterlessEventHandler.Invoke();
						break;
				}
			}
		}

		/// <summary>
		/// Creates an event instance of type <typeparamref name="TEvent"/> with the optional <paramref name="eventToRaiseArguments"/> arguments and raises it.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to raise</typeparam>
		/// <param name="eventToRaiseArguments">List of constructor arguments to create the event instance</param>
		public static void RaiseEvent<TEvent>(params object[] eventToRaiseArguments) where TEvent : GameEvent
		{
			var eventToRaise = Activator.CreateInstance(typeof(TEvent), eventToRaiseArguments) as TEvent;
			RaiseEvent(eventToRaise);
		}

		/// <summary>
		/// Creates and raises an event of type <paramref name="eventToRaiseType"/> with the optional <paramref name="eventToRaiseArguments"/> event information.
		/// The event type must be a child of <see cref="GameEvent"/>.
		/// </summary>
		/// <param name="eventToRaiseType">The type of event to raise</param>
		/// <param name="eventToRaiseArguments">Optional list of constructor arguments to create an instance of the provided type</param>
		/// <remarks>
		/// This method is much much slower compared to <see cref="RaiseEvent{TEvent}(TEvent)"/>. It uses reflection to create and invoke a generic method instance based on the supplied type.
		/// Prefer using <see cref="RaiseEvent{TEvent}(TEvent)"/> when possible!
		/// </remarks>
		public static void RaiseEvent(Type eventToRaiseType, params object[] eventToRaiseArguments)
		{
			if(!eventToRaiseType.IsSubclassOf(typeof(GameEvent)))
			{
				throw new ArgumentException($"Event type must be a child of {typeof(GameEvent).FullName} but was {eventToRaiseType.FullName} instead.", nameof(eventToRaiseType));
			}

			if(!specificRaiseEventMethods.TryGetValue(eventToRaiseType, out MethodInfo raiseEventMethod))
			{
				raiseEventMethod = genericRaiseEventMethodInfo.MakeGenericMethod(eventToRaiseType);
				specificRaiseEventMethods.Add(eventToRaiseType, raiseEventMethod);
			}
			
			raiseEventMethod.Invoke(null, new object[] { Activator.CreateInstance(eventToRaiseType, eventToRaiseArguments) });
		}

		/// <summary>
		/// Adds a <typeparamref name="TEvent"/> event listener that cares about the event information.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to listen to</typeparam>
		/// <param name="listener">Delegate function that accepts the event type as a parameter that will be called when the event is raised</param>
		/// <param name="priorityOrder">The priority of the listener when the event is raised. Higher priority listeners get called first</param>
		public static void AddListener<TEvent>(Action<TEvent> listener, int priorityOrder = 0) where TEvent : GameEvent
		{
			var handler = new EventHandler<TEvent>(listener, priorityOrder);
			AddListenerInternal<TEvent>(handler);
		}

		/// <summary>
		/// Adds a <typeparamref name="TEvent"/> event listener that does not care about the event information.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to listen to</typeparam>
		/// <param name="listener">Delegate function that takes no parameters that will be called when the event is raised</param>
		/// <param name="priorityOrder">The priority of the listener when the event is raised. Higher priority listeners get called first</param>
		public static void AddListener<TEvent>(Action listener, int priorityOrder = 0) where TEvent : GameEvent
		{
			AddListener(typeof(TEvent), listener, priorityOrder);
		}

		public static void AddListener(Type eventType, Action listener, int priorityOrder = 0)
		{
			Assert.IsTrue(eventType.IsSubclassOf(typeof(GameEvent)));
			
			var handler = new ParameterlessEventHandler(listener, priorityOrder);
			AddListenerInternal(eventType, handler);
		}

		/// <summary>
		/// Removes a <typeparamref name="TEvent"/> event listener.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to stop listening to</typeparam>
		/// <param name="listener">The delegate function to remove</param>
		public static void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : GameEvent
		{
			RemoveListenerInternal<TEvent>(listener);
		}

		/// <summary>
		/// Removes a <typeparamref name="TEvent"/> event listener.
		/// </summary>
		/// <typeparam name="TEvent">The type of event to stop listening to</typeparam>
		/// <param name="listener">The delegate function to remove</param>
		public static void RemoveListener<TEvent>(Action listener) where TEvent : GameEvent
		{
			RemoveListenerInternal<TEvent>(listener);
		}

		public static void RemoveListener(Type eventType, Action listener)
		{
			Assert.IsTrue(eventType.IsSubclassOf(typeof(GameEvent)));
			
			RemoveListenerInternal(eventType, listener);
		}

		private static void AddListenerInternal<TEvent>(EventHandler handler) where TEvent : GameEvent
		{
			AddListenerInternal(typeof(TEvent), handler);
		}
		
		private static void AddListenerInternal(Type eventType, EventHandler handler)
		{
			Assert.IsTrue(eventType.IsSubclassOf(typeof(GameEvent)));
			
			if(!eventTypesToHandlers.ContainsKey(eventType))
			{
				eventTypesToHandlers.Add(eventType, new List<EventHandler>());
			}

			var handlers = eventTypesToHandlers[eventType];
			handlers.Add(handler);

			handlers.Sort((firstHandler, secondHandler) => secondHandler.PriorityOrder.CompareTo(firstHandler.PriorityOrder));
		}

		private static void RemoveListenerInternal<TEvent>(Delegate listener) where TEvent : GameEvent
		{
			RemoveListenerInternal(typeof(TEvent), listener);
		}

		private static void RemoveListenerInternal(Type eventType, Delegate listener)
		{
			Assert.IsTrue(eventType.IsSubclassOf(typeof(GameEvent)));
			
			var handlers = GetEventHandlers(eventType);
			var handler = handlers.FirstOrDefault(eventHandler => eventHandler == listener);
			
			handlers.Remove(handler);
		}

		private static List<EventHandler> GetEventHandlers<TEvent>() where TEvent : GameEvent
		{
			return GetEventHandlers(typeof(TEvent));
		}

		private static List<EventHandler> GetEventHandlers(Type eventType)
		{
			Assert.IsTrue(eventType.IsSubclassOf(typeof(GameEvent)));

			return eventTypesToHandlers.TryGetValue(eventType, out var handlers) ? handlers : new List<EventHandler>();
		}
	}
}
