using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreWall : MonoBehaviour
{

    public static ScoreWall instance;

    [SerializeField] private int playerN;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.HitWall(playerN);
        }
    }
}
