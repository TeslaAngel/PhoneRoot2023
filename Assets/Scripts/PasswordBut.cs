using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGeneratorClass
{
    public static int ButOfKey = Random.Range(0, 9);
}

[RequireComponent(typeof(SpriteRenderer))]
public class PasswordBut : MonoBehaviour
{
    public int ButNum;
    public GameObject key;

    [Space]
    private Vector3 OriginScale;
    private Vector3 ScaleWhenDrag;
    public float DragScale;

    private Color initialColor;
    public Color MousingColor;
    private SpriteRenderer spriteRenderer;

    public float ShrinkTimeLerp;

    private Transform transform;
    private bool Mousing = false; //is true when being pressing by mouse
    private bool Moving = false; //is true when moving
    private Vector3 PosBeforePress;

    [Space]
    public PasswordContainer passwordContainer;
    public bool enter;


    private void Awake()
    {
        transform = GetComponent<Transform>();
        OriginScale = transform.localScale;
        ScaleWhenDrag = OriginScale * DragScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;

        PosBeforePress = transform.localPosition;
    }

    private void OnMouseDown()
    {
        Mousing = true;
        Moving = true;

        PosBeforePress = transform.localPosition;
    }

    private void OnMouseExit()
    {
        Mousing = false;
    }

    private void OnMouseUp()
    {
        if (Mousing)
        {
            Mousing = false;
            //then enter a num
            if (PosBeforePress == transform.position)
                passwordContainer.EnterNum(enter);
        }
    }

    private void Update()
    {
        if (Mousing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, ScaleWhenDrag, ShrinkTimeLerp * Time.deltaTime);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, MousingColor, ShrinkTimeLerp * 2f * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, OriginScale, ShrinkTimeLerp * Time.deltaTime);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, initialColor, ShrinkTimeLerp * 2f * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Moving = false;
        }
        if (Moving && Input.GetAxis("Mouse X")!=0 && Input.GetAxis("Mouse Y") != 0)
        {
            //Moving part
            Vector3 NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            NewPos.z = 0f;
            transform.position = NewPos;

            //Generate key
            if(ButNum == KeyGeneratorClass.ButOfKey)
            {
                KeyGeneratorClass.ButOfKey = -1;
                Instantiate(key, transform.position, transform.rotation);
            }
        }
    }
}
