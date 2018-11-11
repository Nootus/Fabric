using Nootus.Fabric.Mobile.Core;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Validations
{
    public class ValidatableObject<T> : BaseNotifyPropertyChanged
    {
        private readonly List<IValidationRule<T>> validations;

        public ValidatableObject(): this(default(T)) { }

        public ValidatableObject(T value)
        {
            _value = value;
            isValid = true;
            errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> Validations => validations;

        private List<string> errors;
        public List<string> Errors
        {
            get { return errors; }
            set { SetProperty(ref errors, value); }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
