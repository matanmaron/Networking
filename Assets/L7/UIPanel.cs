using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L7
{
    public class UIPanel : MonoBehaviour
    {
        public void StartGame(CellType turn)
        {
            ShowTurn(turn);
        }

        public void ShowTurn(CellType turn)
        {
            Debug.Log($"Turn - {turn}");
        }
    }
}