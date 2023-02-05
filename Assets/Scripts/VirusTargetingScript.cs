using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class VirusTargetingScript : MonoBehaviour
{
    private Vector3 TargetedPos = new Vector3(2.36f, -4.1f, 0f);
    private bool OnCapture = true;
    public GameObject HB;

    private void Start()
    {
        HB = VirusShelter.HB;
        HB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnCapture = false;
        }

        if (OnCapture)
        {
            //Moving part
            Vector3 NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            NewPos.z = 0f;
            transform.position = NewPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, TargetedPos, .08f);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(.05f, .05f, 1f), .08f);
            if (HB)
            {
                HB.SetActive(true);
                HB = null;
            }
        }
    }
}
