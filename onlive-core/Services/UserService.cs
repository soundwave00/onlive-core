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

        public LoginResponse login(Users req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			LoginResponse loginResponse = new LoginResponse();

			Users user = new Users();
			Sessions session = new Sessions();

			try
			{
				user = userDataAccess.getUser(req.Username);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting user");
            }

			if (user == null)
				throw new Exception("Username does not exist");

			string salt = user.Salt;
			string hashSource = req.Password + salt;

			if(!verifyHash(hashSource, user.Password)){
				throw new Exception("Login failed");
			} else {
				string codiceToken = createSalt(16);
				
				try
				{
					session = userDataAccess.openSession(req.Username, codiceToken);
				}
				catch (Exception exc)
				{
					throw new Exception("Error opening session");
				}
			}

			user.Salt = null;
			user.Password = null;

			loginResponse.body.user = user;
			loginResponse.body.session = session;

			return loginResponse;
        }

		public void signup(Users req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			Users user = new Users();

			validationSignupRequest(req);

			try
			{
				user = userDataAccess.getUser(req.Username);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting user");
            }
			
			if (user != null)
				throw new Exception("Username already exist");

			string salt = createSalt(16);
			string hashSource = req.Password + salt;
			
			req.DateCreate = DateTime.Now;
			req.IsActive = true;
			req.Salt = salt;
			req.Password = getHash(hashSource);

			try
			{
				userDataAccess.signup(req);
			}
			catch (Exception exc)
            {
                throw new Exception("Error creating user");
            }
        }
		
		#endregion

		#region private Method

		private static string createSalt(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			
			return new string(
				Enumerable.Repeat(chars, length)
					.Select(s => s[random.Next(s.Length)])
					.ToArray()
			);
		}

		private static string getHash(string input)
		{
			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			using (SHA256 sha256Hash = SHA256.Create())
            {
				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Loop through each byte of the hashed data
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
			}
			
			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		private static bool verifyHash(string input, string hash)
		{
			// Create a StringComparer an compare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			// Hash the input.
			var hashOfInput = getHash(input);

			return comparer.Compare(hashOfInput, hash) == 0;
		}

		private Boolean checkSpecialChar(string value)
		{
			var regexItem = new Regex("^[a-zA-Z0-9]*$");

			// se IsMatch = true vuol dire che ci sono solo caratteri indicati nella Regex
			if(regexItem.IsMatch(value)) 
				return false;
			
			return true;
		}

		private void validationSignupRequest(Users req)
		{
			if (string.IsNullOrEmpty(req.Username))
				throw new Exception("Username is null or empty");
			
			if (checkSpecialChar(req.Username))
				throw new Exception("Username cannot contain special characters");

			if (string.IsNullOrEmpty(req.Name))
				throw new Exception("Name is null or empty");

			if (checkSpecialChar(req.Name))
				throw new Exception("Name cannot contain special characters");
			
			if (string.IsNullOrEmpty(req.Surname))
				throw new Exception("Surname is null or empty");
			
			if (checkSpecialChar(req.Surname))
				throw new Exception("Surname cannot contain special characters");
			
			if (string.IsNullOrEmpty(req.Password))
				throw new Exception("Password is null or empty");
			
			if (req.Password.Length < 8)
				throw new Exception("Password must be at least 8 characters long");
			
			if (string.IsNullOrEmpty(req.Email))
				throw new Exception("Email is null or empty");
		}

		#endregion
    }
}
