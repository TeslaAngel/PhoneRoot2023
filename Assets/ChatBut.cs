using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBut : MonoBehaviour
{
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
    public GameObject ChatContainer;


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
                ChatContainer.SetActive(true);
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
    }
}
