using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L6
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] Text _cellText;
        [HideInInspector] public CellType CellValue = CellType.None;

        public void Clean()
        {
            CellValue = CellType.None;
            _cellText.text = string.Empty;
        }
    }
}