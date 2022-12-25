using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    bool started = false;
    float collectedCount;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //started = true;
        }
        if (started)
        {
            //transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectable")
        {
            Transform collectPoint = transform.GetChild(0);
            other.gameObject.transform.position = collectPoint.position + new Vector3(0, 0.5f, 0) * collectedCount;
            other.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            other.gameObject.transform.SetParent(collectPoint);
            collectedCount = collectedCount + 1;
        }
    }

}
