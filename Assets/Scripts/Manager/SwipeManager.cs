using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Manager.Sound;

public enum ViewState
{
    Ingame, Stats,
    Upgrade ,Boss
}

public class ButtonState
{
    public bool isCheck = false;
    public Button myButton = null;

    public ButtonState(bool _isCheck, Button _button)
    {
        isCheck = _isCheck;
        myButton = _button;
    }
}

public class SwipeManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static SwipeManager instance; 

    public Dictionary<ViewState, ButtonState> mainButtons;
    public Sprite[] checkImages;

    private ViewState oldView, curView;
    private float curCamPos = 0f;
    private bool isMove = false;

    void Awake()
    {
        instance = GetComponent<SwipeManager>();
    }

    void Start()
    {
        curView = oldView = ViewState.Ingame;
        mainButtons = new Dictionary<ViewState, ButtonState>()
        {
            { ViewState.Stats, new ButtonState(true, GameObject.Find("StatButton").GetComponent<Button>()) },
            { ViewState.Upgrade, new ButtonState(true, GameObject.Find("UpgradeButton").GetComponent<Button>()) },
            { ViewState.Boss,  new ButtonState(true, GameObject.Find("BossButton").GetComponent<Button>()) },
            { ViewState.Ingame,  new ButtonState(true, GameObject.Find("FieldButton").GetComponent<Button>()) }
        };
        mainButtons[curView].myButton.image.sprite = checkImages[1];
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (isMove || Boss.instance.isBoss)
        //    return;

        //// Stats , Upgrade , Ingame , Boss
        //oldView = curView;
        //Vector2 p = eventData.position - eventData.pressPosition;

        //if (Mathf.Abs(p.x) > Mathf.Abs(p.y))
        //{
        //    if (Mathf.Sign(p.x) == Vector2.left.x)
        //    {
        //        print("Left");
        //        if (curView != ViewState.Boss)
        //        {
        //            curView += 1;
        //            curCamPos += 5.6f;
        //        }
        //    }
        //    else if (Mathf.Sign(p.x) == Vector2.right.x)
        //    {
        //        print("Right");
        //        if (curView != ViewState.Ingame)
        //        {
        //            curView -= 1;
        //            curCamPos -= 5.6f;
        //        }
        //    }
        //}

        //mainButtons[ViewState.Ingame].isCheck = false;
        //mainButtons[ViewState.Stats].isCheck = false;
        //mainButtons[ViewState.Upgrade].isCheck = false;
        //mainButtons[ViewState.Boss].isCheck = false;
        //mainButtons[ViewState.Ingame].myButton.image.sprite = checkImages[0];
        //mainButtons[ViewState.Stats].myButton.image.sprite = checkImages[0];
        //mainButtons[ViewState.Upgrade].myButton.image.sprite = checkImages[0];
        //mainButtons[ViewState.Boss].myButton.image.sprite = checkImages[0];

        //mainButtons[oldView].isCheck = false;
        //mainButtons[curView].isCheck = true;
        //mainButtons[oldView].myButton.image.sprite = checkImages[0];
        //mainButtons[curView].myButton.image.sprite = checkImages[1];

        //StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }

    public void ChanageView(ViewState view, float cPos)
    {
        SoundManager.instance.StopSFX();

        oldView = curView;
        curView = view;
        curCamPos = cPos;

        mainButtons[oldView].isCheck = false;
        mainButtons[curView].isCheck = true;
        mainButtons[oldView].myButton.image.sprite = checkImages[0];
        mainButtons[curView].myButton.image.sprite = checkImages[1];

        StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }

    public IEnumerator SmoothMove(float aPos = 0f, float seconds = 1f)
    {
        GameLogic.instance.Save();
        isMove = true;
        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime / seconds;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(aPos, 0f, -10f), Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        isMove = false;
    }

    public void OnStats()
    {
        if (isMove)
            return;

        oldView = curView;
        curView = ViewState.Stats;
        curCamPos = 5.6F;

        mainButtons[ViewState.Ingame].isCheck = false;
        mainButtons[ViewState.Stats].isCheck = false;
        mainButtons[ViewState.Upgrade].isCheck = false;
        mainButtons[ViewState.Boss].isCheck = false;
        mainButtons[ViewState.Ingame].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Stats].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Upgrade].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Boss].myButton.image.sprite = checkImages[0];

        mainButtons[oldView].isCheck = false;
        mainButtons[curView].isCheck = true;
        mainButtons[oldView].myButton.image.sprite = checkImages[0];
        mainButtons[curView].myButton.image.sprite = checkImages[1];

        StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }

    public void OnUpgrade()
    {
        if (isMove)
            return;

        oldView = curView;
        curView = ViewState.Upgrade;
        curCamPos = 11.2F;

        mainButtons[ViewState.Ingame].isCheck = false;
        mainButtons[ViewState.Stats].isCheck = false;
        mainButtons[ViewState.Upgrade].isCheck = false;
        mainButtons[ViewState.Boss].isCheck = false;
        mainButtons[ViewState.Ingame].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Stats].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Upgrade].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Boss].myButton.image.sprite = checkImages[0];

        mainButtons[oldView].isCheck = false;
        mainButtons[curView].isCheck = true;
        mainButtons[oldView].myButton.image.sprite = checkImages[0];
        mainButtons[curView].myButton.image.sprite = checkImages[1];

        StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }

    public void OnGame()
    {
        if (isMove)
            return;

        Ingame.instance.ShowBackGround();

        oldView = curView;
        curView = ViewState.Ingame;
        curCamPos = -0F;

        mainButtons[ViewState.Ingame].isCheck = false;
        mainButtons[ViewState.Stats].isCheck = false;
        mainButtons[ViewState.Upgrade].isCheck = false;
        mainButtons[ViewState.Boss].isCheck = false;
        mainButtons[ViewState.Ingame].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Stats].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Upgrade].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Boss].myButton.image.sprite = checkImages[0];

        mainButtons[oldView].isCheck = false;
        mainButtons[curView].isCheck = true;
        mainButtons[oldView].myButton.image.sprite = checkImages[0];
        mainButtons[curView].myButton.image.sprite = checkImages[1];

        StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }

    public void OnBoss()
    {
        if (isMove)
            return;

        Boss.instance.ResetBossView();

        oldView = curView;
        curView = ViewState.Boss;
        curCamPos = 16.8F;

        mainButtons[ViewState.Ingame].isCheck = false;
        mainButtons[ViewState.Stats].isCheck = false;
        mainButtons[ViewState.Upgrade].isCheck = false;
        mainButtons[ViewState.Boss].isCheck = false;
        mainButtons[ViewState.Ingame].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Stats].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Upgrade].myButton.image.sprite = checkImages[0];
        mainButtons[ViewState.Boss].myButton.image.sprite = checkImages[0];

        mainButtons[oldView].isCheck = false;
        mainButtons[curView].isCheck = true;
        mainButtons[oldView].myButton.image.sprite = checkImages[0];
        mainButtons[curView].myButton.image.sprite = checkImages[1];

        StartCoroutine(SmoothMove(curCamPos, 0.5f));
    }
}
