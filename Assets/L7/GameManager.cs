using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L7
{
    public class GameManager : MonoBehaviour
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

        [HideInInspector] public CellType Turn = CellType.None;

        Cell[] _board;
        [SerializeField] GamePanel _gamePanel;
        [SerializeField] UIPanel _uiPanel;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            Turn = (CellType)Random.Range(1, 3);
            _gamePanel.StartGame();
            _uiPanel.StartGame(Turn);
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
            Turn = Turn == CellType.X ? CellType.O : CellType.X;
            _uiPanel.ShowTurn(Turn);
        }

        private void WinGame(CellType status)
        {
            Debug.Log($"{status} wins, game over");
            StartGame();
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