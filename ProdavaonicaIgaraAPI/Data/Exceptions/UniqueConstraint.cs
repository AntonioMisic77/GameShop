namespace ProdavaonicaIgaraAPI.Data.Exceptions
{
    public class UniqueConstraint : Exception
    {
        public UniqueConstraint(string message) : base(message) { }
    }
}
