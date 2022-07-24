using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    private int maxRound;
    public int p1ScoreInt = 0;
    public int p2ScoreInt = 0;
    [SerializeField] private TMP_Text p1Score;
    [SerializeField] private TMP_Text p2Score;
    private PhotonView pv;

    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
        maxRound = GameManager.instance.maxRound;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetWinnerPlayer()
    {
        if (p1ScoreInt >= maxRound)
            return 1;
        if (p2ScoreInt >= maxRound)
            return 2;
        return 0;
    }
    public void ResetScore()
    {
        p1ScoreInt = 0;
        p2ScoreInt = 0;
        p1Score.text = "0";
        p2Score.text = "0";
    }
    [PunRPC]
    public void AddScore(int p)
    {
        if (p == 1)
        {
            p1ScoreInt++;
            p1Score.text = p1ScoreInt.ToString();
        }
        if (p == 2)
        {
            p2ScoreInt++;
            p2Score.text = p2ScoreInt.ToString();
        }
    }
}
