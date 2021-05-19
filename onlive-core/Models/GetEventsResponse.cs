using onlive_core.Entities;
using onlive_core.DbModels;
using System.Collections.Generic;

namespace onlive_core.Models
{
    public class GetEventsResponse: Response
    {
		  public List<Events> eventsList { get; set; }
    }
}
