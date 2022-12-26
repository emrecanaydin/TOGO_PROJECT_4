using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Barrier")
        {
            transform.parent.gameObject.GetComponent<PlayerController>().isBarrierClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Barrier")
        {
            transform.parent.gameObject.GetComponent<PlayerController>().isBarrierClose = false;
        }
    }
}
