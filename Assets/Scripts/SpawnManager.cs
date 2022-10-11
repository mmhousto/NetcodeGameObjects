using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;



public class SpawnManager : NetworkBehaviour
{
    private NetworkVariable<int> enemiesToSpawn = new NetworkVariable<int>();
    public GameObject[] spawnPoints;
    public NetworkManager networkManager;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemiesToSpawn.Value = 2;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        //SpawnClientRpc();
    }




    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Enemy") && IsServer)
        {
            enemiesToSpawn.Value += 2;
            //GameManager.Instance.NextWave();
            SpawnClientRpc();
        }
    }



    [ClientRpc]
    private void SpawnClientRpc()
    {
        for (int i = 0; i < enemiesToSpawn.Value; i++)
        {
            int j = Random.Range(0, spawnPoints.Length);
            GameObject clone = Instantiate(enemy,
                spawnPoints[j].transform.position,
                spawnPoints[j].transform.rotation);
            clone.GetComponent<NetworkObject>().Spawn();
        }
    }
}