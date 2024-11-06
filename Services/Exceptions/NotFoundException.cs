namespace Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Об'єкт \"{name}\" ({key}) не було знайдено.") { }
    }
}
