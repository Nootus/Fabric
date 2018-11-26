namespace Nootus.Fabric.Mobile.Validations
{
    public class LengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int Length { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return str.Length == Length;
        }
    }
}
