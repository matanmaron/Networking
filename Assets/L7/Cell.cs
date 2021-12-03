using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L7
{
    public class Cell : NetworkBehaviour
    {
        [SerializeField] GameObject X;
        [SerializeField] GameObject O;
        CellType _cellValue = CellType.None;
        public CellType CellValue => _cellValue;
        public void SetCell(CellType value)
        {
            _cellValue = value;
            switch (_cellValue)
            {
                case CellType.None:
                    Destroy(GetComponentInChildren<Transform>().gameObject);
                    break;
                case CellType.X:
                    Instantiate(X, transform);
                    break;
                case CellType.O:
                    Instantiate(O, transform);
                    break;
                default:
                    break;
            }
        }

        public void Clean()
        {
            SetCell(CellType.None);
        }
    }
}