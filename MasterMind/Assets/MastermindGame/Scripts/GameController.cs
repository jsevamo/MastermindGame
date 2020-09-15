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

        public List<GameObject> GetListOfBoardPieces()
        {
            return Pieces;
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
        [SerializeField] private GameObject checkButton;
        [SerializeField] private List<GameObject> playPieces;
        
        [SerializeField] private List<int> Col1;
        [SerializeField] private List<int> Col2;
        [SerializeField] private List<int> Col3;
        [SerializeField] private List<int> Col4;
        [SerializeField] private List<int> Col5;
        [SerializeField] private List<int> Col6;
        [SerializeField] private List<int> Col7;
        [SerializeField] private List<int> Col8;

        [SerializeField]
        int columnBeingPlayedOn;


        // Start is called before the first frame update
        private void Start()
        {
            boardHolder = new GameObject("Board Pieces are Here");
            
            columns = new List<Column>();
            SpawnBoardPieces();

            colorPieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("ColorPiece"));
            playPieces = new List<GameObject>();

            hasSomethingBeenClicked = false;
            hasAColorBeenSelected = false;

            columnBeingPlayedOn = 0;
            checkButton.SetActive(false);

        }

        // Update is called once per frame
        private void Update()
        {
            SpawnColorPieces();
            ChangeActualPieceColor();
            CheckIfColumnFull();
            DisplayNewColumns();

            

        }

        void DisplayNewColumns()
        {
            for (int i = 0; i < columns[columnBeingPlayedOn].GetListOfBoardPieces().Count; i++)
            {
                GameObject boardPiece = columns[columnBeingPlayedOn].GetAtRow(i);
                boardPiece.SetActive(true);
            }
        }

        void CheckIfColumnFull()
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
                //columnBeingPlayedOn++;
                checkButton.SetActive(true);
                //a = 0;
            }
        }

        public void MoveToNextColumn()
        {

            bool canProgress = false;
            
            if (columnBeingPlayedOn == 0)
            {

                for (int i = 0; i < Col1.Count; i++)
                {
                    if (Col1[i] == 0)
                    {
                        
                        checkButton.SetActive(false);
                        
                        Debug.Log("You need to add colors to pieces");
                        

                        List<GameObject> bps = columns[columnBeingPlayedOn].GetListOfBoardPieces();

                        for (int x = 0; x < bps.Count; x++)
                        {
                            BoardPiece bp = bps[x].GetComponent<BoardPiece>();
                            bp.DeletePieceOnTop();
                            bp.SetHasSomethingOnFalse();
                        }
                        
                        
                        Col1.Clear();
                        
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
                }
            }

            if (canProgress)
            {
                Debug.Log("Go on Goo sir");
                columnBeingPlayedOn++;
                checkButton.SetActive(false);
                canProgress = false;
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
                    piece.SetActive(false);
                    
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
                
                playPieces.Add(playPieceIns);

                _bp.pieceOnTop = playPieceIns;
                actualPlayPiece = playPieceIns;
            }
            else
            {
                actualPlayPiece = _bp.pieceOnTop;
            }

            hasAColorBeenSelected = false;
            colorOfSelectedPiece = new Color(0,0,0);
            


        }

        public void SaveOrderOfPlayPieces()
        {
            List<GameObject> boardPieceList = columns[columnBeingPlayedOn].GetListOfBoardPieces();

            if (columnBeingPlayedOn == 0)
            {
                foreach (var bp in boardPieceList)
                {
                    BoardPiece _bp = bp.GetComponent<BoardPiece>();
                    GameObject pieceOnTop = _bp.pieceOnTop;
                    PlayPiece _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                
                    Col1.Add(_pieceOnTop.ParseColorToNumber());
                    //Debug.Log(_pieceOnTop.GetColor());
                
                }
            }

            
        }
    }
}