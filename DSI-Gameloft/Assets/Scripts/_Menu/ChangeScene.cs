using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    int ScriptPage;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        ScriptPage = 1;
    }

    public void Next()
    {
        if (ScriptPage < 11)
        {
            ScriptPage++;
            anim.SetInteger("Page", ScriptPage);
        }

        if (ScriptPage == 11)
        {
            Application.LoadLevel("StartMenu");
        }
    }

    public void Previous()
    {
        if (ScriptPage > 1)
        {
            ScriptPage--;
            anim.SetInteger("Page", ScriptPage);
        }

        if (ScriptPage == 1)
        {
            Application.LoadLevel("StartMenu");
        }
    }

    public void Increase()
    {
        ScriptPage++;
    }

    public void Decrease()
    {
        Application.LoadLevel("StartMenu");
    }
}