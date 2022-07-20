using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{

    public float minY;
    public float maxY;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            Vector3 mousePos = Input.mousePosition;
            float worldPositionY = Camera.main.ScreenToWorldPoint(mousePos).y;
            float halfY = transform.localScale.y;
            if (worldPositionY < minY + halfY)
            {
                worldPositionY = minY + halfY;
            }
            else if (worldPositionY > maxY - halfY)
            {
                worldPositionY = maxY - halfY;
            }
            transform.position = new Vector3(transform.position.x, worldPositionY, transform.position.z);
        }
    }

}
