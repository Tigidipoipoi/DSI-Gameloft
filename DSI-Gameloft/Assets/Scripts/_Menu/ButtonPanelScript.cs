using UnityEngine;
using System.Collections;

public class ButtonPanelScript : MonoBehaviour
{
    int ScriptPage;
    int PreviousPage;

    public PageScript Animator1;
    public PageScript Animator2;
    public PageScript Animator3;
    public PageScript Animator4;
    public PageScript Animator5;
    public PageScript Animator6;
    public PageScript Animator7;
    public PageScript Animator8;
    public PageScript Animator9;
    public PageScript Animator10;
    public PageScript Animator11;
    public PageScript Animator12;

    void Start()
    {
        ScriptPage = 1;
    }

    public void Next()
    {
        if (ScriptPage < 12)
        {
            PreviousPage = ScriptPage;
            ScriptPage++;
            Change();
        }

        if (ScriptPage == 12)
        {
            Application.LoadLevel("StartMenu");
        }
    }

    public void Previous()
    {
        if (ScriptPage > 1)
        {
            PreviousPage = ScriptPage;
            ScriptPage--;
            Change();
        }

        if (ScriptPage == 1)
        {
            Application.LoadLevel("StartMenu");
        }
    }

    void Change()
    {
        switch (PreviousPage)
        {
            case 1:
                Animator1.Change(0);
                Animator2.Change(1);
            break;
        }
    }
}