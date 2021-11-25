using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace L6
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

        Cell[] _board;

        private bool wasInit = false;
        private uint playerIDTurn = 0;
        bool _gameOver = false;

        public uint PlayerIDTurn => playerIDTurn;

        public bool IsMyTurn(uint playerNetID)
        {
            if (!wasInit)
            {
                playerIDTurn = playerNetID;
                wasInit = true;
            }
            return playerIDTurn == playerNetID;
        }

        private void Start()
        {
            _board = FindObjectsOfType<Cell>().OrderBy(x => x.name).ToArray();
            foreach (var cell in _board)
            {
                cell.Clean();
            }
        }

        public void TakeAction(int squareID, bool isX)
        {
            Debug.Log($"take action on {squareID}");
            if (_gameOver)
            {
                Debug.Log("GAME OVER...");
                return;
            }
            if (_board[squareID-1].CellValue != CellType.None)
            {
                Debug.Log("TAKEN!!");
                return;
            }
            _board[squareID-1].CellValue = isX ? CellType.X : CellType.O;
            _board[squareID-1].SetText(_board[squareID-1].CellValue.ToString());
            foreach (var p in FindObjectsOfType<Player>())
            {
                p.RPCUpdateCell(squareID);
            }
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
                p.RPCUpdateTurn(playerIDTurn == p.connectionToClient.identity.netId);
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
            Debug.Log($"{status} wins, game over");
        }

        private CellType WhosWinning()
        {
            //row
            for (int i = 0; i < _board.Length; i += 3)
            {
                if (_board[i].CellValue == _board[i + 1].CellValue && _board[i + 1].CellValue == _board[i + 2].CellValue && _board[i].CellValue != CellType.None)
                {
                    return _board[i].CellValue;
                }
            }
            //column
            for (int i = 0; i < 3; i++)
            {
                if (_board[i].CellValue == _board[i + 3].CellValue && _board[i + 3].CellValue == _board[i + 6].CellValue && _board[i].CellValue != CellType.None)
                {
                    return _board[i].CellValue;
                }
            }
            //diagonal
            if (_board[0].CellValue == _board[4].CellValue && _board[4].CellValue == _board[8].CellValue && _board[0].CellValue != CellType.None)
            {
                return _board[0].CellValue;
            }
            if (_board[2].CellValue == _board[4].CellValue && _board[4].CellValue == _board[6].CellValue && _board[2].CellValue != CellType.None)
            {
                return _board[2].CellValue;
            }
            return CellType.None;
        }

        bool isFullBoard()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i].CellValue == CellType.None)
                {
                    return false;
                }
            }
            return true;
        }
    }
}