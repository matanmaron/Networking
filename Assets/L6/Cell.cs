using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L6
{
    public class Cell : NetworkBehaviour
    {
        [SerializeField] Text _cellText;
        [HideInInspector] public CellType CellValue = CellType.None;

        public void UpdateText(string text)
        {
            _cellText.fontSize = 88;
            Debug.Log($"cell value - {text}");
            _cellText.text = text;
        }
    }
}