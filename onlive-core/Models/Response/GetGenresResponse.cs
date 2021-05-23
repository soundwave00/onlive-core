using System.Collections.Generic;

using onlive_core.Entities;
using onlive_core.DbModels;

namespace onlive_core.Models
{
    public class GetGenresResponse: Response
    {
		  public List<Genres> genres { get; set; }
		  public List<int> userGenres { get; set; }
    }
}
