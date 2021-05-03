using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

using onlive_core.Db;
using onlive_core.DbModels;

namespace onlive_core.DataAccess
{
    public class UserDataAccess
    {
        #region Metodi

		/*
        public Users login(Request req)
        {
			Events eventItem = new Events();

			using (var context = new onliveContext())
			{
				eventItem = context.Events
					.Where(x => x.Id == eventId)
					.FirstOrDefault();
			}

            return eventItem;
		}
		*/

        public Boolean checkUser(string req)
        {
			Users user = new Users();

			using (var context = new onliveContext())
			{
				user = context.Users
					.Where(x => x.Username == req)
					.FirstOrDefault();
			}

			if (user != null)
				return true;
			else 
				return false;
        }

		public void signup(Users req)
        {
			using (var context = new onliveContext())
			{
				context.Add(req);
				context.SaveChanges();
			}
        }

		#endregion
    }
}
