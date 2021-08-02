namespace KadOzenka.Dal.YRLParser
{
    public class SalesAgent
    {
        public string Name { get; set; }
        public string[] Phone { get; set; }
        public string Category { get; set; }
        public string Organization { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Partner { get; set; }
    }

    public class Location
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string LocalityName { get; set; }
        public string SubLocalityName { get; set; }
        public string Address { get; set; }
        public string Apartment { get; set; }
        public string Direction { get; set; }
        public string Distance { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string RailwayStation { get; set; }
        public Metro[] Metro { get; set; }
    }

    public class Metro
    {
        public string Name { get; set; }
        public string TimeOnTransport { get; set; }
        public string TimeOnFoot { get; set; }
    }

    public class Space
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }
}