namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private readonly Dictionary<Tuple<TKey1,TKey2>,TValue> _map = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        
        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements => _map.Count;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            set => _map.Add(new Tuple<TKey1, TKey2>(key1, key2), value);
            get => _map[new Tuple<TKey1, TKey2>(key1, key2)];
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return _map.Keys
                .Where(t => t.Item1.Equals(key1))
                .Select(t => new Tuple<TKey2, TValue>(t.Item2, _map[t]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return _map.Keys
                .Where(t => t.Item2.Equals(key2))
                .Select(t => new Tuple<TKey1, TValue>(t.Item1, _map[t]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return _map
                .Select(t => new Tuple<TKey1, TKey2, TValue>(t.Key.Item1, t.Key.Item2, t.Value))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            var keys2Array = keys2.ToArray();   // Preventing possible multiple enumeration warning.
            foreach (var k1 in keys1)
            {
                foreach (var k2 in keys2Array)
                {
                    _map[new Tuple<TKey1, TKey2>(k1, k2)] = generator(k1, k2);
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            var testMap = other as Map2D<TKey1, TKey2, TValue>;
            
            if (testMap is null || testMap.GetType() != GetType())
            {
                return false;
            }
            
            return _map.Count.Equals(testMap._map.Count) && _map.SequenceEqual(testMap._map);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => HashCode.Combine(_map.Keys, _map.Values);

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString() => _map.ToString();
    }
}
