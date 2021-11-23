using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] GameObject _cellPrefab;
        List<Cell> _cells = new List<Cell>();

        void Start()
        {
            for (int i = 0; i < 9; i++)
            {
                _cells.Add(Instantiate(_cellPrefab, transform).GetComponent<Cell>());
            }
            GameManager.Instance.SetCells(_cells);
        }

        public void StartGame()
        {
            foreach (var cell in _cells)
            {
                cell.Clean();
            }
        }
    }
}