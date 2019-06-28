using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Members.Core.Domain.Entities
{
    public class Members
    {
        public string Email { get; set; }

        public string Gender { get; set; }

        public Name Name { get; set; }

        public string Phone { get; set; }

        public string Cell { get; set; }

        public Location Location { get; set; }
    }

    public class Name
    {
        public string Title { get; set; }

        public string First { get; set; }

        public string Last { get; set; }
    }

    public class Location
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }
    }
}
