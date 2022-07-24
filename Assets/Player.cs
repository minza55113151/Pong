using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float minY;
    public float maxY;
    public int playerID;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerID = 1;
        }
        else
        {
            playerID = 2;
        }
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
    [PunRPC]
    public void Width(int p, float scale, float sec)
    {
        if (p != playerID)
            return;
        Vector3 localScale = transform.localScale;
        transform.localScale.Scale(new Vector3(localScale.x, localScale.y * scale, localScale.z));
        StartCoroutine(ResetWidth(scale, sec));
    }
    private IEnumerator ResetWidth(float scale, float sec)
    {
        yield return new WaitForSeconds(sec);
        Vector3 localScale = transform.localScale;
        transform.localScale.Scale(new Vector3(localScale.x, localScale.y / scale, localScale.z));
    }
}
