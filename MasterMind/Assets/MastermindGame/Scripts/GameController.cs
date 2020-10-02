//TODO: MAYBE COL1 --- COL8 COULD BE DELETED? CURRENTGUESSLIST SEEMS LIKE A GOOD REPLACEMENT.

using System;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using Boo.Lang;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace MastermindGame.Scripts
{
    [Serializable]
    public class Column
    {
        [SerializeField] private System.Collections.Generic.List<GameObject> Pieces = new System.Collections.Generic.List<GameObject>();
        

        public Column(System.Collections.Generic.List<GameObject> piecesForColumn)
        {
            for (var i = 0; i < piecesForColumn.Count; i++) Pieces.Add(piecesForColumn[i]);
        }

        public int GetNumberOfPieces()
        {
            return Pieces.Count;
        }

        public bool GetHasSomethingOn(int row)
        {
            var bp = Pieces[row].GetComponent<BoardPiece>();
            return bp.GetHasSomethingOn();
        }

        public GameObject GetAtRow(int i)
        {
            return Pieces[i];
        }

        public System.Collections.Generic.List<GameObject> GetListOfBoardPieces()
        {
            return Pieces;
        }
    }

    public class GameController : MonoBehaviour
    {
        private GameObject boardHolder;
        private GameObject HnBHolder;
        private bool canProgress;
        [SerializeField] private bool hasDuplicateColors;

        [FormerlySerializedAs("numberToGuess")] [SerializeField] [Range(2.0f, 4.0f)]
        private int numberOfRowsToGuess = 4;

        [SerializeField] private GameObject boardPiece;
        [SerializeField] private GameObject playPiece;
        [SerializeField] private GameObject winningPiece;
        [SerializeField] private GameObject hitBlowBoardPiece;
        [SerializeField] private GameObject hitBlowPiece;
        [SerializeField] private List<Column> columns;
        [SerializeField] private List<GameObject> hitAndBlowPiecesList;
        [SerializeField] private System.Collections.Generic.List<GameObject> colorPieces;
        [SerializeField] public bool hasSomethingBeenClicked;
        [SerializeField] private bool hasAColorBeenSelected;
        public Color colorOfSelectedPiece;
        [SerializeField] private GameObject actualPlayPiece;
        [SerializeField] private GameObject checkButton;

        [FormerlySerializedAs("playPieces")] [SerializeField]
        private System.Collections.Generic.List<GameObject> playPiecesPutOnBoard;

        [SerializeField]private System.Collections.Generic.List<int> currentGuess;
        [SerializeField] private System.Collections.Generic.List<int> Col1;
        [SerializeField] private System.Collections.Generic.List<int> Col2;
        [SerializeField] private System.Collections.Generic.List<int> Col3;
        [SerializeField] private System.Collections.Generic.List<int> Col4;
        [SerializeField] private System.Collections.Generic.List<int> Col5;
        [SerializeField] private System.Collections.Generic.List<int> Col6;
        [SerializeField] private System.Collections.Generic.List<int> Col7;
        [SerializeField] private System.Collections.Generic.List<int> Col8;
        [SerializeField] private System.Collections.Generic.List<int>[] columnArray = new System.Collections.Generic.List<int>[8];

        [SerializeField] private System.Collections.Generic.List<int> winList;

        [SerializeField] private int columnBeingPlayedOn;

        [SerializeField] private bool gameOver;
        

        public int GetNumberOfRowsToGuess()
        {
            return numberOfRowsToGuess;
        }


        // Start is called before the first frame update
        private void Start()
        {
            gameOver = false;

            boardHolder = new GameObject("Board Pieces are Here");
            HnBHolder = new GameObject("Hit&Blow Pieces are Here");

            columns = new List<Column>();
            SpawnBoardPieces();

            colorPieces =
                new System.Collections.Generic.List<GameObject>(GameObject.FindGameObjectsWithTag("ColorPiece"));
            playPiecesPutOnBoard = new System.Collections.Generic.List<GameObject>();
            hitAndBlowPiecesList = new List<GameObject>();
            currentGuess = new System.Collections.Generic.List<int>();
            winList = new System.Collections.Generic.List<int>();

            hasSomethingBeenClicked = false;
            hasAColorBeenSelected = false;

            columnBeingPlayedOn = 0;
            checkButton.SetActive(false);

            AddColumnsToColumnArray();
            CreateWinArrangement();
            SpawnHitAndBlowPieces();
            
        }

        // Update is called once per frame
        private void Update()
        {
            if (!gameOver)
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
                    gameOver = true;
                }
            }
            else
            {
                Debug.Log("Game Over");
            }
        }

        private void CreateWinArrangement()
        {
            if (hasDuplicateColors)
                for (var i = 0; i < numberOfRowsToGuess; i++)
                    winList.Add(Random.Range(1, 6));
            else
                for (var i = 0; i < numberOfRowsToGuess; i++)
                {
                    var numberToAdd = Random.Range(1, 6);

                    while (winList.Contains(numberToAdd)) numberToAdd = Random.Range(1, 6);

                    winList.Add(numberToAdd);
                }
            
            
            //Override Win List for tests
            winList.Clear();
            winList.Add(6);
            winList.Add(6);
            winList.Add(4);
            winList.Add(2);
            
            DrawWinningArrangement();
        }

        private void DrawWinningArrangement()
        {
            var solutionList = new System.Collections.Generic.List<GameObject>();
            var x = 6.3f;
            float y = 0;
            var z = 1.2f;
            for (var i = 0; i < numberOfRowsToGuess; i++)
            {
                var solutionItem = Instantiate(winningPiece, new Vector3(x, y, z), Quaternion.identity);
                z = z - 1.5f;
                solutionList.Add(solutionItem);
            }

            for (var i = 0; i < winList.Count; i++)
            {
                var piece = solutionList[i].GetComponent<ColorPiece>();
                var number = winList[i];
                piece.ParseNumberToColor(number);
            }
        }

        private void AddColumnsToColumnArray()
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

        private void DisplayNewColumns()
        {
            for (var i = 0; i < columns[columnBeingPlayedOn].GetListOfBoardPieces().Count; i++)
            {
                var boardPiece = columns[columnBeingPlayedOn].GetAtRow(i);
                boardPiece.SetActive(true);
            }
        }

        private void CheckIfColumnFull()
        {
            var activeRow = 0;
            for (var i = 0; i < numberOfRowsToGuess; i++)
            {
                var bp = columns[columnBeingPlayedOn].GetAtRow(i).GetComponent<BoardPiece>();

                if (bp.GetHasSomethingOn()) activeRow++;
            }

            if (activeRow == numberOfRowsToGuess)
            {
                checkButton.SetActive(true);
            }
        }



        int CountHowManyOfNumber(System.Collections.Generic.List<int> list, int n)
        {
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == n)
                {
                    count++;
                }
            }
            return count;
        }
        
        void CompareToSolution()
        {
            var obj = hitAndBlowPiecesList[columnBeingPlayedOn - 1];
            var currentPlay = obj.GetComponent<HitnBlow>();
            var hits = 0;
            var blows = 0;

            var usedSlots = new System.Collections.Generic.List<int>();

            for (var i = 0; i < currentGuess.Count; i++)
                if (currentGuess[i] == winList[i])
                {
                    hits++;
                    usedSlots.Add(i);
                }

            var guessListAfterHits = new System.Collections.Generic.List<int>();
            var winListAfterHits = new System.Collections.Generic.List<int>();

            for (var i = 0; i < winList.Count; i++)
                if (!usedSlots.Contains(i))
                    winListAfterHits.Add(winList[i]);


            for (var i = 0; i < currentGuess.Count; i++)
                if (!usedSlots.Contains(i))
                    guessListAfterHits.Add(currentGuess[i]);


            var flaggedN = new System.Collections.Generic.List<int>();

            foreach (var n in guessListAfterHits)
                if (winListAfterHits.Contains(n))
                {
                    var countGuess = CountHowManyOfNumber(guessListAfterHits, n);
                    var countWin = CountHowManyOfNumber(winListAfterHits, n);


                    if (countGuess == countWin)
                    {
                        if (!flaggedN.Contains(n))
                            for (var i = 0; i < countGuess; i++)
                            {
                                blows++;
                                flaggedN.Add(n);
                            }
                    }
                    else
                    {
                        if (!flaggedN.Contains(n))
                        {
                            blows++;
                            flaggedN.Add(n);
                        }
                    }
                }


            flaggedN.Clear();


            currentPlay.AddHitsAndBlows(hits, blows);
            if (hits == numberOfRowsToGuess) gameOver = true;
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

        private void CheckIfCanMove(int colN)
        {
            canProgress = false;
            for (var i = 0; i < columnArray[colN].Count; i++)
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

            if (canProgress)
            {
                //Debug.Log("To the Next one");
                columnBeingPlayedOn++;
                checkButton.SetActive(false);
                CompareToSolution();
                canProgress = false;
            }
        }


        private void SaveOnColumn(System.Collections.Generic.List<GameObject> boardPieceList, int colN)
        {
            foreach (var bp in boardPieceList)
            {
                var _bp = bp.GetComponent<BoardPiece>();
                var pieceOnTop = _bp.pieceOnTop;
                var _pieceOnTop = pieceOnTop.GetComponent<PlayPiece>();
                if (colN == 0)
                {
                    Col1.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col1;
                }

                if (colN == 1)
                {
                    Col2.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col2;
                }

                if (colN == 2)
                {
                    Col3.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col3;
                }

                if (colN == 3)
                {
                    Col4.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col4;
                }

                if (colN == 4)
                {
                    Col5.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col5;
                }

                if (colN == 5)
                {
                    Col6.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col6;
                }

                if (colN == 6)
                {
                    Col7.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col7;
                }

                if (colN == 7)
                {
                    Col8.Add(_pieceOnTop.ParseColorToNumber());
                    currentGuess = Col8;
                }
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

        private void ChangeActualPieceColor()
        {
            if (actualPlayPiece) actualPlayPiece.GetComponent<Renderer>().material.color = colorOfSelectedPiece;
        }

        public void SetHasAColorBeenSelected()
        {
            hasAColorBeenSelected = true;
        }

        void SpawnHitAndBlowPieces()
        {
            float xstart = -4.5f;
            
            for (int x = 0; x < 8; x++)
            {
                GameObject piece = Instantiate(hitBlowBoardPiece, new Vector3(xstart, 0.07f, 2), Quaternion.identity);
                xstart = xstart + 1.1f;
                hitAndBlowPiecesList.Add(piece);
                piece.transform.parent = HnBHolder.transform;
            }
        }

        private void SpawnColorPieces()
        {
            if (hasSomethingBeenClicked)
                for (var i = 0; i < colorPieces.Count; i++)
                {
                    var cp = colorPieces[i];
                    cp.SetActive(true);
                }
            else
                for (var i = 0; i < colorPieces.Count; i++)
                {
                    var cp = colorPieces[i];
                    cp.SetActive(false);
                }
        }

        private void SpawnBoardPieces()
        {
            BoardPiece bp;

            var x_start = -4.5f;
            var z_start = 1.2f;

            var pieces = new System.Collections.Generic.List<GameObject>();

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

            var bp = columns[colID].GetAtRow(rowID);

            var _bp = bp.GetComponent<BoardPiece>();

            if (_bp.GetHasSomethingOn() == false)
            {
                _bp.SetHasSomethingOn();


                var playPieceIns = Instantiate(playPiece, new Vector3(bp.transform.position.x,
                    bp.transform.position.y, bp.transform.position.z), Quaternion.identity);

                playPiecesPutOnBoard.Add(playPieceIns);

                _bp.pieceOnTop = playPieceIns;
                actualPlayPiece = playPieceIns;
            }
            else
            {
                actualPlayPiece = _bp.pieceOnTop;
            }

            hasAColorBeenSelected = false;
            colorOfSelectedPiece = new Color(0, 0, 0);
        }
    }
}