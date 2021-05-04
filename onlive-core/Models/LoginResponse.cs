using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class LoginResponseBody
    {
		public Users user { get; set; }
		public Sessions session { get; set; }
    }

    public class LoginResponse: Response
    {
		public LoginResponseBody body { get; set; }

        public LoginResponse()
        {
        	body = new LoginResponseBody();
        }
    }
}
