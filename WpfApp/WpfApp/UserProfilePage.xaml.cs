using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для UserProfilePage.xaml
	/// </summary>
	public partial class UserProfilePage : Page
	{
		private Пользователь currentUser;

		public UserProfilePage(Пользователь user)
		{
			InitializeComponent();
			currentUser = user;
			DataContext = currentUser;
			LoadPhoto();
		}

		private void LoadPhoto()
		{
			if (currentUser.Фото != null && currentUser.Фото.Length > 0)
			{
				using (var ms = new MemoryStream(currentUser.Фото))
				{
					var image = new BitmapImage();
					image.BeginInit();
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.StreamSource = ms;
					image.EndInit();
					PhotoIm.Source = image;
				}
			}
		}

		private void AddPhoto_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
			{
				string filePath = openFileDialog.FileName;
				byte[] imageData = File.ReadAllBytes(filePath);

				// Отображение выбранного изображения
				PhotoIm.Source = new BitmapImage(new Uri(filePath));
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Обновление пароля, если он был изменен
			if (!string.IsNullOrEmpty(PasswordBox.Password))
			{
				currentUser.ХэшПароль = PasswordBox.Password;
			}

			using (var db = new WarEntities())
			{
				var user = db.Пользователь.FirstOrDefault(u => u.Номер == currentUser.Номер);
				if (user != null)
				{
					user.Фамилия = currentUser.Фамилия;
					user.Имя = currentUser.Имя;
					user.Отчество = currentUser.Отчество;
					user.Логин = currentUser.Логин;
					user.ХэшПароль = currentUser.ХэшПароль;

					if (PhotoIm.Source != null && PhotoIm.Source is BitmapImage bitmapImage)
					{
						using (var ms = new MemoryStream())
						{
							var encoder = new PngBitmapEncoder();
							encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
							encoder.Save(ms);
							user.Фото = ms.ToArray();
						}
					}

					db.SaveChanges();
					LoadPhoto();
					MessageBox.Show("Изменения сохранены.");
				}
			}
		}
	}
}