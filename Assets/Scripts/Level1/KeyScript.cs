using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private bool Moving = false;

    private void OnMouseDown()
    {
        Moving = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Moving = false;
        }

        if (Moving && Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
        {
            //Moving part
            Vector3 NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            NewPos.z = 0f;
            transform.position = NewPos;
        }
    }

}
