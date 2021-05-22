namespace onlive_core.Entities
{
    public class Response
    {
        public int rCode { get; set; }
        public string rTitle { get; set; }
        public string rMessage { get; set; }

		public Response()
		{
			rCode = 0;
			rTitle = "";
			rMessage = "";
		}
    }
}
