using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : NetworkBehaviour
{
    public NetworkManager networkManager;
    private NetworkObject localPlayer;
    private CinemachineVirtualCamera cm;

    private void Start()
    {
        cm = GetComponent<CinemachineVirtualCamera>();
    }

    public void Update()
    {
        if (networkManager.LocalClient != null && networkManager.LocalClient.PlayerObject.IsLocalPlayer)
        {
            if (localPlayer == null)
            {
                localPlayer = networkManager.LocalClient.PlayerObject;
            }
            if (localPlayer != null)
            {
                cm.Follow = localPlayer.transform;
            }
        }
        
    }
}
