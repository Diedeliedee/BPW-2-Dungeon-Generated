using UnityEngine;

namespace Joeri.Tools.Utilities
{
    public class DeleteOnAnimationEvent : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
