using System;
using UnityEngine;

namespace MastermindGame.Scripts
{
    public class BoardPiece : MonoBehaviour
    {
        private GameController GC;
        [SerializeField] private bool hasSomethingOn; 
        [SerializeField] private bool hasBeenClicked;
        [SerializeField] private int colID;
        [SerializeField] private int rowID;
        [SerializeField] public GameObject pieceOnTop;



        
        //TODO: ADD THE PlayPiece Class here.
 
        private void Start()
        {
            GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            hasSomethingOn = false;
            hasBeenClicked = false;
        }

        private void Update()
        {
            
        }

        private void OnMouseDown()
        {
            GC.AddPlayPieceToBoardPiece(colID, rowID);
        }

        public bool GetHasSomethingOn()
        {
            return hasSomethingOn;
        }
        
        public void SetHasSomethingOn()
        {
             hasSomethingOn = true;
        }
        
        public void SetHasSomethingOnFalse()
        {
            hasSomethingOn = false;
        }

        public bool GetHasBeenClicked()
        {
            return hasBeenClicked;
        }

        public int GetRowID()
        {
            return rowID;
        }

        public void SetRowID(int id)
        {
            rowID = id;
        }
        
        public int GetColID()
        {
            return colID;
        }

        public void SetColID(int id)
        {
            colID = id;
        }

        public void SetHasBeenClicked(bool b)
        {
            hasBeenClicked = b;
        }

        public void DeletePieceOnTop()
        {
            Destroy(pieceOnTop);
        }
    }
}
