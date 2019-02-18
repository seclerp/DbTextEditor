using System.Collections.ObjectModel;

namespace DbTextEditor.Shared
{
    public class ObservableCollectionHelper
    {
        public static void ClearObservableCollection<T>(ObservableCollection<T> collection)
        {
            while (collection.Count > 0)
            {
                collection.RemoveAt(0);
            }
        }
    }
}