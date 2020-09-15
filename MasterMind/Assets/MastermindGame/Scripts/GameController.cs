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
            var canProgress = false;
            if (columnBeingPlayedOn == 0)
                for (var i = 0; i < Col1.Count; i++)
                    if (Col1[i] == 0)
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

                        Col1.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            
            
            if (columnBeingPlayedOn == 1)
                for (var i = 0; i < Col2.Count; i++)
                    if (Col2[i] == 0)
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

                        Col2.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 2)
                for (var i = 0; i < Col3.Count; i++)
                    if (Col3[i] == 0)
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

                        Col3.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 3)
                for (var i = 0; i < Col4.Count; i++)
                    if (Col4[i] == 0)
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

                        Col4.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 4)
                for (var i = 0; i < Col5.Count; i++)
                    if (Col5[i] == 0)
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

                        Col5.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 5)
                for (var i = 0; i < Col6.Count; i++)
                    if (Col6[i] == 0)
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

                        Col6.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 6)
                for (var i = 0; i < Col7.Count; i++)
                    if (Col7[i] == 0)
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

                        Col7.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (columnBeingPlayedOn == 7)
                for (var i = 0; i < Col8.Count; i++)
                    if (Col8[i] == 0)
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

                        Col8.Clear();
                        return;
                    }
                    else
                    {
                        canProgress = true;
                    }
            
            if (canProgress)
            {
                
                Debug.Log("To the Next one");

                columnBeingPlayedOn++;
                
                
                checkButton.SetActive(false);
                canProgress = false;
                
                
            }

            
        }
        
        public void SaveOrderOfPlayPieces()
        {
            List<GameObject> boardPieceList = columns[columnBeingPlayedOn].GetListOfBoardPieces();
            if (columnBeingPlayedOn == 0)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col1.Add(_pieceOnTop.ParseColorToNumber());
                    //Debug.Log(_pieceOnTop.GetColor());
                }

            if (columnBeingPlayedOn == 1)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col2.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 2)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col3.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 3)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col4.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 4)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col5.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 5)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col6.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 6)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col7.Add(_pieceOnTop.ParseColorToNumber());
                }
            
            if (columnBeingPlayedOn == 7)
                foreach (var bp in boardPieceList)
                {
                    var _bp = bp.GetComponent<BoardPiece>();
                    var pieceOnTop = _bp.pieceOnTop;
                    var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                    Col8.Add(_pieceOnTop.ParseColorToNumber());
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

        
    }
}