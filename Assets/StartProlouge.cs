using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class StartProlouge : MonoBehaviour
{
    private float TimeFlow;
    public float EntryInterval;
    private string Lov = "Œ“œ≤ª∂ƒ„ttttwwwwŒ“œ≤ª∂ƒ„wwww";
    private TextMeshPro tmp;
    [Space]
    public GameObject SceneJumper;


    // Start is called before the first frame update
    void Start()
    {
        TimeFlow = EntryInterval;
        tmp = GetComponent<TextMeshPro>();
        SceneJumper.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        TimeFlow -= Time.deltaTime;
        if (TimeFlow <=0 && Lov.Length>0)
        {
            if (Lov.Substring(0, 1) != "t" && Lov.Substring(0, 1) != "w")
            {
                tmp.text += Lov.Substring(0, 1);
                TimeFlow = EntryInterval;
            }
            else if (Lov.Substring(0, 1) == "t")
            {
                tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
                TimeFlow = EntryInterval/3f;
            }
            else if (Lov.Substring(0, 1) == "w")
            {
                //tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
                TimeFlow = EntryInterval;
            }
            Lov = Lov.Substring(1, Lov.Length - 1);
        }

        if (Lov.Length <= 0)
        {
            SceneJumper.SetActive(true);
        }
    }
}
