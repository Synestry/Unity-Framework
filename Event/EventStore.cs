using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Framework.Event
{
	public class EventStore
	{
		public delegate void CustomEventHandler<in TGameEvent>(TGameEvent gameEvent) where TGameEvent : GameEvent;

		public delegate void EventHandler(GameEvent gameEvent);

		protected Dictionary<Enum, List<EventHandler>> Handlers { get; set; }

		protected Dictionary<Delegate, Dictionary<Enum, EventHandler>> CustomEventHandlers { get; set; }

		private EventStore Parent { get; set; }

		public EventStore(EventStore parent = null)
		{
			CustomEventHandlers = new Dictionary<Delegate, Dictionary<Enum, EventHandler>>();
			Handlers = new Dictionary<Enum, List<EventHandler>>();

			Parent = parent;
		}

		public void AddHandler(Enum[] eventTypes, EventHandler handler)
		{
			eventTypes.ToList().ForEach(et => AddHandler(et, handler));
		}

		public void AddHandler<TGameEvent>(Enum[] eventTypes, CustomEventHandler<TGameEvent> handler) where TGameEvent : GameEvent
		{
			eventTypes.ToList().ForEach(et => AddHandler(et, handler));
		}

		public CustomEventHandler<TGameEvent> AddHandler<TGameEvent>(Enum eventType, CustomEventHandler<TGameEvent> handler) where TGameEvent : GameEvent
		{
			EventHandler customHandler = gameEvent => handler(gameEvent as TGameEvent);

			//Store the delegate handler so that we can remove it if required.
			//Custom event handlers holds a dictionary as multiple events can be assigned to the same handler.
			if (!CustomEventHandlers.ContainsKey(handler))
			{
				CustomEventHandlers[handler] = new Dictionary<Enum, EventHandler>();
			}

			CustomEventHandlers[handler][eventType] = customHandler;

			AddHandler(eventType, customHandler);

			return handler;
		}

		public EventHandler AddHandler(Enum eventType, EventHandler handler)
		{
			if (!Handlers.ContainsKey(eventType))
			{
				var opcodeHandlers = new List<EventHandler>();
				Handlers.Add(eventType, opcodeHandlers);
			}

			List<EventHandler> handlersForEventType = Handlers[eventType];
			handlersForEventType.Add(handler);

			if (Parent != null)
			{
				Parent.AddHandler(eventType, handler);
			}

			return handler;
		}

		public void RemoveHandler<TGameEvent>(Enum[] eventTypes, CustomEventHandler<TGameEvent> handler) where TGameEvent : GameEvent
		{
			eventTypes.ToList().ForEach(et => RemoveHandler(et, handler));
		}

		public void RemoveHandler<TGameEvent>(Enum eventType, CustomEventHandler<TGameEvent> handler) where TGameEvent : GameEvent
		{
			if (CustomEventHandlers.ContainsKey(handler))
			{
				Dictionary<Enum, EventHandler> customHandler = CustomEventHandlers[handler];

				if (customHandler.ContainsKey(eventType))
				{
					RemoveHandler(eventType, customHandler[eventType]);
				}
			}
		}

		public void RemoveHandler(Enum eventType, EventHandler handler)
		{
			List<EventHandler> handlersForEventType = Handlers[eventType];
			handlersForEventType.Remove(handler);

			if (Parent != null)
			{
				Parent.RemoveHandler(eventType, handler);
			}

			if (handlersForEventType.Count == 0)
			{
				Handlers.Remove(eventType);
			}
		}

		public void RemoveAllHandlers()
		{
			//Linq breaks AOT Compilation on IOS 8 Devices.
			//Use while loop to ensure all handlers are removed even when modified during enumeration
			while (CustomEventHandlers.Count > 0)
			{
				KeyValuePair<Delegate, Dictionary<Enum, EventHandler>> eventCallbacks = CustomEventHandlers.First();

				while (eventCallbacks.Value.Count > 0)
				{
					KeyValuePair<Enum, EventHandler> eventCallback = eventCallbacks.Value.First();
					RemoveHandler(eventCallback.Key, eventCallback.Value);
					eventCallbacks.Value.Remove(eventCallback.Key);
				}

				CustomEventHandlers.Remove(eventCallbacks.Key);
			}

			while (Handlers.Count > 0)
			{
				KeyValuePair<Enum, List<EventHandler>> eventCallbacks = Handlers.First();

				while (eventCallbacks.Value.Count > 0)
				{
					EventHandler eventCallback = eventCallbacks.Value.First();
					RemoveHandler(eventCallbacks.Key, eventCallback);
					eventCallbacks.Value.Remove(eventCallback);
				}

				Handlers.Remove(eventCallbacks.Key);
			}
		}
	}
}