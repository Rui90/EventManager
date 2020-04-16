namespace ProAgil.WebApi.Model
{
    public class Event
    {
        public int EventId {get; set;}

        public string Local {get; set;}

        public string Date {get;set;}

        public string Theme {get;set;}

        public int Capacity {get;set;}

        public string Lote {get; set;}

        public string ImageUrl { get; set;}
    }
}