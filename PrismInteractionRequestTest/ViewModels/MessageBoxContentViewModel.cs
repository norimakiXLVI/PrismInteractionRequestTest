using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace PrismInteractionRequestTest.ViewModels
{
	public class MessageBoxContentViewModel : BindableBase, IInteractionRequestAware
	{
		public Action FinishInteraction { get; set; }

		private INotification notification;
		public INotification Notification
		{
			get
			{
				return notification;
			}
			set
			{
				if (value is MessageBoxNotification info)
				{
					UpdateNotification(info);
				}

				notification = value;
			}
		}

		public DelegateCommand ConfirmCommand { get; }
		public DelegateCommand CancelCommand { get; }

		private string content;
		public string Content
		{
			get { return content; }
			set { this.SetProperty(ref content, value); }
		}

		private BitmapSource iconImage;
		public BitmapSource IconImage
		{
			get { return iconImage; }
			set { this.SetProperty(ref iconImage, value); }
		}

		private Visibility iconVisibility;
		public Visibility IconVisibility
		{
			get { return iconVisibility; }
			set { this.SetProperty(ref iconVisibility, value); }
		}

		private Visibility cancelVisibility;
		public Visibility CancelVisibility
		{
			get { return cancelVisibility; }
			set { this.SetProperty(ref cancelVisibility, value); }
		}

		public MessageBoxContentViewModel()
		{
			ConfirmCommand = new DelegateCommand(() =>
			{
				if (notification is MessageBoxNotification info)
				{
					info.Confirmed = true;
				}
				FinishInteraction();
			});

			CancelCommand = new DelegateCommand(() =>
			{
				if (notification is MessageBoxNotification info)
				{
					info.Confirmed = false;
				}
				FinishInteraction();
			});
		}

		private void UpdateNotification(MessageBoxNotification info)
		{
			info.Confirmed = false;

			Content = info.Content.ToString();

			Icon icon = null;
			switch (info.Icon)
			{
				case MessageBoxNotification.IconType.Error:
					icon = SystemIcons.Error;
					break;
				case MessageBoxNotification.IconType.Warning:
					icon = SystemIcons.Warning;
					break;
				case MessageBoxNotification.IconType.Information:
					icon = SystemIcons.Information;
					break;
				case MessageBoxNotification.IconType.Exclamation:
					icon = SystemIcons.Exclamation;
					break;
				case MessageBoxNotification.IconType.Question:
					icon = SystemIcons.Question;
					break;
				case MessageBoxNotification.IconType.None:
				default:
					break;
			}

			if (icon != null)
			{
				IconVisibility = Visibility.Visible;
				var image = Imaging.CreateBitmapSourceFromHIcon(
							icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				image.Freeze();
				IconImage = image;
				icon.Dispose();
			}
			else
			{
				IconVisibility = Visibility.Collapsed;
			}

			if (info.Button == MessageBoxNotification.ButtonType.Ok)
			{
				CancelVisibility = Visibility.Collapsed;
			}
			else
			{
				CancelVisibility = Visibility.Visible;
			}
		}
	}
}

