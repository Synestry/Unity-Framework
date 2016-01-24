using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.Component;
using Assets.Scripts.Framework.Component.Input;
using Assets.Scripts.Framework.Component.Sound;
using Assets.Scripts.Framework.Component.State;
using Assets.Scripts.Framework.Event;
using Assets.Scripts.Framework.IO.Persistence;
using Assets.Scripts.Framework.IO.Resource;

using UnityEngine;

namespace Assets.Scripts.Framework
{
	public abstract class AbstractGameManager : MonoBehaviour
	{
		public static AbstractGameManager Instance { get; private set; }

		public ResourceManager Resources { get; protected set; }

		public PersistenceManager Persistence { get; set; }

		public List<AbstractGameComponent> Components { get; private set; }

		public EventDispatcher EventDispatcher { get; private set; }

		protected virtual void Awake()
		{
			Instance = this;
			EventDispatcher = new EventDispatcher();
			Components = new List<AbstractGameComponent>();
			Persistence = new PersistenceManager();
            Resources = new ResourceManager();

            Components.Add(new StateComponent());
            Components.Add(new InputComponent());
            Components.Add(new SoundComponent());
		}

		private void Update()
		{
			float dt = Time.deltaTime;
			Components.ForEach(m => { if (m.Enabled) m.Update(dt); });
		}

		private void FixedUpdate()
		{
			float dt = Time.fixedDeltaTime;
			Components.ForEach(m => { if (m.Enabled) m.FixedUpdate(dt); });
		}

		private void LateUpdate()
		{
			float dt = Time.unscaledDeltaTime;
			Components.ForEach(m => { if (m.Enabled) m.LateUpdate(dt); });
		}

		public void Add(AbstractGameComponent gameComponent)
		{
			Components.Add(gameComponent);
		}

		public void Remove(AbstractGameComponent gameComponent)
		{
			Components.Remove(gameComponent);
		}

		public void Remove<T>() where T : AbstractGameComponent
		{
			Components.Remove(Get<T>());
		}

		public T Get<T>() where T : AbstractGameComponent
		{
			return Components.OfType<T>().SingleOrDefault();
		}
	}
}