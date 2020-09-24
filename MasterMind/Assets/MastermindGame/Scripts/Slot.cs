using UnityEngine;

namespace MastermindGame.Scripts
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] bool isFull;
    
        // Start is called before the first frame update
        void Start()
        {
            isFull = false;
        }
        

        public void SetIsFull(bool b)
        {
            isFull = b;
        }

        public bool IsFull()
        {
            return isFull;
        }
    }
}
