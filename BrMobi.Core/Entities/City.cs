namespace BrMobi.Core.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }

        public City()
        {
        }

        public City(string name, State state)
        {
            Name = name;
            State = state;
        }
    }
}