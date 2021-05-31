namespace Lib.Domain
{
    public record Message
    {
        public string Code { get; init; }
        public string Text { get; init; }

        public Message(string code, string text)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new System.ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new System.ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
            }

            this.Code = code;
            this.Text = text;
        }

        public override string ToString()
        {
            return $"{this.Code}: {this.Text}";
        }
    }
}
