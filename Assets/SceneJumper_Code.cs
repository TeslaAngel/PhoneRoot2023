using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper_Code : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public string CodeForRoot;

    [Space]
    public float WordPerSec;
    private float TimeFlowed;

    [Space]
    public string SceneToJump;

    private void Awake()
    {
        TimeFlowed = 0;
    }

    private void Update()
    {
        TimeFlowed += Time.deltaTime;
        if((int)(TimeFlowed*WordPerSec) < CodeForRoot.Length)
        {
            textMeshPro.text = CodeForRoot.Substring(0, (int)(TimeFlowed * WordPerSec));
        }
        if ((TimeFlowed) * WordPerSec >= CodeForRoot.Length)
        {
            textMeshPro.SetText("");
        }
        if ((TimeFlowed-.5f) * WordPerSec >= CodeForRoot.Length)
        {
            SceneManager.LoadScene(SceneToJump);
        }
    }
}
