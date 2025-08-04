using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class List<T> : IListTypeLookup<T> where T : class
    {
        private readonly System.Collections.Generic.List<T> _list = new System.Collections.Generic.List<T>();

        public void Register(T item)
        {
            _list.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}