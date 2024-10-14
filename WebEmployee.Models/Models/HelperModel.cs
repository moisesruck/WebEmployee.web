namespace WebEmployee.Models.Models
{
    public static class HelperModel
    {
        public static List<Country> Countries { get; set; } = new List<Country>();
        public static List<State> States { get; set; }= new List<State>();
    }
    public class Country
    {
        public string iso2 { get; set; }
        public string name { get; set; }
    }
    public class State
    {
        public string iso2 { get; set; }
        public string name { get; set; }
    }
}
