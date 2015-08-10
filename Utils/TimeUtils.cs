using System;

using Assets.Scripts.Framework.Component.State.Event;
using Assets.Scripts.Framework.Event;

using UnityEngine;

namespace Assets.Scripts.Framework.Utils
{
	public class TimeUtils
	{
		public static Timer ExecuteAfter(Action action, float seconds, bool pausable = false)
		{
			return new Timer(action, seconds, pausable);
		}
	}

	public class Timer : IEventDispatcher
	{
		public float TimePassed { get; private set; }

		public float ExecuteTime { get; private set; }

		public float TimeRemaining { get { return Mathf.Clamp(ExecuteTime - TimePassed, 0, ExecuteTime); } }

		public bool Completed { get; private set; }

		public EventDispatcher EventDispatcher { get; private set; }

		private bool Pausable { get; set; }

		private Action Callback { get; set; }

		public Timer(Action callback, float seconds, bool pausable = false)
		{
			Callback = callback;
			ExecuteTime = seconds;
			Pausable = pausable;
			TimePassed = 0;
			Completed = false;

			EventDispatcher = new EventDispatcher(GameManager.Instance.EventDispatcher);
			EventDispatcher.AddHandler<GameUpdateEvent>(StateEventType.GameUpdate, OnGameUpdate);
		}

		public void IncreaseTime(float additionalTime)
		{
			ExecuteTime += additionalTime;
		}

		public void Stop()
		{
			EventDispatcher.RemoveHandler<GameUpdateEvent>(StateEventType.GameUpdate, OnGameUpdate);
		}

		private void OnGameUpdate(GameUpdateEvent updateEvent)
		{
			if (Pausable && updateEvent.GamePaused)
			{
				return;
			}

			TimePassed += updateEvent.DeltaTime;

			if (TimePassed > ExecuteTime)
			{
				Completed = true;
				Callback();
				Stop();
			}
		}
	}
}