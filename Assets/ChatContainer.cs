using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShelter
{
    public static int InNumof = Random.Range(1, 3);
    public static GameObject HB;
}

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
    public GameObject VirusPreFab;
    public SpriteRenderer background;
    public Sprite EmptySprite;
    public GameObject HB;
    private void Awake()
    {
        VirusShelter.HB = HB;
    }

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
                preFab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                Instantiate(preFab, StartVector3, transform.rotation);
                EventNum = -1;
            }
            else if(EventNum>0 && EventNum <= 3)//Speak by Virus
            {
                preFab.GetComponent<SpriteRenderer>().sprite = chatSprites[EventNum];
                VirusPreFab.GetComponent<SpriteRenderer>().sprite = chatSprites[EventNum];
                Chats.Add(preFab.transform);
                if(Chats.Count == VirusShelter.InNumof*2)
                {
                    //Make a bubble with virus
                    //preFab.GetComponent<SpriteRenderer>().color = new Color(.95f, .75f, .95f);
                    Instantiate(VirusPreFab, new Vector3(-1.8f, 2.75f - .75f * Chats.Count, 0f), transform.rotation);
                    VirusShelter.InNumof = -1;
                }
                else
                {
                    preFab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    Instantiate(preFab, new Vector3(-1.8f, 2.75f - .75f * Chats.Count, 0f), transform.rotation);
                }
                //Instantiate(preFab, new Vector3(-1.8f, 2.75f - .75f*Chats.Count, 0f), transform.rotation);
                EventNum = -1;
            }
        }
    }


}
