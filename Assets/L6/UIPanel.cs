using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class UIPanel : MonoBehaviour
    {
        public void ShowTurn(bool isX)
        {
            var turn = isX ? CellType.X : CellType.O;
            Debug.Log($"Turn - {turn}");
        }
    }
}