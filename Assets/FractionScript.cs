using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractionScript : MonoBehaviour
{
    public bool Moving = false;

    private void OnMouseDown()
    {
        Moving = true;
    }



    private void OnMouseExit()
    {
        Moving = false;
    }

    private void OnMouseUp()
    {
        Moving = false;
    }
}
