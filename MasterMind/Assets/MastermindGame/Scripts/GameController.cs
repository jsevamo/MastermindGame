using System.Collections.Generic;
using UnityEngine;

namespace MastermindGame.Scripts
{
    [System.Serializable]
    public class Column
    {
        [SerializeField] private List<GameObject> Pieces = new List<GameObject>();


        public Column(List<GameObject> piecesForColumn)
        {
            for (var i = 0; i < piecesForColumn.Count; i++) Pieces.Add(piecesForColumn[i]);
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
        private void Start()
        {
            columns = new List<Column>();
            SpawnBoardPieces();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void SpawnBoardPieces()
        {
            var x_start = -4.5f;
            var z_start = 1.8f;

            var pieces = new List<GameObject>();

            for (var x = 0; x < 8; x++)
            {
                for (var z = 0; z < numberToGuess; z++)
                {
                    var piece = Instantiate(boardPiece, new Vector3(x_start + x * 1.1f, 0, z_start - z * 1.5f),
                        Quaternion.identity);
                    pieces.Add(piece);
                }

                var newCol = new Column(pieces);
                columns.Add(newCol);
                pieces.Clear();
            }

            var c = columns[0];
            Debug.Log(c.GetNumberOfPieces());
        }
    }
}