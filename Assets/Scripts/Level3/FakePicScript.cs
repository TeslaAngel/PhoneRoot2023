using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePicScript : MonoBehaviour
{
    public Transform FracTL;
    public Transform FracBR;
    private FractionScript FSTL;
    private FractionScript FSBR;


    private void Awake()
    {
        FSTL = FracTL.GetComponent<FractionScript>();
        FSBR = FracBR.GetComponent<FractionScript>();
    }

    private void Update()
    {
        if (FSTL.Moving)
        {
            if(Input.GetAxis("Mouse X") < 0)
            {
                float d = Vector3.Distance(FracTL.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)));
                FracTL.Rotate(0f, 0f, -Mathf.Asin(Input.GetAxis("Mouse X") / d)*100f);
                print("R");
            }
        }
        if (FSBR.Moving)
        {
            if (Input.GetAxis("Mouse Y") < 0)
            {
                float d = Vector3.Distance(FracTL.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)));
                FracBR.Rotate(0f, 0f, Mathf.Asin(Input.GetAxis("Mouse Y") / d)*100f);
                print("L");
            }
        }

        if (Mathf.Abs(FracTL.localRotation.z) + Mathf.Abs(FracBR.localRotation.z) > .35f)
        {
            Destroy(gameObject);
        }
    }

}
