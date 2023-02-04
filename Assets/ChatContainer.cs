using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatContainer : MonoBehaviour
{
    public List<Transform> Chats = new List<Transform>();
    private float HeighInterval = -0.75f;
    private Vector3 StartVector3 = new Vector3(-1.8f, 2.75f, 0f);
    public float MessageInterval;
    private float Timeflow;
    private int EventNum = 0; //0:begin; 1:Love; 2:MilkTea; 3:ENE;

    [Space]
    public Sprite[] chatSprites;
    public GameObject preFab;
    public SpriteRenderer background;
    public Sprite EmptySprite;

    private void Start()
    {
        Timeflow = MessageInterval;
    }

    public void SendChat(int eve)
    {
        Timeflow = MessageInterval;
        EventNum = eve;
        preFab.GetComponent<SpriteRenderer>().sprite = chatSprites[EventNum+3];
        Chats.Add(preFab.transform);
        Instantiate(preFab, new Vector3(1.8f, 2.75f - .75f * Chats.Count, 0f), transform.rotation);
        if(Chats.Count == 5)
        {
            background.sprite = EmptySprite;
        }
        print(Chats.Count);
    }

    private void Update()
    {
        Timeflow-=Time.deltaTime;
        if (Timeflow <= 0) //Trigger Event
        {
            if(EventNum == 0)
            {
                preFab.GetComponent<SpriteRenderer>().sprite = chatSprites[EventNum];
                //Chats.Add(preFab.transform);
                Instantiate(preFab, StartVector3, transform.rotation);
                EventNum = -1;
            }
            else if(EventNum>0 && EventNum <= 3)//Speak by Virus
            {
                preFab.GetComponent<SpriteRenderer>().sprite = chatSprites[EventNum];
                Chats.Add(preFab.transform);
                Instantiate(preFab, new Vector3(-1.8f, 2.75f - .75f*Chats.Count, 0f), transform.rotation);
                EventNum = -1;
            }
        }
    }


}
