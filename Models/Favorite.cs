using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UserLogin.Models
{
	public class Favorite
	{
		public int favoriteId { get; set; }
        public String Title { get; set; }
		public String Year { get; set; }
		public String imdb_identification { get; set; }
		public String Type { get; set; }
		public String Poster { get; set; }
		public user user_id { get; set; }
	}
}

