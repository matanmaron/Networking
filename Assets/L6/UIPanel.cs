using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class UIPanel : MonoBehaviour
    {
        public void ShowTurn(CellType turn)
        {
            Debug.Log($"Turn - {turn}");
        }
    }
}