using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class GameManager : NetworkBehaviour
    {
        #region singleton
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (isServer)
            {
                if (_instance != null && _instance != this)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    _instance = this;
                }
            }
        }
        #endregion

        [Serializable] UIManager _UIManager;
        private bool wasInit = false;
        private uint _playerIdTurn = 0;
        public uint PlayerIdTurn => _playerIdTurn;

        public bool IsMyTurn(uint playerNetId)
        {
            if (!wasInit)
            {
                _playerIdTurn = playerNetId;
                wasInit = true;
            }
            return _playerIdTurn == playerNetId;
        }

        public void NextTurn()
        {
            Player[] players = GameObject.FindObjectsOfType<Player>();
            if (IsMyTurn(players[0].connectionToClient.identity.netId))
            {
                _playerIdTurn = players[1].connectionToClient.identity.netId;
            }
            else
            {
                _playerIdTurn = players[0].connectionToClient.identity.netId;
            }
        }

        [Command]
        public void AskToMove(int squreID)
        {
            Debug.Log($"move is {squreID}");
            if (IsValidMove(squreID))
            {

            }
            else
            {
                Debug.Log("not a valid move!");
            }
        }

        private bool IsValidMove(int squreID)
        {
            return false;
        }
    }
}