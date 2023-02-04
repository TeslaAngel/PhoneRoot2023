using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class SwitchSceneAnimation : MonoBehaviour
{
    public float SSTime;
    public string nextScene;

    private float SSedTime;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        SSedTime = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SSedTime += Time.deltaTime;
        if (SSedTime <= SSTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, SSedTime / SSTime);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
