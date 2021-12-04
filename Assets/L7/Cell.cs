using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L7
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] GameObject X;
        [SerializeField] GameObject O;
        CellType _cellValue = CellType.None;
        public CellType CellValue => _cellValue;
        public void SetCell(CellType value)
        {
            Debug.Log($"SetCell {value}");
            _cellValue = value;
            switch (_cellValue)
            {
                case CellType.None:
                    Destroy(GetComponentInChildren<Transform>().gameObject);
                    break;
                case CellType.X:
                    var x = Instantiate(X, transform);
                    x.transform.position = new Vector3(0, 15, 0);
                    break;
                case CellType.O:
                    var o = Instantiate(O, transform);
                    o.transform.position = new Vector3(0, 15, 0);
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