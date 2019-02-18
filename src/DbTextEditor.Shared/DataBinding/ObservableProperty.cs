using System;

namespace DbTextEditor.Shared.DataBinding
{
    public class ObservableProperty<T>
    {
        private T _value;
        private Action<T> _afterSet;

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

        public ObservableProperty()
        {
        }

        public ObservableProperty(T value) : this()
        {
            Value = value;
        }

        public ObservableProperty(T value, Action<T> afterSet)
        {
            _afterSet = afterSet;
            Value = value;
        }

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