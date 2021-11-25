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
        [SyncVar] string text;

        public void Clean()
        {
            CellValue = CellType.None;
            _cellText.text = string.Empty;
            text = string.Empty;
        }

        public void SetText(string txt)
        {
            text = txt;
            UpdateText();
        }

        public void UpdateText()
        {
            _cellText.text = text;
        }
    }
}