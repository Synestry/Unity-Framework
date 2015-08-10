using System.Collections.Generic;

using Assets.Scripts.Framework.Component.State.Event;
using Assets.Scripts.Framework.Component.UI.Event;
using Assets.Scripts.Framework.Event;
using Assets.Scripts.Framework.Utils.Extensions;

using UnityEngine;

namespace Assets.Scripts.Framework.Component.UI
{
	public class UIComponent<TUIView> : AbstractGameComponent
	{
		public GameObject UIRoot { get; private set; }

		private Dictionary<TUIView, IUIController> Views { get; set; }

		private List<IUIController> OpenViews { get; set; }

		private Camera UICamera { get; set; }

		public UIComponent()
		{
			UIRoot = UIGameObjects.UIRoot.Find();
			OpenViews = new List<IUIController>();
			Views = new Dictionary<TUIView, IUIController>();

			//Ensure that atleast one camera is always in the scene so the UI is rendered correctly
			//Fixes issue that occurs in IOS / Android devices.
			UICamera = UIRoot.AddComponent<Camera>();

			EventDispatcher.AddHandler(StateEventType.GameStarted, OnGameStarted);
			EventDispatcher.AddHandler(StateEventType.GameEnded, OnGameEnded);
			EventDispatcher.AddHandler(StateEventType.GameRestarted, OnGameEnded);
		}

		private void OnGameStarted(GameEvent gameEvent)
		{
			UICamera.enabled = false;
		}

		private void OnGameEnded(GameEvent gameEvent)
		{
			UICamera.enabled = true;
		}

		public void OpenView(TUIView uiView)
		{
			IUIController view = Views[uiView];
			view.Open();
		}

		public void CloseView(TUIView uiView)
		{
			IUIController view = Views[uiView];
			if (view.IsOpen)
			{
				view.Close();
			}
		}

		public T Get<T>(TUIView uiView) where T : IUIController
		{
			return (T)Views[uiView];
		}

		public override void Update(float dt)
		{
			OpenViews.ForEach(
				uiv =>
				{
					if (uiv.IsOpen)
					{
						uiv.Update(dt);
					}
				});
		}

		protected void AddView(TUIView viewId, IUIController viewController)
		{
			viewController.OnOpened += OnViewOpened;
			viewController.OnClosed += OnViewClosed;

			Views.Add(viewId, viewController);
		}

		protected void OnViewOpened(IUIController controller)
		{
			OpenViews.Add(controller);
			EventDispatcher.Dispatch(UIEventType.ViewOpened, new UIEvent { Controller = controller });
		}

		protected void OnViewClosed(IUIController controller)
		{
			OpenViews.Remove(controller);
			EventDispatcher.Dispatch(UIEventType.ViewClosed, new UIEvent { Controller = controller });
		}
	}
}