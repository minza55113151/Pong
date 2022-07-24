using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExplosiveShard : MonoBehaviour
{
    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
            return;
        }
        Invoke("Destroy", 2f);
    }
    private void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
