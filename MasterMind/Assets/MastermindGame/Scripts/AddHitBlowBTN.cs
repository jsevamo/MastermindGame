using System;
using UnityEngine;

namespace MastermindGame.Scripts
{
    public class AddHitBlowBTN : MonoBehaviour
    {
        private GameController GC;
    
        // Start is called before the first frame update
        void Start()
        {
            GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Pressed()
        {
            string blows = GC.blowsInput.text;
            string hits = GC.hitsInput.text;
            
            if (blows.Length <= 0 || hits.Length<=0)
            {
                throw new Exception("Neither blows nor hits can be empty if you want to use this function.");
            }
            
            Debug.Log(blows + " " + hits);

            GC.playingManually = true;
            int nblows = int.Parse(blows);
            int nhits = int.Parse(hits);

            GC.manualBlows = nblows;
            GC.manualHits = nhits;
            
            
            GC.MoveToNextColumn();

        }

    }
}
