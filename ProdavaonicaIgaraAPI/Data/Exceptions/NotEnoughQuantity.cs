namespace ProdavaonicaIgaraAPI.Data.Exceptions
{
    public class NotEnoughQuantity : Exception
    {
        public NotEnoughQuantity(string message) : base(message)
        {
        }
    }
}
