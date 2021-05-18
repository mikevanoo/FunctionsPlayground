using System;

namespace FunctionsPlayground.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Id} {Forename} {Surname}";
        }
    }
}
