namespace Orders.Domain
{
    public record Cpf
    {
        public string Value { get; init; }

        public Cpf(string value)
        {
            this.Value = value;
        }
    }
}
