using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class FakePicScript : MonoBehaviour
{
    public Transform FracTL;
    public Transform FracBR;
    private FractionScript FSTL;
    private FractionScript FSBR;
    public PolygonCollider2D PCofReal;
    public Animation VirusAnimation;

    private bool Exiting = false;

    private void Awake()
    {
        FSTL = FracTL.GetComponent<FractionScript>();
        FSBR = FracBR.GetComponent<FractionScript>();
        PCofReal.enabled = false;
    }

    private void Update()
    {
        if (FSTL.Moving)
        {
            if(Input.GetAxis("Mouse X") < 0)
            {
                float d = Vector3.Distance(FracTL.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)));
                FracTL.Rotate(0f, 0f, -Mathf.Asin(Input.GetAxis("Mouse X") / d)*20f);
                print("R");
            }
        }
        if (FSBR.Moving)
        {
            if (Input.GetAxis("Mouse Y") < 0)
            {
                float d = Vector3.Distance(FracTL.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)));
                FracBR.Rotate(0f, 0f, Mathf.Asin(Input.GetAxis("Mouse Y") / d)*20f);
                print("L");
            }
        }

        if (Mathf.Abs(FracTL.localRotation.z) + Mathf.Abs(FracBR.localRotation.z) > .65f)
        {
            Exiting = true;
        }

        if (Exiting)
        {
            transform.Translate(-10 * Time.deltaTime, 0, 0);
            PCofReal.enabled = true;
            if(VirusAnimation)
                VirusAnimation.Play();
        }
    }

}
