    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KebappSoft.Core.Singletons;
using Unity.Netcode;


public class PlayersManager : NetworkSingleton<PlayersManager>
{
    private NetworkVariable<int> playerInGame = new NetworkVariable<int>();
    public int PlayerIngame
    {

        get
        {

            return playerInGame.Value;
        }
            

    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {

            if (IsServer)
            {
              
                playerInGame.Value++;
            }
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {

            if (IsServer)
            {
               
                playerInGame.Value--;
            }
        };
    }

}
 