using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mainu : BaseScreen<Mainu>
{
    public class ButtonState
    {
        public bool   isCheck = false;
        public Button myButton = null;

        public ButtonState(bool _isCheck, Button _button)
        {
            isCheck = _isCheck;
            myButton = _button;
        }
    }
    public Dictionary<string, ButtonState> mainButtons;
    public Sprite[] checkImages;

    void Start()
    {
        mainButtons = new Dictionary<string, ButtonState>()
        {
            { "Stats", new ButtonState(true, GameObject.Find("StatButton").GetComponent<Button>()) },
            { "Upgrade", new ButtonState(true, GameObject.Find("UpgradeButton").GetComponent<Button>()) },
            { "Boss",  new ButtonState(true, GameObject.Find("BossButton").GetComponent<Button>()) }
        };
    }

    public void OnStats()
    {
        if (!mainButtons["Stats"].isCheck)
        {
            Ingame.instance.HideScreen();
            Stats.instance.ShowScreen();
            Upgrade.instance.HideScreen();
            Boss.instance.HideScreen();
        } else {
            Ingame.instance.ShowScreen();
            Stats.instance.HideScreen();
            Upgrade.instance.HideScreen();
            Boss.instance.HideScreen();
        }
    }

    public void OnUpgrade()
    {
        if (!mainButtons["Upgrade"].isCheck)
        {
            Ingame.instance.HideScreen();
            Stats.instance.HideScreen();
            Upgrade.instance.ShowScreen();
            Boss.instance.HideScreen();
        } else {
            Ingame.instance.ShowScreen();
            Stats.instance.HideScreen();
            Upgrade.instance.HideScreen();
            Boss.instance.HideScreen();
        }
    }

    public void OnBoss()
    {
        if (!Boss.instance.isBoss && !mainButtons["Boss"].isCheck)
        {
            Ingame.instance.HideScreen();
            Stats.instance.HideScreen();
            Upgrade.instance.HideScreen();
            Boss.instance.ShowScreen();
        } else {
            Ingame.instance.ShowScreen();
            Stats.instance.HideScreen();
            Upgrade.instance.HideScreen();
            Boss.instance.HideScreen();
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
