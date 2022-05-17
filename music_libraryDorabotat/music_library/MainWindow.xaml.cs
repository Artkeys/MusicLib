using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Data.SQLite;
using System.Media;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows.Threading;

namespace music_library
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string directItems = "";
		AppContext db;
		
		public MainWindow()
		{
			InitializeComponent();

			db = new AppContext();
			db.Songs.Load();
			this.DataContext = db.Songs.Local.ToBindingList();
			//List<Song> songs = db.Songs.ToList();
			//ListOfSongs.ItemsSource = songs;
		}

		private void ScanFiles_Click(object sender, RoutedEventArgs e) //Сканирование файлов
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			directItems = "";
			CommonFileDialogResult result = dialog.ShowDialog();
			if (result == CommonFileDialogResult.Ok)
			{
				directItems = dialog.FileName;
				MessageBox.Show(directItems);

			}
			string[] findFiles = Directory.GetFiles(directItems, "*.mp3", System.IO.SearchOption.AllDirectories);
			for (int i = 0; i < findFiles.Length; i++)
			{
				findFiles[i] = findFiles[i].Substring(findFiles[i].LastIndexOf('\\') + 1);
			}
			MusicList.ItemsSource = findFiles;
		}

		private void AddSongs_Click(object sender, RoutedEventArgs e) //Открытие формы
		{
			AddSongWpf addSongWpf = new AddSongWpf()
			{
				ShowInTaskbar = false,
				Owner = Application.Current.MainWindow
			};

			addSongWpf.SongPath.Text = directItems + @"\" + MusicList.SelectedValue;
			addSongWpf.ShowDialog();
			this.DataContext = addSongWpf.DataContext;
		}

		private void PlayBtn_Click(object sender, RoutedEventArgs e) //Включение музыки
		{
            var id = (int)(sender as Button).Tag;

			foreach (Song song in ListOfSongs.ItemsSource)
			{
				if (id == song.id)
				{
					//MessageBox.Show(song.Path);
					//MessageBox.Show(Directory.GetCurrentDirectory());

					mediaElement.Source = new Uri(Directory.GetCurrentDirectory() + song.Path, UriKind.RelativeOrAbsolute);
					mediaElement.Play();
				}
			}	
		}

		private void DeleteSongs_Click(object sender, RoutedEventArgs e)
		{

			if (ListOfSongs.SelectedItem == null) MessageBox.Show("Выберите трек!", "Ошибка!");

			else
			{
				Song song = ListOfSongs.SelectedItem as Song;
				MessageBox.Show($"Вы хотите удалить трек:{song.Creator + "-" + song.Name}");
				db.Songs.Remove(song);
				db.SaveChanges();
			}
		}

		private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
		{
			TimeLineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
		}

		private void TimeLineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			int SliderValue = (int)TimeLineSlider.Value;
			TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
			mediaElement.Position = ts;
		}
	}
}
