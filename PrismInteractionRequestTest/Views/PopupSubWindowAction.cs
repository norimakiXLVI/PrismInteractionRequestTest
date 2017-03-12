using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;

namespace PrismInteractionRequestTest.Views
{
	public class PopupSubWindowAction : PopupWindowAction
	{
		protected override void Invoke(object parameter)
		{
			var args = parameter as InteractionRequestedEventArgs;
			if (args == null)
			{
				return;
			}

			var info = args.Context as Notification;
			if (info == null)
			{
				return;
			}

			var eventInfo = info.GetType().GetEvent("Closing");
			if (eventInfo == null)
			{
				return;
			}

			var subWindow = new SubWindow();

			if (AssociatedObject != null)
			{
				if (AssociatedObject is Window parent)
				{
					subWindow.Owner = parent;
				}
			}

			EventHandler handlerClosing = null;
			handlerClosing = (s, e) =>
			{
				subWindow.Close();
			};
			eventInfo.AddEventHandler(info, handlerClosing);

			EventHandler handlerClosed = null;
			handlerClosed = (s, e) =>
			{
				subWindow.Closed -= handlerClosed;
				eventInfo.RemoveEventHandler(info, handlerClosing);
			};
			subWindow.Closed += handlerClosed;

			subWindow.Show();
		}
	}
}

