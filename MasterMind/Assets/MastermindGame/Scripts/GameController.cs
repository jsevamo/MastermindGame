using System.Collections.Generic;
using UnityEngine;

namespace MastermindGame.Scripts
{
    public class Column 
    {
        private List<GameObject> Pieces = new List<GameObject>();


        public Column(List<GameObject> piecesForColumn)
        {
            for (int i = 0; i < piecesForColumn.Count; i++)
            {
                Pieces.Add(piecesForColumn[i]);
            }
        }

        public int GetNumberOfPieces()
        {
            return Pieces.Count;
        }
    }
    
    public class GameController : MonoBehaviour
    {

        [SerializeField] [Range(2.0f, 4.0f)] private int numberToGuess = 4;
        [SerializeField] private GameObject boardPiece;
        [SerializeField] private List<Column> columns; 


        // Start is called before the first frame update
        void Start()
        {
            columns = new List<Column>();
            SpawnBoardPieces();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SpawnBoardPieces()
        {
            float x_start = -4.5f;
            float z_start = 1.8f;
            
            List<GameObject> pieces = new List<GameObject>();

            for (int x = 0; x < 8; x++)
            {

                
                for (int z = 0; z < numberToGuess; z++)
                {
                    
                    GameObject piece = Instantiate(boardPiece, new Vector3(x_start + (x * 1.1f), 0, z_start - (z * 1.5f)), Quaternion.identity);
                    pieces.Add(piece);
                }
                
                Column newCol = new Column(pieces);
                columns.Add(newCol);
                pieces.Clear();
            }

            Column c = columns[0];
            Debug.Log(c.GetNumberOfPieces());
        }
    }
}
