namespace FunctionsPlayground.Models
{
    public class PersonRequest
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Forename} {Surname}";
        }
    }
}
