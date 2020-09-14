using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        public bool GetHasSomethingOn(int row)
        {
            BoardPiece bp = Pieces[row].GetComponent<BoardPiece>();
            return bp.GetHasSomethingOn();
        }

        public GameObject GetAtRow(int i)
        {
            return Pieces[i];
        }
    }

    public class GameController : MonoBehaviour
    {
        private GameObject boardHolder;
        
        [SerializeField] [Range(2.0f, 4.0f)] private int numberToGuess = 4;
        [SerializeField] private GameObject boardPiece;
        [SerializeField] private GameObject playPiece;
        [SerializeField] private List<Column> columns;
        [SerializeField] List<GameObject> colorPieces;
        [SerializeField] public bool hasSomethingBeenClicked;
        [SerializeField] private bool hasAColorBeenSelected;
        public Color colorOfSelectedPiece;
        [SerializeField] private GameObject actualPlayPiece;

        [SerializeField]
        int columnBeingPlayedOn;


        // Start is called before the first frame update
        private void Start()
        {
            boardHolder = new GameObject("Board Pieces are Here");
            
            columns = new List<Column>();
            SpawnBoardPieces();

            colorPieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("ColorPiece"));

            hasSomethingBeenClicked = false;
            hasAColorBeenSelected = false;

            columnBeingPlayedOn = 0;
        }

        // Update is called once per frame
        private void Update()
        {
            SpawnColorPieces();
            ChangeActualPieceColor();
            NextColumnChecker();

            
            
        }

        void NextColumnChecker()
        {
            int a = 0;
            for (int i = 0; i < numberToGuess; i++)
            {
                BoardPiece bp = columns[columnBeingPlayedOn].GetAtRow(i).GetComponent<BoardPiece>();

                if (bp.GetHasSomethingOn() == true)
                {
                    a++;
                }
            }

            if (a == numberToGuess)
            {
                Debug.Log("It's Filled");
                columnBeingPlayedOn++;
                a = 0;
            }
        }

        void ChangeActualPieceColor()
        {
            if (actualPlayPiece)
            {
                actualPlayPiece.GetComponent<Renderer>().material.color = colorOfSelectedPiece;
            }

        }

        public void SetHasAColorBeenSelected()
        {
            hasAColorBeenSelected = true;
        }

        void SpawnColorPieces()
        {
            if (hasSomethingBeenClicked)
            {
                for (int i = 0; i < colorPieces.Count; i++)
                {
                    GameObject cp = colorPieces[i];
                    cp.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < colorPieces.Count; i++)
                {
                    GameObject cp = colorPieces[i];
                    cp.SetActive(false);
                }
            }
        }

        private void SpawnBoardPieces()
        {
            BoardPiece bp;
            
            var x_start = -4.5f;
            var z_start = 1.2f;

            var pieces = new List<GameObject>();

            for (var x = 0; x < 8; x++)
            {
                for (var z = 0; z < numberToGuess; z++)
                {
                    var piece = Instantiate(boardPiece, new Vector3(x_start + x * 1.1f, 0, z_start - z * 1.5f),
                        Quaternion.identity);
                    piece.transform.parent = boardHolder.transform;
                    pieces.Add(piece);
                    
                    bp = piece.GetComponent<BoardPiece>();
                    bp.SetRowID(z);
                    bp.SetColID(x);
                }
                
                var newCol = new Column(pieces);
                columns.Add(newCol);
                
                pieces.Clear();
            }


        }

        public void AddPlayPieceToBoardPiece(int colID, int rowID)
        {
            hasSomethingBeenClicked = true;

            GameObject bp = columns[colID].GetAtRow(rowID);

            BoardPiece _bp = bp.GetComponent<BoardPiece>();

            if (_bp.GetHasSomethingOn() == false)
            {
                _bp.SetHasSomethingOn();
                

                var playPieceIns = Instantiate(playPiece, new Vector3(bp.transform.position.x,
                    bp.transform.position.y, bp.transform.position.z), Quaternion.identity);

                _bp.pieceOnTop = playPieceIns;
                actualPlayPiece = playPieceIns;
            }
            else
            {
                actualPlayPiece = _bp.pieceOnTop;
            }

            
            colorOfSelectedPiece = Color.black;
            


        }
    }
}