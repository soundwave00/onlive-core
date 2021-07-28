using onlive_core.Entities;
using onlive_core.DbModels;
using System.Collections.Generic;

namespace onlive_core.Models
{
    public class GetUserResponse: Response
    {
		public Users user { get; set; }
    public List<Users> allUser { get; set; }
    }
}
