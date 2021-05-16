using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class SignupRequest: Request
    {
		public Users user { get; set; }
    }
}
