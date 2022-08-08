using Assets.state;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateCollection<T> : ScriptableObject, ICollection<T> where T : BaseEntityState
{
    List<T> _collection = new List<T>();

    public int Count => _collection.Count;
    public bool IsReadOnly => false;
    public void Add(T item) => _collection.Add(item);
    public void Clear() => _collection.Clear();
    public bool Contains(T item) => _collection.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();
    public bool Remove(T item) => _collection.Remove(item);
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
}
