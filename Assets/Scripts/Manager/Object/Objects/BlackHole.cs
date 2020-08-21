using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    GameObject comeObj = null;

    void Start()
    {
        comeObj = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(gameObject, 2.5f);
    }

    void FixedUpdate()
    {
        if (comeObj != null)
        {
            Vector3 direction = comeObj.transform.position - transform.position;
            comeObj.transform.position -= direction.normalized * 5f * Time.deltaTime;
            Debug.Log(direction.normalized);
        }
        else
        {
            comeObj = GameObject.FindGameObjectWithTag("Enemy");
        }
    }
}
