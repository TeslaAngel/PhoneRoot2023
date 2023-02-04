using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBut : MonoBehaviour
{
    private Vector3 OriginScale;
    private Vector3 ScaleWhenDrag;
    public float DragScale;

    private Color initialColor;
    public Color MousingColor;
    private TextMeshPro tmp;

    public float ShrinkTimeLerp;

    private Transform transform;
    private bool Mousing = false; //is true when being pressing by mouse
    private bool Moving = false; //is true when moving

    [Space]
    public ChatContainer chatContainer;
    public int evenum;

    public GameObject CPanel;


    private void Awake()
    {
        transform = GetComponent<Transform>();
        OriginScale = transform.localScale;
        ScaleWhenDrag = OriginScale * DragScale;

        if (GetComponent<TextMeshPro>())
        {
            tmp = GetComponent<TextMeshPro>();
            initialColor = tmp.color;
        }
        else
        {
            initialColor = GetComponent<SpriteRenderer>().color;
        }
        
    }

    private void OnMouseDown()
    {
        Mousing = true;
        Moving = true;
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
            if (chatContainer)
            {
                chatContainer.SendChat(evenum);
                Destroy(gameObject);
            }
            else
            {
                CPanel.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Mousing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, ScaleWhenDrag, ShrinkTimeLerp * Time.deltaTime);
            if(tmp)
                tmp.color = Color.Lerp(tmp.color, MousingColor, ShrinkTimeLerp * 2f * Time.deltaTime);
            else
                GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, MousingColor, ShrinkTimeLerp * 2f * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, OriginScale, ShrinkTimeLerp * Time.deltaTime);
            if(tmp)
                tmp.color = Color.Lerp(tmp.color, initialColor, ShrinkTimeLerp * 2f * Time.deltaTime);
            else
                GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, initialColor, ShrinkTimeLerp * 2f * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Moving = false;
        }
    }
}
