using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanel : MonoBehaviour, IDragHandler
{

    public Transform player;
    public Transform child;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = player.position;
        position.x = Mathf.Clamp(position.x + (eventData.delta.x / 100), -3.76f, 3.76f);
        player.position = position;

        Quaternion rotation = child.rotation;
        rotation.y = Mathf.Clamp(rotation.y + (eventData.delta.x / 500), -3.76f, 3.76f);
        child.rotation = rotation;
    }

}
