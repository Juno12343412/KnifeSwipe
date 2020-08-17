using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Pooling;

namespace KnifeSwipe
{
    public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler //, IDragHandler
    {
        public static Controller instance;

        [SerializeField] private GameObject objKnife;
        ObjectPool<Knife> poolKnife = new ObjectPool<Knife>();

        void Awake()
        {
            instance = GetComponent<Controller>();
        }

        void Start()
        {
            poolKnife.Init(objKnife, 10);
            ChanageKnife();
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

        public void ChanageKnife()
        {
            foreach (var obj in poolKnife)
            {
                if (obj != null)
                {
                    obj.GetComponent<SpriteRenderer>().sprite = PlayerStats.instance.knifeImgs[PlayerStats.instance.stats.knifeLv - 1];
                    objKnife.GetComponent<SpriteRenderer>().sprite = PlayerStats.instance.knifeImgs[PlayerStats.instance.stats.knifeLv - 1];
                }
            }
        }
    }
}