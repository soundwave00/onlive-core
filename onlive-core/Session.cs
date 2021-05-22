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

namespace onlive_core
{
    public class Session
    {
		public static void checkCodToken(Context ctx)
		{
			UserDataAccess userDataAccess = new UserDataAccess();

			Sessions session = new Sessions();

			try
			{
				session = userDataAccess.getSession(ctx.session);
			}
			catch (Exception exc)
            {
                throw new Exception("Error getting token", exc.InnerException);
            }

			if( session == null )
                throw new Exception("Token not found");

			if( session.DateExp.CompareTo(DateTime.Now) < 0 )
                throw new Exception("Token expired");
			
		}
	}
}
