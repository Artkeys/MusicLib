using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_library
{
	class Song
	{
		public int id { get; set; }

		private string creator, name, genre, comments, path;

		private double marks;

		public string Creator
		{
			get { return creator; }
			set { creator = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Genre
		{
			get { return genre; }
			set { genre = value; }
		}

		public string Comments
		{
			get { return comments; }
			set { comments = value; }
		}

		public double Marks
		{
			get { return marks; }
			set { marks = value; }
		}

		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		public Song() { }

		public Song(string creator, string name, string genre, string comments, double marks, string path)
		{
			this.creator = creator;
			this.name = name;
			this.genre = genre;
			this.comments = comments;
			this.marks = marks;
			this.path = path;
		}
	}
}
