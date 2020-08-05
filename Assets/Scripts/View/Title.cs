using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : BaseScreen<Title>
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("Ingame");
        }
        else if (Input.anyKey)
        {
            SceneManager.LoadScene("Ingame");
        }
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
    }

    public override void HideScreen()
    {
        base.HideScreen();
    }
}
