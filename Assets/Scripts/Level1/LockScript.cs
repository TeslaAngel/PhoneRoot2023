using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public GameObject SceneSwitcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<KeyScript>() != null)
        {
            print("unlocked");
            //Jump to next scene / play jumpscene animation
            SceneSwitcher.SetActive(true);
        }
    }
}
