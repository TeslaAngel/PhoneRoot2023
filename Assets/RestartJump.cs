using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartJump : MonoBehaviour
{
    public void jump()
    {
        SceneManager.LoadScene("Scene1");
    }
}
