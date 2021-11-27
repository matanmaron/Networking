using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L7
{
    public class Canvas3D : NetworkBehaviour
    {
        [SerializeField] List<GameObject> cellHolders;
        [SerializeField] Cell cellPrefab;

        [SerializeField] List<Cell> cells;

        public void StartGame()
        {
            foreach (var holder in cellHolders)
            {
                Instantiate(cellPrefab, holder.transform);
            }
        }

        public void UpdateBoard(int squareID, int value)
        {
            cells[squareID].SetCell((CellType)value);
        }
    }
}