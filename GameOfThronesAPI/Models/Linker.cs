namespace GameOfThronesAPI.Models
{
    /// <summary>
    /// Through linker class we can manage the api page stepping 
    /// </summary>
    public class Linker
    {
        public string Next { get; set; }
        public string Prev { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}
