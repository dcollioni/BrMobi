namespace BrMobi.Core.Entities.Map
{
    public class BusLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InfoUrl { get; set; }

        public BusLine()
        {
        }

        public BusLine(string name, string infoUrl)
        {
            Name = name;
            InfoUrl = infoUrl;
        }
    }
}