using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.2f;
    private RectTransform recttransform;
    [SerializeField] private Sprite[] ButtonImages = new Sprite[2];
    

    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
        panelLocation = recttransform.anchoredPosition;
        GameObject.Find("StatButton").GetComponent<Image>().sprite = ButtonImages[1];
        GameObject.Find("UpgradeButton").GetComponent<Image>().sprite = ButtonImages[1];
        GameObject.Find("BossButton").GetComponent<Image>().sprite = ButtonImages[1];
        GameObject.Find("FieldButton").GetComponent<Image>().sprite = ButtonImages[0];
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = (data.pressPosition.x - data.position.x) /800;

        recttransform.anchoredPosition = panelLocation - new Vector3(difference, 0, 0);
        //panelLocation = recttransform.anchoredPosition;
    }
    public void OnEndDrag(PointerEventData data)
    {
        
        Debug.Log((data.pressPosition.x - data.position.x) / 800);
        float percentage = (data.pressPosition.x - data.position.x) / 800;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0)
            {
                if (!(newLocation.x <= -800))
                {
                    newLocation += new Vector3(-800, 0, 0);
                }
            }
            else if (percentage < 0)
            {
                if (!(newLocation.x >= 1600))
                {
                    newLocation += new Vector3(800, 0, 0);
                }
            }
            StartCoroutine(SmoothMove(recttransform.anchoredPosition, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(recttransform.anchoredPosition, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 start, Vector3 end, float seconds)
    {
        switch (end.x)
        {
            case -800://boss
                GameObject.Find("StatButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("UpgradeButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("BossButton").GetComponent<Image>().sprite = ButtonImages[0];
                GameObject.Find("FieldButton").GetComponent<Image>().sprite = ButtonImages[1];
                Mainu.instance.mainButtons["Boss"].isCheck = true;
                break;
            case 0://field
                GameObject.Find("StatButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("UpgradeButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("BossButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("FieldButton").GetComponent<Image>().sprite = ButtonImages[0];
                break;
            case 800://upgrade
                GameObject.Find("StatButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("UpgradeButton").GetComponent<Image>().sprite = ButtonImages[0];
                GameObject.Find("BossButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("FieldButton").GetComponent<Image>().sprite = ButtonImages[1];
                break;
            case 1600://stats
                GameObject.Find("StatButton").GetComponent<Image>().sprite = ButtonImages[0];
                GameObject.Find("UpgradeButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("BossButton").GetComponent<Image>().sprite = ButtonImages[1];
                GameObject.Find("FieldButton").GetComponent<Image>().sprite = ButtonImages[1];
                break;

        }
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            recttransform.anchoredPosition = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void ClickStats()
    {
        StartCoroutine(SmoothMove(recttransform.anchoredPosition,new Vector3(1600,0,0), easing));
        
    }
    public void ClickUpgrade()
    {
        StartCoroutine(SmoothMove(recttransform.anchoredPosition, new Vector3(800, 0, 0), easing));
        
    }
    public void ClickField()
    {
        StartCoroutine(SmoothMove(recttransform.anchoredPosition, new Vector3(0, 0, 0), easing));
        

    }
    public void ClickBoss()
    {
        StartCoroutine(SmoothMove(recttransform.anchoredPosition, new Vector3(-800, 0, 0), easing));
        
    }


}
