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
        [SerializeField] GamePanel _gamePanel;
        [SerializeField] UIPanel _uiPanel;

        private bool wasInit = false;
        private uint _playerIdTurn = 0;
        public uint PlayerIdTurn => _playerIdTurn;
        bool isNowX;

        public bool ActiveGame = false;
        Player[] _players;

        public bool IsMyTurn(uint playerNetId)
        {
            if (ActiveGame)
            {
                return false;
            }
            if (!wasInit)
            {
                _playerIdTurn = playerNetId;
                wasInit = true;
            }
            return _playerIdTurn == playerNetId;
        }

        public void NextTurn()
        {
            int nextPlayer = 0;
            if (IsMyTurn(_players[0].connectionToClient.identity.netId))
            {
                nextPlayer = 1;
            }
            _playerIdTurn = _players[nextPlayer].connectionToClient.identity.netId;
            isNowX = _players[nextPlayer].IsX;
            _uiPanel.ShowTurn(isNowX ? CellType.X : CellType.O);
        }

        [Command]
        public void CmdAskToMove(int cellID)
        {
            if (!ActiveGame)
            {
                return;
            }
            Cell cell = _board.FirstOrDefault(x => x.cellID == cellID);
            if (cell == null)
            {
                Debug.LogError($"cannot find cell {cellID}");
                return;
            }
            if (cell.CellValue != CellType.None)
            {
                Debug.Log("not a valid move!");
                return;
            }
            cell.CellValue = isNowX ? CellType.X : CellType.O;
            cell.SetText(cell.CellValue.ToString());
            AfterTurnChecks();
        }

        public void OnConnect()
        {
            int players = GameObject.FindObjectsOfType<Player>().Length;
            if (!ActiveGame && players == 2)
            {
                Debug.Log("LET'S BEGIN");
                _players = GameObject.FindObjectsOfType<Player>().ToArray();
                StartGame();
            }
            else if (ActiveGame && players < 2)
            {
                EndGame();
                Debug.Log("DISCONNECTED, GAME END");
                return;
            }
            else
            {
                Debug.Log("WELCOME OBSERVER   O^O  ");
            }
        }

        private void EndGame()
        {
            _players = null;
            ActiveGame = false;
            NetworkManager.singleton.StopHost();
        }

        private void StartGame()
        {
            ActiveGame = true;
            int x = Random.Range(0, 2);
            _players[x].IsX = true;
            _gamePanel.StartGame();
            isNowX = _players[x].isMyTurn? true : false;
            _uiPanel.ShowTurn(isNowX ? CellType.X : CellType.O);
        }

        public void SetCells(List<Cell> cells)
        {
            _board = cells.ToArray();
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i].cellID = i;
            }
        }

        public void AfterTurnChecks()
        {
            if (isFullBoard())
            {
                Debug.Log("board full, game over");
                EndGame();
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
            Debug.Log($"{status} wins, game over");
            EndGame();
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