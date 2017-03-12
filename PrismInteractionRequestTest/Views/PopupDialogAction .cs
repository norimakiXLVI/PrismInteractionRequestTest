using Prism.Interactivity;
using System.Windows;

namespace PrismInteractionRequestTest.Views
{
	public class PopupDialogAction : PopupWindowAction
	{
		protected override Window CreateWindow()
		{
			return new DialogWindow();
		}
	}
}

