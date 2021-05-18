using System;

namespace InfenixTools.DataStructures
{
    [Serializable]
    public struct Bind<TKey, TValue>
    {
        public TKey key;
        public TValue value;

        public Bind(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
