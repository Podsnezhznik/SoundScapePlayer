using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1_Base
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public AppContext() : base("DefaultConnection")
        {  }
        
    }
}
