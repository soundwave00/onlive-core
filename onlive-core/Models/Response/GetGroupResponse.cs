using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class GetGroupResponse: Response
    {
		  public Groups group { get; set; }
    }
}
