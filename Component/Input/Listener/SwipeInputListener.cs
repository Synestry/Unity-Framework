using System.Linq;

using Assets.Scripts.Framework.Component.Input.Event;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.Input.Listener
{
	public class SwipeInputListener : InputListener
	{
		private const float MinSwipeDistance = 10.0f;

		private const float MaxSwipeTime = 0.5f;

		private Vector2 fingerStartPos = Vector2.zero;

		private float? fingerStartTime;

		private bool isSwipe;

		public override void ProcessInput()
		{
			Touch[] touches = UnityEngine.Input.touches;
			if (touches.Count() <= 0)
			{
				return;
			}

			foreach (Touch touch in touches)
			{
				switch (touch.phase)
				{
					case TouchPhase.Began:

						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;

					case TouchPhase.Canceled:
					case TouchPhase.Ended:
						if (!isSwipe)
						{
							InputEvents.Enqueue(new InputEvent { Type = InputEventType.Click });
						}
						else
						{
							isSwipe = false;
							fingerStartTime = null;
						}

						break;

					case TouchPhase.Moved:

						//Edge case for if we dropped a frame and the began event is missed
						if (fingerStartTime == null)
						{
							isSwipe = true;
							fingerStartTime = Time.time;
							fingerStartPos = touch.position;
						}

						float swipeTime = Time.time - fingerStartTime.Value;
						float swipeDistance = (touch.position - fingerStartPos).magnitude;

						if (isSwipe && swipeTime < MaxSwipeTime && swipeDistance > MinSwipeDistance)
						{
							Vector2 direction = touch.position - fingerStartPos;

							if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
							{
								Vector2 swipeDirection = Vector2.right * Mathf.Sign(direction.x);

								InputEvents.Enqueue(
									swipeDirection.x > 0.0f ? new InputEvent { Type = InputEventType.SwipeRight } : new InputEvent { Type = InputEventType.SwipeLeft });
								isSwipe = false;
							}
							else
							{
								Vector2 swipeDirection = Vector2.up * Mathf.Sign(direction.y);

								InputEvents.Enqueue(
									swipeDirection.y > 0.0f ? new InputEvent { Type = InputEventType.SwipeUp } : new InputEvent { Type = InputEventType.SwipeDown });
								isSwipe = false;
							}
						}
						break;
				}
			}
		}
	}
}