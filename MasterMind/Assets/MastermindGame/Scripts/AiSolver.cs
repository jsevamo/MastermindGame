using System.Collections.Generic;
using UnityEngine;

namespace MastermindGame.Scripts
{
    
    
    public class AiSolver : MonoBehaviour
    {
        
        private GameController GC;
        [SerializeField ]List<List<int>> S = new List<List<int>>();
    
        void Start()
        {
            GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            
            CreateTheSetS();
            PrintHowManyGuessesLeft();
            SuggestACombination();
        }
    

        void CreateTheSetS()
        {
            for (var a = 1; a < 7; a++)
            for (var b = 1; b < 7; b++)
            for (var c = 1; c < 7; c++)
            for (var d = 1; d < 7; d++)
            {
                var list = new List<int>();
                list.Clear();
                list.Add(a);
                list.Add(b);
                list.Add(c);
                list.Add(d);
                S.Add(list);
            }
        }

        void PrintListFromS(List<int> list)
        {
            Debug.Log("[" + list[0] + "]" + "[" + list[1] + "]" + "[" + list[2] + "]" + "[" + list[3] + "]");
        }

        void PrintHowManyGuessesLeft()
        {
            Debug.Log("There currently are " + ReturnGuessesLeft() + " possible combinations left to play.");
            Debug.Log("That means there is a certainty of " + (1 / ReturnGuessesLeft())*100 + "% that you will win in this turn with what I suggest." );
        }

        int ReturnGuessesLeft()
        {
            return S.Count;
        }

        void PrintSuggestedMove(int a, int b, int c, int d)
        {
            Debug.Log("Suggested Move: " + "[" + a + "]"  + "[" + b + "]" + "[" + c + "]" + "[" + d + "]" );
        }

        void SuggestACombination()
        {
            if (GC.columnBeingPlayedOn == 0)
            {
                PrintSuggestedMove(1,1,2,2);
            }
        }
    }
}
