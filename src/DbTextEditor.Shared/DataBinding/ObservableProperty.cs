using System;

namespace DbTextEditor.Shared.DataBinding
{
    public class ObservableProperty<T>
    {
        private readonly Action<T> _afterSet;
        private T _value;

        public ObservableProperty()
        {
        }

        public ObservableProperty(T value) : this()
        {
            _value = value;
        }

        public ObservableProperty(T value, Action<T> afterSet)
        {
            _afterSet = afterSet;
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!AreEqual(_value, value))
                {
                    _value = value;
                    _afterSet?.Invoke(_value);
                    OnValueChanged(_value);
                }
            }
        }

        public event EventHandler<T> ValueChanged;

        protected virtual void OnValueChanged(T newValue)
        {
            ValueChanged?.Invoke(this, newValue);
        }

        public static implicit operator T(ObservableProperty<T> observableProperty)
        {
            return observableProperty.Value;
        }

        private static bool AreEqual(T a, T b)
        {
            return a == null ? b == null : a.Equals(b);
        }
    }
}