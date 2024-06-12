using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1_Base
{
    public class User
    {
        public User()
        {
            
        }

        public User(string login, string password)
        {
            this.login = login;
			this.password = password;
        }

        public string login;

		public string Login
		{
			get { return login; }
			set { login = value; }
		}

        public string password;

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

        public int userId { get; set; }
    }
}
