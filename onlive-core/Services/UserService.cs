using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using onlive_core.DataAccess;
using onlive_core.DbModels;
using onlive_core.Entities;

namespace onlive_core.Services
{
    public class UserService
    {
        #region Metodi pubblici
		/*
        public Response login(Request req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			//
			Events eventItem = userDataAccess.createEvent(req);
			
			int port = setPort(eventItem.Id);

			int pid = startHomeProcess(port, bash);
			
			homeDataAccess.setStartEvent(eventItem.Id, pid);

			response.rMessage = "Event " + eventItem.Name + " started";

			return response;
        }
		*/
		public void signup(Users req)
        {
			UserDataAccess userDataAccess = new UserDataAccess();

			validationSignupRequest(req);
			
			if (userDataAccess.checkUser(req.Username)) 
				throw new Exception("Username already exist");
			
			req.DateCreate = DateTime.Now;
			req.IsActive = true;

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

		private Boolean checkSpecialChar(string value)
		{
			var regexItem = new Regex("^[a-zA-Z0-9]*$");

			// se IsMatch = true vuol dire che non ci sono i caratteri indicati nella Regex
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
			
			if (checkSpecialChar(req.Password))
				throw new Exception("Password cannot contain special characters");	
			
			if (string.IsNullOrEmpty(req.Email))
				throw new Exception("Email is null or empty");	
			
		}

		#endregion
    }
}