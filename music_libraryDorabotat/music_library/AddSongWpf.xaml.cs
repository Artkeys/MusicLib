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
using System.Windows.Media.Animation;

namespace music_library
{
	/// <summary>
	/// Логика взаимодействия для AddSongWpf.xaml
	/// </summary>
	public partial class AddSongWpf : Window
	{
		AppContext db;
		public AddSongWpf()
		{
			InitializeComponent();

			db = new AppContext();

			var window1 = this.DataContext;


			/*List<Song> songs = db.Songs.ToList();
			string str = "";
			foreach (Song song in songs)
				str += "Исполнитель: " + song.Creator + "  Трек:  " + song.Name;
			Vivod.Text = str;*/
		}

		private void AnimationOfWidth(TextBox textBox, int NewWidth)
		{
			DoubleAnimation WidthAnimation = new DoubleAnimation();
			WidthAnimation.From = Convert.ToInt32(textBox.Width);
			WidthAnimation.To = NewWidth;
			WidthAnimation.Duration = TimeSpan.FromSeconds(1);
			textBox.BeginAnimation(TextBox.WidthProperty, WidthAnimation);
		}
		private void SongName_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongName, 365);
		}

		private void SongGenre_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongGenre, 365);
		}

		private void SongMark_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongMark, 365);
		}

		private void SongComments_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongComments, 365);
		}

		/*private void SongPath_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongPath, 365);
		}*/

		private void SongName_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongName, 200);
		}

		private void SongGenre_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongGenre, 200);
		}

		private void SongMark_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongMark, 200);
		}

		private void SongComments_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongComments, 200);
		}

		/*private void SongPath_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongPath, 200);
		}*/

		private void SongCreator_MouseEnter(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongCreator, 365);
		}

		private void SongCreator_MouseLeave(object sender, MouseEventArgs e)
		{
			AnimationOfWidth(SongCreator, 200);
		}

		private void AddSongBtn_Click(object sender, RoutedEventArgs e)
		{			
			string[] filesName = SongPath.Text.Split('\\');
			string directory = Directory.GetCurrentDirectory();
			directory = directory + @"\Music\";
			MessageBox.Show(SongPath.Text);
			MessageBox.Show(directory);
			Song song = new Song(SongCreator.Text, SongName.Text, SongGenre.Text, SongComments.Text, Convert.ToDouble(SongMark.Text), @"\Music\" + filesName[filesName.Length - 1]);
			File.Copy(SongPath.Text, directory + filesName[filesName.Length-1], true);
			db.Songs.Add(song);
			db.SaveChanges();
			this.DataContext = db.Songs.ToList();
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}
	}
}
