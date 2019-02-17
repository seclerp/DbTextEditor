using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace DbTextEditor.Shared
{
    public static class Bindings
    {
        public static void MakeForCollection<T>(ObservableCollection<T> collection,
            Action<NotifyCollectionChangedEventArgs> action)
        {
            collection.CollectionChanged += (sender, newValue) => action(newValue);
        }

        public static void ListenObservable<T>(ObservableProperty<T> property, Action<T> action)
        {
            property.ValueChanged += (sender, newValue) => action(newValue);
        }

        public static void BindObservables<T>(ObservableProperty<T> from, ObservableProperty<T> to, BindingMode mode = BindingMode.TwoWay)
        {
            from.ValueChanged += (sender, newValue) => to.Value = newValue;
            if (mode == BindingMode.TwoWay)
            {
                to.ValueChanged += (sender, newValue) => from.Value = newValue;
            }

            to.Value = from.Value;
        }
    }
}