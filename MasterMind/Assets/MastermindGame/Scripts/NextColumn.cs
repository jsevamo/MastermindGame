using System;
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
            GC.SaveOrderOfPlayPieces();
            GC.MoveToNextColumn();
            AI.ActivateAiforTurn();
        }
    }
}
