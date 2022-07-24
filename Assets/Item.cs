using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviour
{
    public string itemName;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ItemManager.instance.UseItem(itemName, collision.gameObject.GetComponent<Ball>().lastPlayerTouch);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
