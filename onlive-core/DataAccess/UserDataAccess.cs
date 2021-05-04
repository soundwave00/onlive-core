﻿using System;
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

        public Users getUser(string username)
        {
			Users user = new Users();

			using (var context = new onliveContext())
			{
				user = context.Users
					.Select(d => new Users
					{
						Username = d.Username,
						Name = d.Name,
						Surname = d.Surname,
						Email = d.Email,
						Password = d.Password,
						Salt = d.Salt
					})
					.Where(x => x.Username == username)
					.FirstOrDefault();
			}

			return user;
        }

		public void signup(Users req)
        {
			using (var context = new onliveContext())
			{
				context.Users.Add(req);
				context.SaveChanges();
			}
        }

        public Sessions openSession(string username, string codiceToken)
        {
			Sessions session = new Sessions();
			session.Username = username;
			session.CodiceToken = codiceToken;
			session.DateStart = DateTime.Now;
			session.DateExp = DateTime.Now.AddDays(7);

			using (var context = new onliveContext())
			{
				context.Sessions.Add(session);
				context.SaveChanges();
			}

			return session;
		}

		#endregion
    }
}