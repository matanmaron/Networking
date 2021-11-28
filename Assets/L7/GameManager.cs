using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace L7
{
    public class GameManager : NetworkBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        int[] _board = new int[9];
        Canvas3D _canvas3D;
        private uint playerIDTurn = 0;
        bool _gameOver = false;
        public uint PlayerIDTurn => playerIDTurn;
        int players = 0;
        
        public int GetSign()
        {
            if (players>2)
            {
                return 0;
            }
            players++;
            return players;
        }

        public bool IsMyTurn(uint id)
        {
            return PlayerIDTurn == id;
        }

        private void Start()
        {
            Debug.Log("start");
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i] = (int)CellType.None;
            }
            _canvas3D = FindObjectOfType<Canvas3D>();
        }

        public void TakeAction(int squareID, bool isX)
        {
            Debug.Log($"take action on {squareID}, x-{isX}");
            if (FindObjectsOfType<Player>().Length != 2)
            {
                foreach (var p in FindObjectsOfType<Player>())
                {
                    p.UpdateUIText("waiting for more players...");
                }
                Debug.Log("waiting for more players...");
                return;
            }
            if (_gameOver)
            {
                Debug.Log("GAME OVER...");
                foreach (var p in FindObjectsOfType<Player>())
                {
                    p.UpdateUIText("GAME OVER...");
                }
                return;
            }
            if (_board[squareID - 1] != (int)CellType.None)
            {
                Debug.Log("TAKEN!!");
                return;
            }
            _board[squareID - 1] = isX ? (int)CellType.X : (int)CellType.O;
            _canvas3D.UpdateBoard(squareID-1, _board[squareID - 1]);
            AfterTurnChecks();
        }

        public void NextTurn()
        {
            Debug.Log("NextTurn()");
            Player[] players = FindObjectsOfType<Player>();
            if (players.Length != 2)
            {
                throw new System.InvalidProgramException($"Number of players is illegal - {players.Length}");
            }
            //find next player turn
            if (playerIDTurn == players[0].connectionToClient.identity.netId)
            {
                playerIDTurn = players[1].connectionToClient.identity.netId;
            }
            else
            {
                playerIDTurn = players[0].connectionToClient.identity.netId;
            }
            //update all players
            foreach (var p in players)
            {
                p.RPCUpdateTurn();
            }
        }

        public void AfterTurnChecks()
        {
            if (isFullBoard())
            {
                Debug.Log("board full, game over");
                WinGame(CellType.None);
                return;
            }
            var status = WhosWinning();
            switch (status)
            {
                case CellType.None:
                    break;
                case CellType.X:
                case CellType.O:
                    WinGame(status);
                    return;
                default:
                    break;
            }
            NextTurn();
        }

        private void WinGame(CellType status)
        {
            _gameOver = true;
            foreach (var p in FindObjectsOfType<Player>())
            {
                p.UpdateUIText($"{status} wins, game over");
            }
            Debug.Log($"{status} wins, game over");
        }

        private CellType WhosWinning()
        {
            //row
            for (int i = 0; i < _board.Length; i += 3)
            {
                if (_board[i] == _board[i + 1] && _board[i + 1] == _board[i + 2] && _board[i] != (int)CellType.None)
                {
                    return (CellType)_board[i];
                }
            }
            //column
            for (int i = 0; i < 3; i++)
            {
                if (_board[i] == _board[i + 3] && _board[i + 3] == _board[i + 6] && _board[i] != (int)CellType.None)
                {
                    return (CellType)_board[i];
                }
            }
            //diagonal
            if (_board[0] == _board[4] && _board[4] == _board[8] && _board[0] != (int)CellType.None)
            {
                return (CellType)_board[0];
            }
            if (_board[2] == _board[4] && _board[4] == _board[6] && _board[2] != (int)CellType.None)
            {
                return (CellType)_board[2];
            }
            return CellType.None;
        }

        bool isFullBoard()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] == (int)CellType.None)
                {
                    return false;
                }
            }
            return true;
        }
    }
}