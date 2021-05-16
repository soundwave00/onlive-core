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

        public Users getUser(string username="", string email="")
        {
			Users user = null;
			IQueryable<Users> userTmp;

			using (var context = new ONSTAGEContext())
			{
				
				if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(email)) {
					userTmp = context.Users
						.Select(d => new Users
						{
							Username = d.Username,
							Name = d.Name,
							Surname = d.Surname,
							Password = d.Password,
							Salt = d.Salt,
							Email = d.Email,
							IsActive = d.IsActive
						})
						.Where(x => x.IsActive == true);

					if (!String.IsNullOrEmpty(username)) {
						user = userTmp
							.Where(x => x.Username == username)
							.FirstOrDefault();
					}
					
					if (!String.IsNullOrEmpty(email) && user == null ) {
						user = userTmp
							.Where(x => x.Email == email)
							.FirstOrDefault();
					}
				}
			}

			return user;
        }

		public void signup(Users req)
        {
			using (var context = new ONSTAGEContext())
			{
				context.Users.Add(req);
				context.SaveChanges();
			}
        }

        public Sessions openSession(string username, string codToken)
        {
			Sessions session = new Sessions();
			session.Username = username;
			session.CodToken = codToken;
			session.DateStart = DateTime.Now;
			session.DateExp = DateTime.Now.AddDays(7);

			using (var context = new ONSTAGEContext())
			{
				context.Sessions.Add(session);
				context.SaveChanges();
			}

			return session;
		}

        public void closeSession(string username, string codToken)
        {
			using (var context = new ONSTAGEContext())
			{
				Sessions session = context.Sessions
					.Where(x => x.Username == username)
					.Where(x => x.CodToken == codToken)
					.FirstOrDefault();

				context.Sessions.Remove(session);
				context.SaveChanges();
			}
		}

        public Sessions getSession(Sessions session)
        {
			Sessions response = new Sessions();

			using (var context = new ONSTAGEContext())
			{
				response = context.Sessions
					.Where(x => x.CodToken == session.CodToken)
					.Where(x => x.Username == session.Username)
					.FirstOrDefault();
			}

			return response;
		}

		#endregion
    }
}
