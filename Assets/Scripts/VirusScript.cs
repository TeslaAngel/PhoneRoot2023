using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VirusScript : MonoBehaviour
{
    public Sprite[] Sprites;
    public float AnimationInterval;
    private float AnimatedInterval;
    private int AniProcess = 0;
    private SpriteRenderer spriteRenderer;

    [Space]
    public float life;
    private void OnCollisionStay2D(Collision2D collision)
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            this.GetComponent<VirusScript>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AnimatedInterval = AnimationInterval;
        spriteRenderer = GetComponent<SpriteRenderer>();
        AniProcess = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatedInterval -= Time.deltaTime;
        if(AnimatedInterval <= 0f)
        {
            AnimatedInterval = AnimationInterval;
            AniProcess++;
            if (AniProcess >= Sprites.Length)
            {
                AniProcess = 0;
            }
            spriteRenderer.sprite = Sprites[AniProcess];
        }

    }
}
