using Assets.Scripts.Framework.Component.State;
using Assets.Scripts.Framework.Event;

namespace Assets.Scripts.Framework.Component
{
	public abstract class AbstractGameComponent : IEventDispatcher
	{
		public GameManager Manager { get { return GameManager.Instance; } }

		public bool GamePaused { get { return Manager.Get<StateComponent>().GamePaused; } }

		public bool Enabled { get; set; }

		protected AbstractGameComponent()
		{
			EventDispatcher = new EventDispatcher(Manager.EventDispatcher);
			Enabled = true;
		}

		public EventDispatcher EventDispatcher { get; private set; }

		public virtual void Update(float dt) {}

		public virtual void LateUpdate(float dt) {}

		public virtual void FixedUpdate(float dt) {}
	}
}