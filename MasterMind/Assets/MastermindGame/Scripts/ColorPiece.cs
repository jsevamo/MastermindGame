using System;
using UnityEngine;

namespace MastermindGame.Scripts
{
    public class ColorPiece : MonoBehaviour
    {
        [SerializeField] private Renderer rend;
        private GameController GC;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }

        public Color GetMaterialColor()
        {
            return rend.material.color;
        }

        private void OnMouseDown()
        {
            GC.SetHasAColorBeenSelected();
            GC.colorOfSelectedPiece = rend.material.color;
            GC.hasSomethingBeenClicked = false;
        }
    }
}
