using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordContainer : MonoBehaviour
{

    public List<SpriteRenderer> passwordIndicator;
    public Sprite UnmarkedIndicator;
    public Sprite MarkedIndicator;
    private int EnteredIndicator = 0;

    private float ShackNum;

    public void EnterNum(bool enter) // true is enter, false is delete
    {
        passwordIndicator[EnteredIndicator].sprite = MarkedIndicator;
        EnteredIndicator ++;
        if (EnteredIndicator >= passwordIndicator.Count)
        {
            EnteredIndicator = 0;
            for(int i = 0; i < passwordIndicator.Count; i++)
            {
                passwordIndicator[i].sprite = UnmarkedIndicator;
            }
            Shake();
        }
    }

    public void Shake()
    {

    }
    
}
