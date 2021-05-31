namespace Orders.Domain
{
    public record Adress
    {
        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Number { get; set; }

    }
}
