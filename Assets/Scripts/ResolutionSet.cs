using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSet : MonoBehaviour
{
    public int rH;
    public int rV;

    private void Awake()
    {
        Screen.SetResolution(rH, rV, false);
    }
}
