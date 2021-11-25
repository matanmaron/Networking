using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class GamePanel : MonoBehaviour
    {
        List<Cell> _cells = new List<Cell>();

        public void Init()
        {
            GameManager.Instance.SetCells(_cells);
            foreach (var cell in _cells)
            {
                cell.Clean();
            }
        }
    }
}