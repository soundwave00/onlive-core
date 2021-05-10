using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class GetEventResponse: Response
    {
		  public Events eventItem { get; set; }
    }
}
