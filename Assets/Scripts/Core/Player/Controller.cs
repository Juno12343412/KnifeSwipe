using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Pooling;

namespace KnifeSwipe
{
    public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler //, IDragHandler
    {
        [SerializeField] private RectTransform back;
        [SerializeField] private RectTransform joyStick;

        [SerializeField] private GameObject objLine;
        [SerializeField] private GameObject objKnife;

        Vector3 vecMove;
        Vector2 vecNormal;
 
        float maxVec;
        float moveSpeed = 5f;

        bool isTouch = false;

        ObjectPool<Knife> poolKnife = new ObjectPool<Knife>();

        void Start()
        {
            poolKnife.Init(objKnife, 10);
            maxVec = back.rect.width * 0.5f;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isTouch = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isTouch = false;

            Knife obj = poolKnife.Spawn(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f));
            obj.transform.position = new Vector3(obj.transform.position.x, -2.05f, 0f);
        }
    }
}