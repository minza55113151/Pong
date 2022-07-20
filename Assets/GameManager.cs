using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Setting")]
    public int maxRound;
    public int ballSpeed;

    public bool p1Ready = false;
    public bool p2Ready = false;
    private bool isGameStarted = false;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private TMP_Text readyText;
    [SerializeField] private TMP_Text nPlayerReadyText;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private TMP_Text winnerText;

    private GameObject ball;
    private PhotonView pv;

    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();

    }
    private void Start()
    {   
        
    }
    private void Update()
    {
        if (!isGameStarted)
        {
            UpdateNPlayerReady();            
            if (p1Ready && p2Ready)
            {
                isGameStarted = true;
                nPlayerReadyText.text = "";
                readyButton.SetActive(false);
                RoundStart();
            }
        }
    }
    [PunRPC]
    private void GameOver()
    {
        int winner = ScoreManager.instance.GetWinnerPlayer();
        string text = "Player " + winner.ToString() + " Win!!!";
        winnerText.text = text;
        Invoke("ClearWinnerText", 3f);
        p1Ready = false;
        p2Ready = false;
        isGameStarted = false;
        readyButton.SetActive(true);
    }
    private void ClearWinnerText()
    {
        winnerText.text = "";
    }
    public void HitWall(int p)
    {
        ball.GetComponent<Ball>().Destroy();
        if (PhotonNetwork.IsMasterClient)
        {
            AddScore(p);

            if (ScoreManager.instance.GetWinnerPlayer() != 0)
            {
                pv.RPC("GameOver", RpcTarget.All);
            }
        }

        Invoke("RoundStart", 3f);
    }
    private void AddScore(int p)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (p == 1)
        {
            PhotonView.Get(ScoreManager.instance).RPC("AddScore", RpcTarget.All, 2);
        }
        if (p == 2)
        {
            PhotonView.Get(ScoreManager.instance).RPC("AddScore", RpcTarget.All, 1);
        }
    }
    private void UpdateNPlayerReady()
    {
        if (!isGameStarted)
        {
            int allPlayerReady = (p1Ready ? 1 : 0) + (p2Ready ? 1 : 0);
            nPlayerReadyText.text = "Ready\n" + allPlayerReady.ToString() + "/2";
        }
    }
    public void ClickToReady()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            pv.RPC("PlayerReady", RpcTarget.All, 1);
        }
        else
        {
            pv.RPC("PlayerReady", RpcTarget.All, 2);
        }
    }
    [PunRPC]
    private void PlayerReady(int p)
    {
        if (p == 1)
        {
            p1Ready = !p1Ready;
        }
        if (p == 2)
        {
            p2Ready = !p2Ready;
        }
    }
    private void RoundStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
            ball.GetComponent<Ball>().StartGame();
        }
        StartCoroutine(ReadyGo());
    }
    private IEnumerator ReadyGo()
    {
        readyText.text = "3";
        yield return new WaitForSeconds(1);
        readyText.text = "2";
        yield return new WaitForSeconds(1);
        readyText.text = "1";
        yield return new WaitForSeconds(1);
        readyText.text = "Go!";
        yield return new WaitForSeconds(1);
        readyText.text = "";
    }

}
