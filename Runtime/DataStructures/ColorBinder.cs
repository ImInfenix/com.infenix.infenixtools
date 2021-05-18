using System.Collections.Generic;
using UnityEngine;

namespace InfenixTools.DataStructures
{
    public class ColorBinder<TKey> : Binder<TKey, Color>
    {
        public ColorBinder()
            : base()
        { }

        public ColorBinder(List<Bind<TKey, Color>> bindingsToCreate)
            : base(bindingsToCreate)
        { }

        public void BindToRandomColor(TKey key)
        {
            bool foundAColor = false;
            while (!foundAColor)
            {
                Color newBind = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
                if (!ContainsValue(newBind))
                {
                    AddBinding(key, newBind);
                    foundAColor = true;
                }
            }
        }
    }
}
