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

        public void ParseNumberToColor(int n)
        {
            rend = GetComponent<Renderer>();
            if (n == 1) rend.material.color = Color.blue;
            if (n == 2) rend.material.color = Color.red;
            if (n == 3) rend.material.color = Color.green;
            if (n == 4) rend.material.color = Color.yellow;
            if (n == 5) rend.material.color = Color.magenta;
            if (n == 6) rend.material.color = Color.white;
        }
    }
}
