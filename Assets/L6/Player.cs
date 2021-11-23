using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public bool isMyTurn;
        [SyncVar] public bool IsX;

        public override void OnStartServer()
        {
            base.OnStartServer();
            isMyTurn = GameManager.Instance.IsMyTurn(this.connectionToClient.identity.netId);
            GameManager.Instance.OnConnect();
            Debug.Log($"server: {this.connectionToClient.identity.netId} , myTurn=>{isMyTurn}");
        }
    }
}