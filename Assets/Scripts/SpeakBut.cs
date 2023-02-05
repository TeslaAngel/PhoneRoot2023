using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakBut : MonoBehaviour
{
    public float HoldTime;
    private float HeldTime;
    private bool Mousing;
    public GameObject NormalBubble;
    public GameObject CapturedVirus;

    private void OnMouseDown()
    {
        Mousing = true;
    }

    private void OnMouseExit()
    {
        Mousing = false;
    }

    private void OnMouseUp()
    {
        Mousing = false;
    }

    private void Awake()
    {
        HeldTime = HoldTime;
    }

    private void Update()
    {
        if (Mousing)
        {
            HeldTime -= Time.deltaTime;
            if(HeldTime <= 0)
            {
                //Show the virus
                Vector3 v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
                v3 = new Vector3(v3.x, v3.y, 0f);
                Instantiate(CapturedVirus, v3, transform.rotation);
                NormalBubble.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                NormalBubble.GetComponent<SpriteRenderer>().color = Color.white;
                Instantiate(NormalBubble, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            HeldTime = HoldTime;
        }
    }
}
