using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public bool isMyTurn;
        [SyncVar] public bool isX;

        public override void OnStartServer()
        {
            base.OnStartServer();
            isMyTurn = GameManager.Instance.IsMyTurn(this.connectionToClient.identity.netId);
            Debug.Log($"server: {this.connectionToClient.identity.netId} myTurn {isMyTurn}");
            //setup isx according to num of player
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            if (!isMyTurn)
            {
                return;
            }
            //check input
            CmdTakeAction(1);
        }

        [Command]
        private void CmdTakeAction(int squareID)
        {
            //check valid
            //take action
            // next tuen
            GameManager.Instance.NextTurn();
        }
    }
}