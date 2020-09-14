using UnityEngine;

namespace MastermindGame.Scripts
{
    public class GameController : MonoBehaviour
    {

        [SerializeField] [Range(2.0f, 4.0f)] private int numberToGuess = 4;
        [SerializeField] private GameObject boardPiece;
    
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(boardPiece, new Vector3(0, 0, 0), Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
