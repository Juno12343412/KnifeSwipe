using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Pooling;

namespace KnifeSwipe
{
    public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler //, IDragHandler
    {
        [SerializeField] private GameObject objKnife;
        ObjectPool<Knife> poolKnife = new ObjectPool<Knife>();

        void Start()
        {
            poolKnife.Init(objKnife, 10);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (SwipeManager.instance.mainButtons[ViewState.Ingame].isCheck)
            {
                Knife obj = poolKnife.Spawn(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f));
                obj.transform.position = new Vector3(obj.transform.position.x, -2.05f, 0f);
            }
        }
    }
}