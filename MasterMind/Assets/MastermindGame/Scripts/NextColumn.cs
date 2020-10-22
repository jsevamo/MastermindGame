using System;
using System.Collections.Generic;
using UnityEngine;

namespace MastermindGame.Scripts
{
    public class NextColumn : MonoBehaviour
    {
        // Start is called before the first frame update
    
        private GameController GC;
        private AiSolver AI;
        void Start()
        {
            GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            AI = GameObject.FindWithTag("AI").GetComponent<AiSolver>();
        }

        // Update is called once per frame
        private void OnMouseDown()
        {
            if (GC.columnBeingPlayedOn == 0)
            {
                int hits;
                int blows;

                hits = GC.Ghits;
                blows = GC.Gblows;
                List<int> currentGuess = GC.currentGuess;
                
                GC.SaveOrderOfPlayPieces();
                GC.MoveToNextColumn();
                AI.CompareWithEverythingInS(currentGuess, hits, blows);
                AI.ActivateAiforTurn();
            }
            else
            {
                GC.SaveOrderOfPlayPieces();
                GC.MoveToNextColumn();
                AI.ActivateAiforTurn();
            }
            
        }
    }
}
