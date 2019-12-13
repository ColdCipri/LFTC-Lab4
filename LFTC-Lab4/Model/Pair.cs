using System;
using System.Collections.Generic;
using System.Text;

namespace LFTC_Lab4.Model
{
    public class Pair<K,V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public Pair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return Key + " -> " + Value;
        }

        public string ToString2()
        {
            return Key.ToString() + Value.ToString();
        }
    }
}
