using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class LoginResponse: Response
    {
		public Sessions session { get; set; }
    }
}
