namespace BrMobi.Core.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public State()
        {
        }

        public State(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}