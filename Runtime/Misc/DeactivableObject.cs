using UnityEngine;

namespace InfenixTools.Utils
{
    public class DeactivableObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject content;

        public virtual void Enable()
        {
            if (content != null)
                content.SetActive(true);
        }

        public virtual void Disable()
        {
            if (content != null)
                content.SetActive(false);
        }
    }
}
