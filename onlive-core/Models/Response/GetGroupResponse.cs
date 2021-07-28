using onlive_core.Entities;
using onlive_core.DbModels;
using System.Collections.Generic;

namespace onlive_core.Models
{
    public class GetGroupResponse: Response
    {
		  public Groups group { get; set; }
          public List<int> userGroup { get; set; }
          public List<GroupsMembers> membersGroup{ get; set; }
    }
}
