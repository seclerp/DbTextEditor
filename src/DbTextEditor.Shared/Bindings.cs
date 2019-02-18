using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DbTextEditor.Shared
{
    public static class Bindings
    {
        public static void ForCollection<T>(ObservableCollection<T> collection,
            Action<NotifyCollectionChangedEventArgs> action)
        {
            collection.CollectionChanged += (sender, newValue) => action(newValue);
        }

        public static void ListenObservable<T>(ObservableProperty<T> property, Action<T> action)
        {
            property.ValueChanged += (sender, newValue) => action(newValue);
        }

        public static void BindObservables<TValue>(ObservableProperty<TValue> from, ObservableProperty<TValue> to, BindingMode mode = BindingMode.TwoWay)
        {
            from.ValueChanged += (sender, newValue) => to.Value = newValue;
            if (mode == BindingMode.TwoWay)
            {
                to.ValueChanged += (sender, newValue) => from.Value = newValue;
            }

            to.Value = from.Value;
        }

        public static void BindCollections<TValue>(ObservableCollection<TValue> from, ObservableCollection<TValue> to)
        {
            BindCollections(from, to, value => value);
        }

        public static void BindCollections<TValueFrom, TValueTo>(
            ObservableCollection<TValueFrom> from, ObservableCollection<TValueTo> to,
            Func<TValueFrom, TValueTo> converter)
        {
            from.CollectionChanged += (sender, args) =>
            {
                if (args.OldItems != null)
                {
                    foreach (var oldItem in args.OldItems)
                    {
                        to.Remove(converter((TValueFrom) oldItem));
                    }
                }

                if (args.NewItems != null)
                {
                    foreach (var newItem in args.NewItems)
                    {
                        to.Add(converter((TValueFrom) newItem));
                    }
                }
            };

            to.Clear();
            foreach (var value in from)
            {
                to.Add(converter(value));
            }
        }
    }
}