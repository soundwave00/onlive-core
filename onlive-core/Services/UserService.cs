using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;
using onlive_core.Models;

namespace onlive_core.Services
{
    public class UserService
    {
        #region Metodi pubblici

        public LoginResponse login(UserRequest req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			LoginResponse loginResponse = new LoginResponse();

			Users user = new Users();
			Sessions session = new Sessions();

			try
			{
				user = userDataAccess.getUser(req.user.Username);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting user");
            }

			if (user == null)
				throw new Exception("Username does not exist");

			string salt = user.Salt;
			string hashSource = req.user.Password + salt;

			if(!verifyHash(hashSource, user.Password)){
				throw new Exception("Login failed");
			} else {
				string codiceToken = createSalt(16);
				
				try
				{
					session = userDataAccess.openSession(req.user.Username, codiceToken);
				}
				catch (Exception exc)
				{
					throw new Exception("Error opening session");
				}
			}

			loginResponse.session = session;

			return loginResponse;
        }

		public void signup(UserRequest req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			Users user = new Users();

			//validationSignupRequest(req.user);

			if (!checkSpecialChar(req.user.Password))
				throw new Exception("Password is not complex enough");

			try
			{
				user = userDataAccess.getUser(req.user.Username);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting user");
            }
			
			if (user != null)
				throw new Exception("Username already exist");

			string salt = createSalt(16);
			string hashSource = req.user.Password + salt;
			
			req.user.DateCreate = DateTime.Now;
			req.user.IsActive = true;
			req.user.Salt = salt;
			req.user.Password = getHash(hashSource);

			try
			{
				userDataAccess.signup(req.user);
			}
			catch (Exception exc)
            {
                throw new Exception("Error creating user");
            }
        }

		public void logout(Request req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			checkCodToken(req.ctx);

			try
			{
				userDataAccess.closeSession(req.ctx.session.Username, req.ctx.session.CodToken);
			}
			catch (Exception exc)
            {
                throw new Exception("Error closing session");
            }
        }

		public GetUserResponse getUser(Request req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			GetUserResponse getUserResponse = new GetUserResponse();

			Users user = new Users();

			checkCodToken(req.ctx);

			try
			{
				user = userDataAccess.getUser(req.ctx.session.Username);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting user");
            }
			
			if (user == null)
				throw new Exception("Username does not exist");

			user.Salt = null;
			user.Password = null;

			getUserResponse.user = user;

			return getUserResponse;
        }

		public void checkCodToken(Context ctx)
		{
			UserDataAccess userDataAccess = new UserDataAccess();

			Sessions session = new Sessions();

			try
			{
				session = userDataAccess.getSession(ctx.session);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting token");
            }

			if( session == null )
                throw new Exception("Token not found");

			if( session.DateExp.CompareTo(DateTime.Now) < 0 )
                throw new Exception("Token expired");
			
		}
		
		#endregion

		#region private Method

		private string createSalt(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			
			return new string(
				Enumerable.Repeat(chars, length)
					.Select(s => s[random.Next(s.Length)])
					.ToArray()
			);
		}

		private string getHash(string input)
		{
			StringBuilder sBuilder = new StringBuilder();

			using (SHA256 sha256Hash = SHA256.Create())
            {
				byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
			}
			
			return sBuilder.ToString();
		}

		private bool verifyHash(string input, string hash)
		{
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			var hashOfInput = getHash(input);

			return comparer.Compare(hashOfInput, hash) == 0;
		}

		private Boolean checkSpecialChar(string value)
		{
			var regexItem = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}");

			// Se IsMatch == true allora la stringa contiene tutti i caratteri richiesti: [a-z][A-Z][0-9][caratteri speciali]
			if(regexItem.IsMatch(value))
				return true;
			
			return false;
		}

		/*

		private void validationSignupRequest(Users req)
		{
			if (string.IsNullOrEmpty(req.Username))
				throw new Exception("Username is null or empty");

			if (string.IsNullOrEmpty(req.Name))
				throw new Exception("Name is null or empty");
			
			if (string.IsNullOrEmpty(req.Surname))
				throw new Exception("Surname is null or empty");
			
			if (string.IsNullOrEmpty(req.Password))
				throw new Exception("Password is null or empty");
			
			if (req.Password.Length < 8)
				throw new Exception("Password must be at least 8 characters long");
			
			if (string.IsNullOrEmpty(req.Email))
				throw new Exception("Email is null or empty");
		}

		*/

		#endregion
    }
}
