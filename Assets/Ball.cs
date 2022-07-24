using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{

    public float startSpeed = 10f;
    public float speed = 10f;
    public int lastPlayerTouch = 0;

    public int explosiveShardAmount = 36;
    [SerializeField] private GameObject explosiveShardPrefab;


    private PhotonView pv;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(this);
            Destroy(rb);
            return;
        }
        startSpeed = BallManager.instance.startSpeed;
        speed = BallManager.instance.speed;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlayHitSound();
        if (collision.gameObject.tag == "Player")
        {
            lastPlayerTouch = collision.gameObject.GetComponent<Player>().playerID;
        }
        if (rb.velocity.x < 1f && rb.velocity.x > -1f)
        {
            rb.velocity = new Vector2(Random.Range(-1f, 1f), rb.velocity.y).normalized * speed;
        }
        if (rb.velocity.y < 1f && rb.velocity.y > -1f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(-1f, 1f)).normalized * speed;
        }
    }



    public void Destroy()
    {
        SoundManager.instance.PlayTNTSound();
        for (int i = 0; i < explosiveShardAmount; i++)
        {
            GameObject explosiveShard = PhotonNetwork.Instantiate(explosiveShardPrefab.name, gameObject.transform.position, Quaternion.identity);
            explosiveShard.transform.Rotate(new Vector3(0f, 0f, i));
            explosiveShard.GetComponent<Rigidbody2D>().velocity = new Vector3(30f, 0f, 0f);
        }
        PhotonNetwork.Destroy(gameObject);
    }
/*    
    private void DestroyAllExplosiveShard()
    {
        Debug.Log("destroy1");
        GameObject[] explosiveShards = GameObject.FindGameObjectsWithTag("ExplosiveShard");
        foreach (GameObject explosiveShard in explosiveShards)
        {
            PhotonNetwork.Destroy(explosiveShard);
            Debug.Log("destroyyy");
        }
        Debug.Log("destroy2");
    }*/
}
