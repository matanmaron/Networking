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
        GamePanel _gamePanel;
        UIPanel _uiPanel;

        private bool wasInit = false;
        private uint playerIDTurn = 0;
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
            _gamePanel = FindObjectOfType<GamePanel>();
            _uiPanel = FindObjectOfType<UIPanel>();
            Init();
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

        private void Init()
        {
            _gamePanel.Init();
        }

        public void SetCells(List<Cell> cells)
        {
            _board = cells.ToArray();
        }

        public void AfterTurnChecks()
        {
            if (isFullBoard())
            {
                Debug.Log("board full, game over");
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
            _uiPanel.ShowTurn(FindObjectsOfType<Player>().First(x => x.isMyTurn).isX);
        }

        private void WinGame(CellType status)
        {
            Debug.Log($"{status} wins, game over");
            Init();
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