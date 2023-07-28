namespace TMS.API.Exceptions
{
    public class EntityNotFoundException : Exception
    {

        public EntityNotFoundException() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception exception) : base(message, exception) { }

        public EntityNotFoundException(long entityId, string entityName) : base(FormattableString.Invariant($"'{entityName}' with id '{entityId}' was not found.")) { }


    }
}
