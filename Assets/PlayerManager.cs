using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer(-8);
        }
        else
        {
            SpawnPlayer(8);
        }
    }

    void Update()
    {
        
    }
    
    private void SpawnPlayer(int x)
    {
        Vector2 pos = new Vector2(x, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity, 0);
    }
}
