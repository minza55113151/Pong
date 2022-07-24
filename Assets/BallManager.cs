using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    [SerializeField] private GameObject ballPrefab;
    public float startSpeed = 10f;
    public float speed = 10f;

    private float time;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        startSpeed = GameManager.instance.ballSpeed;
        speed = GameManager.instance.ballSpeed;
    }

    private void Update()
    {
        SpeedControll();
        SpeedGrow();
    }
    private void SpeedControll()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            Rigidbody2D rb = balls[i].GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    private void SpeedGrow()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            time -= 1f;
            speed += 0.5f;
        }
    }

    public void SpawnBall()
    {
        speed = startSpeed;
        GameObject ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
        ball.GetComponent<Ball>().StartGame();
    }
    public void DestroyAllBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            DestroyBall(balls[i]);
        }
    }
    public void DestroyBall(GameObject ball)
    {
        ball.GetComponent<Ball>().Destroy();
    }
}
