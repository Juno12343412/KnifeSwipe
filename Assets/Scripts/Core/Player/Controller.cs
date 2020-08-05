using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Pooling;

namespace KnifeSwipe
{
    public class Controller : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
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

        void OnTouch(Vector2 vecTouch)
        {
            vecMove = Vector2.zero;
            Vector2 vec = new Vector2(vecTouch.x - back.position.x, vecTouch.y - back.position.y);

            vec = Vector2.ClampMagnitude(vec, maxVec);
            joyStick.localPosition = vec;

            vecNormal = vec.normalized;
            float sqr = (back.position - joyStick.position).sqrMagnitude / (maxVec * maxVec);

            vecMove = new Vector3(vecNormal.x * moveSpeed * Time.deltaTime * sqr, vecNormal.y * moveSpeed * Time.deltaTime * sqr, 0f);
            objLine.transform.eulerAngles = new Vector3(0f, 0f, -(Mathf.Atan2(-vecNormal.x, -vecNormal.y) * Mathf.Rad2Deg));
        }

        public void OnDrag(PointerEventData eventData)
        {
            objKnife.transform.position = new Vector3(0, -2.05f, 0f);
            OnTouch(eventData.position);
            isTouch = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnTouch(eventData.position);
            isTouch = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joyStick.localPosition = Vector2.zero;
            isTouch = false;

            Knife obj = poolKnife.Spawn(-vecMove, objLine.transform.rotation);
            obj.transform.position = new Vector3(obj.transform.position.x , -2.05f, 0f);
        }
    }
}