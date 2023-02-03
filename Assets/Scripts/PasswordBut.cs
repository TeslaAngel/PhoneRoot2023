using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PasswordBut : MonoBehaviour
{
    private Vector3 OriginScale;
    private Vector3 ScaleWhenDrag;
    public float DragScale;

    private Color initialColor;
    public Color MousingColor;
    private SpriteRenderer spriteRenderer;

    public float ShrinkTimeLerp;

    private Transform transform;
    private bool Mousing = false; //is true when being dragged by mouse

    private void Awake()
    {
        transform = GetComponent<Transform>();
        OriginScale = transform.localScale;
        ScaleWhenDrag = OriginScale * DragScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

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
        //then enter a num
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
    }
}
