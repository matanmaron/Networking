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
        [SerializeField] Text mySignTxt;
        [SerializeField] Text nowPlayTxt;
        private Canvas3D _canvas3D;

        public Canvas3D Canvas3d { get
            {
                if (_canvas3D == null)
                {
                    _canvas3D = FindObjectOfType<Canvas3D>();
                }
                return _canvas3D;
            }
        }
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
            TestMySign();
            TestNowPlay();
        }

        private void TestNowPlay()
        {
            if (!isLocalPlayer)
            {
                Destroy(nowPlayTxt);
                return;
            }
            nowPlayTxt.text = $"now: {(CellType)GameManager.Instance.Turn}";
        }

        private void TestMySign()
        {
            if (!isLocalPlayer)
            {
                Destroy(mySignTxt);
                return;
            }
            mySignTxt.text = $"you are {(CellType)mySign}";
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
            if (mySignTxt)
            {
                mySignTxt.text = $"you are {(CellType)mySign}";
            }
            //check valid
            //take action
            // next tuen
            GameManager.Instance.TakeAction(squareID, mySign == (int)CellType.X);
        }

        [ClientRpc]
        public void UpdateUIText(string txt)
        {
            if (mySignTxt)
            {
                mySignTxt.text = txt;
            }
        }

        [ClientRpc]
        public void UpdateTurnText(string txt)
        {
            if (nowPlayTxt)
            {
                nowPlayTxt.text = $"now: {txt}";
            }
        }
    }
}