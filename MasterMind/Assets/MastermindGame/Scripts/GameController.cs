using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [FormerlySerializedAs("numberToGuess")] [SerializeField] [Range(2.0f, 4.0f)] private int numberOfRowsToGuess = 4;
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
        
        [SerializeField] private List<int> Col1 = null;
        [SerializeField] private List<int> Col2 = null;
        [SerializeField] private List<int> Col3 = null;
        [SerializeField] private List<int> Col4 = null;
        [SerializeField] private List<int> Col5 = null;
        [SerializeField] private List<int> Col6 = null;
        [SerializeField] private List<int> Col7 = null;
        [SerializeField] private List<int> Col8 = null;
        [SerializeField] List<int>[] columnArray = new List<int>[8];

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
            
            AddColumnsToColumnArray();

        }

        // Update is called once per frame
        private void Update()
        {
            if (columnBeingPlayedOn < 8)
            {
                SpawnColorPieces();
                ChangeActualPieceColor();
                CheckIfColumnFull();
                DisplayNewColumns();
            }
            else
            {
                Debug.Log("you lost");
            }

        }

        void AddColumnsToColumnArray()
        {
            columnArray[0] = Col1;
            columnArray[1] = Col2;
            columnArray[2] = Col3;
            columnArray[3] = Col4;
            columnArray[4] = Col5;
            columnArray[5] = Col6;
            columnArray[6] = Col7;
            columnArray[7] = Col8;
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
            int activeRow = 0;
            for (int i = 0; i < numberOfRowsToGuess; i++)
            {
                BoardPiece bp = columns[columnBeingPlayedOn].GetAtRow(i).GetComponent<BoardPiece>();

                if (bp.GetHasSomethingOn())
                {
                    activeRow++;
                }
            }

            if (activeRow == numberOfRowsToGuess)
            {
                checkButton.SetActive(true);
            }
        }
        

        public void MoveToNextColumn()
        {
            if (columnBeingPlayedOn == 0) CheckIfCanMove(0); 
            if (columnBeingPlayedOn == 1) CheckIfCanMove(1);
            if (columnBeingPlayedOn == 2) CheckIfCanMove(2);
            if (columnBeingPlayedOn == 3) CheckIfCanMove(3);
            if (columnBeingPlayedOn == 4) CheckIfCanMove(4);
            if (columnBeingPlayedOn == 5) CheckIfCanMove(5);
            if (columnBeingPlayedOn == 6) CheckIfCanMove(6);
            if (columnBeingPlayedOn == 7) CheckIfCanMove(7);
        }

        void CheckIfCanMove(int colN)
        {
            var canProgress = false;
            for (int i = 0; i < columnArray[colN].Count; i++)
            {
                if (columnArray[colN][i] == 0)
                {
                    checkButton.SetActive(false);
                    Debug.Log("You need to add colors to pieces");
                    var bps = columns[columnBeingPlayedOn].GetListOfBoardPieces();
                    for (var x = 0; x < bps.Count; x++)
                    {
                        var bp = bps[x].GetComponent<BoardPiece>();
                        bp.DeletePieceOnTop();
                        bp.SetHasSomethingOnFalse();
                    }
                    
                    columnArray[colN].Clear();
                    return;
                }
                else
                {
                    canProgress = true;
                }
            }
            if (canProgress)
            {
                
                Debug.Log("To the Next one");

                columnBeingPlayedOn++;
                
                
                checkButton.SetActive(false);
                canProgress = false;
                
                
            }
        }
        

        void SaveOnColumn(List<GameObject> boardPieceList, int colN)
        {
            foreach (var bp in boardPieceList)
            {
                var _bp = bp.GetComponent<BoardPiece>();
                var pieceOnTop = _bp.pieceOnTop;
                var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                if (colN == 0) Col1.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 1) Col2.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 2) Col3.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 3) Col4.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 4) Col5.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 5) Col6.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 6) Col7.Add(_pieceOnTop.ParseColorToNumber());
                if (colN == 7) Col8.Add(_pieceOnTop.ParseColorToNumber());

            }
        } 
        
        public void SaveOrderOfPlayPieces()
        {
            var boardPieceList = columns[columnBeingPlayedOn].GetListOfBoardPieces();
            if (columnBeingPlayedOn == 0) SaveOnColumn(boardPieceList, 0);
            if (columnBeingPlayedOn == 1) SaveOnColumn(boardPieceList, 1);
            if (columnBeingPlayedOn == 2) SaveOnColumn(boardPieceList, 2);
            if (columnBeingPlayedOn == 3) SaveOnColumn(boardPieceList, 3);
            if (columnBeingPlayedOn == 4) SaveOnColumn(boardPieceList, 4);
            if (columnBeingPlayedOn == 5) SaveOnColumn(boardPieceList, 5);
            if (columnBeingPlayedOn == 6) SaveOnColumn(boardPieceList, 6);
            if (columnBeingPlayedOn == 7) SaveOnColumn(boardPieceList, 7);
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
                for (var z = 0; z < numberOfRowsToGuess; z++)
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

        
    }
}