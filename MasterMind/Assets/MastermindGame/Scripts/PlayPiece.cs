using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPiece : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] public bool hasColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        hasColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfColorHasBeenAssigned();
    }

    public void CheckIfColorHasBeenAssigned()
    {
        if (rend.material.color == Color.black)
        {
            hasColor = false;
        }
        else
        {
            hasColor = true;
        }
    }

    public Color GetColor()
    {
        Color c = rend.material.color;
        return c;
    }

    public int ParseColorToNumber()
    {
        Color c = rend.material.color;
        
        if (c == Color.blue)
        {
            return 1;
        }
        else if (c == Color.red)
        {
            return 2;
        }
        else if (c == Color.green)
        {
            return 3;
        }
        else if (c == new Color(1f, 1f, 0f))
        {
            return 4;
        }
        else if (c == Color.magenta)
        {
            return 5;
        }
        else if (c == Color.white)
        {
            return 6;
        }
        

        else return 0;
    }
}
