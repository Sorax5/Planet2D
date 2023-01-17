using UnityEngine;

namespace script
{
    public abstract class Attractable : MonoBehaviour
    {
        [SerializeField]
        private Vector3 direction;
        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

    }
}