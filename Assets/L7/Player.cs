using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace L7
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public int mySign = (int)CellType.None;
        [SerializeField] Text TurnTxt;
        private bool isMyTurn => GameManager.Instance.Turn == mySign;

        public override void OnStartServer()
        {
            base.OnStartServer();
            Debug.Log($"server: {this.connectionToClient.identity.netId}");
            //setup isx according to num of player
            mySign = GameManager.Instance.GetSign();
        }

        private void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(TurnTxt);
            }
            SetTurn(isMyTurn, (CellType)mySign);
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
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CmdTakeAction(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                CmdTakeAction(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                CmdTakeAction(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                CmdTakeAction(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                CmdTakeAction(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                CmdTakeAction(9);
            }
        }

        [Command]
        private void CmdTakeAction(int squareID)
        {
            //check valid
            //take action
            // next tuen
            GameManager.Instance.TakeAction(squareID, mySign == (int)CellType.X);
        }

        [ClientRpc]
        public void RPCUpdateTurn()
        {
            SetTurn(isMyTurn, (CellType)mySign);
        }

        private void SetTurn(bool isMyTurn, CellType turn)
        {
            if (TurnTxt && isMyTurn)
            {
                TurnTxt.text = $"my turn ({turn})";
            }
            else if (TurnTxt)
            {
                TurnTxt.text = string.Empty;
            }
        }

        [ClientRpc]
        public void UpdateUIText(string txt)
        {
            if (TurnTxt)
            {
                TurnTxt.text = txt;
            }
        }
    }
}