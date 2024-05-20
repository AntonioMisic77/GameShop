namespace ProdavaonicaIgaraAPI.Data.Exceptions
{
    public class UniqueConstraint : Exception
    {
        #region ctor
        public UniqueConstraint(string message) : base(message) { }

        #endregion
    }
}
