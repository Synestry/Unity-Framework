using System;

using Assets.Scripts.Framework.Component.Input.Event;

namespace Assets.Scripts.Framework.Component.Input.Listener
{
	public class AccelerometerInputListener : InputListener
	{
		public override void ProcessInput()
		{
			float deadZone = 0.1f;
			float accelerometerValue = UnityEngine.Input.acceleration.x;
			if (Math.Abs(accelerometerValue) > deadZone)
			{
				InputEvents.Enqueue(new AccelerometerInputEvent { Type = InputEventType.Accelerometer, Value = accelerometerValue });
			}
		}
	}
}