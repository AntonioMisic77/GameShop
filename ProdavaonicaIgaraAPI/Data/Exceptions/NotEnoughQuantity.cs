namespace ProdavaonicaIgaraAPI.Data.Exceptions
{
    public class NotEnoughQuantity : Exception
    {
        #region ctor
        public NotEnoughQuantity(string message) : base(message)
        {
        }
        #endregion
    }
}
