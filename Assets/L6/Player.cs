using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            isX = GameManager.Instance.IsMeX();
        }

        private void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(TurnTxt);
            }
            SetTurn(isMyTurn);
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
            GameManager.Instance.TakeAction(squareID, isX);
        }

        [ClientRpc]
        public void RPCUpdateTurn(bool turn)
        {
            SetTurn(turn);
        }

        private void SetTurn(bool turn)
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

        [ClientRpc]
        public void RPCUpdateCell(int num, string text)
        {
            Debug.Log($"update cell {num}");
            var cell = FindObjectsOfType<Cell>().First(x => x.name == num.ToString());
            cell.UpdateText(text);
        }
    }
}