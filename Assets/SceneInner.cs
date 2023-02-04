using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneInner : MonoBehaviour
{
    public float SSTime;

    private float SSedTime;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        SSedTime = SSTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        SSedTime -= Time.deltaTime;
        if (SSedTime > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, SSedTime / SSTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
