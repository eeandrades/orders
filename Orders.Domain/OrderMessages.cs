using Lib.Domain;

namespace Orders.Domain
{
    public static class OrderMessages
    {
        public static Message ClientIdEmpty = new Message("E001", "ClientId can´t be empty");

        public static Message OrderMustHaveItem = new Message("E002", "The order must have at least one item ");


        public static Message ClientNotFound = new Message("E003", "Client not found");

        public static Message ProductNotFound = new Message("E004", "Product not found");

        public static Message AmountMustBeGreaterThanZero  = new Message("E005", "Amount must be greater than zero ");

        public static Message OrderNotFount = new Message("E006", "Order not found");
    }
}
