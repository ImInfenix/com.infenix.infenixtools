using System.Collections.Generic;
using UnityEngine.Assertions;

namespace InfenixTools.DataStructures
{
    public class Binder<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> bindings;

        public int Count => bindings.Count;

        public Binder()
        {
            bindings = new Dictionary<TKey, TValue>();
        }

        public Binder(List<Bind<TKey, TValue>> bindingsToCreate)
        {
            bindings = new Dictionary<TKey, TValue>();

            if (bindingsToCreate != null)
                foreach (Bind<TKey, TValue> binding in bindingsToCreate)
                    AddBinding(binding.key, binding.value);
        }

        public TValue this[TKey key]
        {
            get => GetValue(key);
            set => AddBinding(key, value);
        }

        public void AddBinding(TKey key, TValue bind)
        {
            Assert.IsFalse(ContainsKey(key), $"The key {key} already exists");
            Assert.IsFalse(ContainsValue(bind), $"The value {bind} already has a bind");
            bindings.Add(key, bind);
        }

        public TValue GetValue(TKey key)
        {
            Assert.IsTrue(ContainsKey(key), $"The key {key} does't exist in the binder");
            return bindings[key];
        }

        public bool ContainsKey(TKey key)
        {
            return bindings.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return bindings.ContainsValue(value);
        }

        public void Remove(TKey key)
        {
            Assert.IsTrue(ContainsKey(key), $"The key {key} doesn't exist");
            bindings.Remove(key);
        }
    }
}
