using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
    
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] private float itemSpawnRate;
    [SerializeField] private GameObject[] itemPrefabs;

    PhotonView pv;
    float time;

    private void Awake()
    {
        instance = this;
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
            return;
        }
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= itemSpawnRate)
        {
            time -= itemSpawnRate;
            SpawnItem();
        }
    }
    private void SpawnItem()
    {
        int itemIndex = Random.Range(0, itemPrefabs.Length);
        GameObject item = itemPrefabs[itemIndex];
        Vector3 position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
        PhotonNetwork.Instantiate(item.name, position, Quaternion.identity);
    }
    public void UseItem(string itemName, int p)
    {
        switch (itemName)
        {
            case "decreaseWidth":
                PhotonView.Get(Player.instance).RPC("Width", RpcTarget.All, p, 0.5f, 5f);
                break;
            case "increaseWidth":
                PhotonView.Get(Player.instance).RPC("Width", RpcTarget.All, p, 2f, 5f);
                break;
            case "decreaseSpeed":
                
                break;
            case "increaseSpeed":
                break;
            case "decreaseAmount":
                break;
            case "increaseAmount":
                break;
            case "decreaseSize":
                break;
            case "increaseSize":
                break;
            case "spawnLine":
                break;

            default:
                break;
        }
    }
    /*item 
 * width
 * speed
 * amount
 * size
 * line
 * 
 * 
 * 
 */
}
