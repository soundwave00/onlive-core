using System.Collections.Generic;

using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class GetGroupRequest: Request
    {
		    public int idEvents { get; set; }
            public Groups Groups { get; set; }
            public List<string> groupComponents { get; set; }
    }
}
