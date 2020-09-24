using System;
using System.Collections;
using System.Collections.Generic;
using MastermindGame.Scripts;
using UnityEngine;

public class HitnBlow : MonoBehaviour
{
    [SerializeField] private List<GameObject> slots;
    [SerializeField] private GameObject piece;
    private GameController GC;
    [SerializeField]  private int numberOfHits;
    [SerializeField]  private int numberOfBlows;
    
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        slots = new List<GameObject>();
        GetListOfSlots();
        
        AddHitsAndBlows(1,1);
        
    }
    


    void PutPieces(int n, Color col)
    {
        for (var i = 0; i < n; i++)
        {
            var slot = slots[i].GetComponent<Slot>();
            var putItHere = 0;
            for (var j = 0; j < slots.Count; j++)
            {
                var s = slots[j].GetComponent<Slot>();
                if (s.IsFull()) putItHere++;
            }

            slot = slots[putItHere].GetComponent<Slot>();
            var ins_piece = Instantiate(piece, slot.transform.position, Quaternion.identity);
            ins_piece.GetComponent<Renderer>().material.color = col;
            slot.SetIsFull(true);
        }
    }

    void AddHitsAndBlows(int hits, int blows)
    {
        if (hits + blows > GC.GetNumberOfRowsToGuess())
            throw new Exception("GodDamnit you can't have more hits and blows than guesses.");
        if (hits > 0) PutPieces(hits, new Color(1, 0.5f, 0));
        if (blows > 0) PutPieces(blows, Color.white);
        numberOfHits = hits;
        numberOfBlows = blows;
    }

    void GetListOfSlots()
    {
        foreach (Transform child in transform)
            if (child.CompareTag("Slot"))
                slots.Add(child.gameObject);
    }
}
