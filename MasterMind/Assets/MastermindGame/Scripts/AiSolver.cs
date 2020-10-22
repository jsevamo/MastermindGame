using System;
using System.Collections.Generic;
using System.Linq;
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
            ActivateAiforTurn();
            
            
            // Testing for removing solution from S
            
            // List<int> a = new List<int>();
            // a.Add(6);
            // a.Add(5);
            // a.Add(4);
            // a.Add(3);
            //
            // RemoveSolutionFromListS(a);
            
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
            Debug.Log("------- Starting suggestion for Turn  " + (GC.columnBeingPlayedOn+1) + " -------");
            
            Debug.Log("After having performed some magic, I currently see " + ReturnGuessesLeft() + 
                      " possible combinations that could win the game in this turn.");
            
            Debug.Log("That means that I have a certainty of " + Math.Round(((1f / ReturnGuessesLeft()) * 100f), 2) 
                                                            + "% that you will win now with the following " +
                                                            "suggested move: " );
        }

        int ReturnGuessesLeft()
        {
            return S.Count;
        }

        void PrintSuggestedMove(int a, int b, int c, int d)
        {
            Debug.Log("Suggested Move: " + "[" + ParseNumberToColor(a) + "]"  + "[" + ParseNumberToColor(b) + "]" 
                      + "[" + ParseNumberToColor(c) + "]" + "[" + ParseNumberToColor(d) + "]" );
        }

        string ParseNumberToColor(int n)
        {
            if (n == 1)
                return "Blue";
            if (n == 2)
                return "Red";
            else if (n == 3)
                return "Green";
            else if (n == 4)
                return "Yellow";
            else if (n == 5)
                return "Pink";
            else if (n == 6)
                return "White";
            else
                throw new Exception("The parsing from numbers to colors is wrong. " +
                                    "Somehow we inputted a number less than 1 or greater than 6");
        }

        void RemoveSolutionFromListS(List<int> toRemove)
        {
            int counter = 0;
            bool hasRemoved = false;
            
            for (int i = 0; i < S.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (toRemove[j] == S[i][j])
                    {
                        counter++;
                        if (counter == 4)
                        {
                            //Debug.Log("ok I found what to delete at S:" + i);
                            S.RemoveAt(i);
                            hasRemoved = true;
                            counter = 0;
                            return;
                        }
                        
                    }

                    if (j == 3) counter = 0;
                }
               
            }
            

            if (!hasRemoved)
            {
                throw new Exception("Nothing has been removed from S");
            }
        }

        void SuggestACombination()
        {
            if (GC.columnBeingPlayedOn == 0)
            {
                PrintSuggestedMove(1, 1, 2, 2);
                Debug.Log("------- End Of Suggestion for Turn " + (GC.columnBeingPlayedOn+1) + " -------");
            }
            else
            {
                int hits;
                int blows;

                hits = GC.Ghits;
                blows = GC.Gblows;

                List<int> currentGuess = GC.currentGuess;
                
                CompareWithEverythingInS(currentGuess, hits, blows);
                
                
                Debug.Log("------- End Of Suggestion for Turn " + (GC.columnBeingPlayedOn+1) + " -------");
            }
        }

        public void CompareWithEverythingInS(List<int> _currentGuess, int _hits, int _blows)
        {
            List<int> a = new List<int>();
            
            a.Add(1);
            a.Add(1);
            a.Add(1);
            a.Add(1);

            CompareHitsAndBlows(_currentGuess,a, _hits, _blows);

        }

        void  CompareHitsAndBlows(List<int> currentGuess, List<int> winList, int hits, int blows)
        {
            currentGuess = GC.currentGuess;
            
            
            int _hits = 0;
            int _blows = 0;
            
            var usedSlots = new System.Collections.Generic.List<int>();
            
            

            for (var i = 0; i < currentGuess.Count; i++)
                if (currentGuess[i] == winList[i])
                {
                    _hits++;
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
                                _blows++;
                                flaggedN.Add(n);
                            }
                    }
                    else
                    {
                        if (!flaggedN.Contains(n))
                        {
                            _blows++;
                            flaggedN.Add(n);
                        }
                    }
                }


            flaggedN.Clear();
            
            

            if (_hits == hits && _blows == blows)
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
            else
            {
                Debug.Log("We have removed some thingies");
                RemoveSolutionFromListS(winList);
            }
            
        }

        public void ActivateAiforTurn()
        {
            
            PrintHowManyGuessesLeft();
            SuggestACombination();
            
            
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
    }
}
