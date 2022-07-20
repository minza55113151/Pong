using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{

    [SerializeField] private float speed = 10f;

    private PhotonView pv;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Destroy(rb);
        }
        speed = GameManager.instance.ballSpeed;
    }
    public void StartGame()
    {
        Invoke("StartMoveBall", 3f);
    }
    private void StartMoveBall()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;
        rb.velocity = direction * speed;
    }
    public void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
        //create bomb!!!
        //
        //
    }
}
