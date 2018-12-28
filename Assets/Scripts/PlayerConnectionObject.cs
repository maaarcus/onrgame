using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject playerModel;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        CmdSpawnPlayer();


    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

    }

    [Command]
    void CmdSpawnPlayer()
    {
        Debug.Log("Asking Server to spawn playerModel");
        GameObject go = Instantiate(playerModel);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
