using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VirusScript))]
public class VirusDirectionChanger : MonoBehaviour
{
    public Sprite[] Front;
    public Sprite[] Left;
    private VirusScript virusScript;

    public VirusScript[] VSs;

    private void Awake()
    {
        virusScript = GetComponent<VirusScript>();
    }

    public void ToLeft()
    {
        virusScript.Sprites = Left;
    }

    public void ToFront()
    {
        virusScript.Sprites = Front;
    }

    public void SelfDestory()
    {
        for(int i = 0; i < VSs.Length; i++)
        {
            VSs[i].enabled = true;
        }
        Destroy(gameObject);
    }
}
