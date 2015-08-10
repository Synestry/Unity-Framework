using System;
using System.Collections.Generic;

namespace Assets.Scripts.Framework.Event
{
	public class EventDispatcher : EventStore
	{
		private EventDispatcher Parent { get; set; }

		private int LastEventIndex { get; set; }

		public EventDispatcher(EventDispatcher parent = null) : base(parent)
		{
			Parent = parent;
			LastEventIndex = 0;
		}

		public void Dispatch(Enum eventType)
		{
			Dispatch(eventType, new GameEvent());
		}

		public void Dispatch(Enum eventType, GameEvent gameEvent)
		{
			gameEvent.EventIndex = ++LastEventIndex;

			if (Parent != null)
			{
				Parent.Dispatch(eventType, gameEvent);
			}
			else
			{
				if (Handlers.ContainsKey(eventType))
				{
					List<EventHandler> handlersForEventType = Handlers[eventType];

					if (handlersForEventType != null && handlersForEventType.Count > 0)
					{
						handlersForEventType.ForEach(
							h =>
							{
								if (!gameEvent.StopPropagation)
								{
									h(gameEvent);
								}
							});
					}
				}
			}
		}
	}
}