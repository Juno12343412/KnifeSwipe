using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager.Sound;

public class Title : BaseScreen<Title>
{
    void Start()
    {
        SoundManager.instance.PlaySound("타이틀 배경음악");
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            SoundManager.instance.StopSFX();
            SceneManager.LoadScene("Ingame");
        }
        else if (Input.anyKey)
        {
            SoundManager.instance.StopSFX();
            SceneManager.LoadScene("Ingame");
        }
    }

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }
}
