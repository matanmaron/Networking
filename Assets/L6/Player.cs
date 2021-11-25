using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L6
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public bool isMyTurn;
        [SyncVar] public bool isX;
        [SerializeField] Text TurnTxt;

        public override void OnStartServer()
        {
            base.OnStartServer();
            isMyTurn = GameManager.Instance.IsMyTurn(this.connectionToClient.identity.netId);
            Debug.Log($"server: {this.connectionToClient.identity.netId} myTurn {isMyTurn}");
            //setup isx according to num of player
            RPCUpdateTurn(isMyTurn);
        }

        private void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(TurnTxt);
            }
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CmdTakeAction(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CmdTakeAction(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CmdTakeAction(3);
            }
        }

        [Command]
        private void CmdTakeAction(int squareID)
        {
            //check valid
            //take action
            // next tuen
            GameManager.Instance.NextTurn();
        }

        [ClientRpc]
        public void RPCUpdateTurn(bool turn)
        {
            isMyTurn = turn;
            if (TurnTxt && isMyTurn)
            {
                TurnTxt.text = "my turn";
            }
            else if (TurnTxt)
            {
                TurnTxt.text = string.Empty;

            }
        }
    }
}