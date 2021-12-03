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

        List<Cell> cells = new List<Cell>();

        public void StartGame()
        {
            foreach (var holder in cellHolders)
            {
                var cell = Instantiate(cellPrefab, holder.transform);
                cells.Add(cell);
            }
        }

        [ClientRpc]
        public void RPCUpdateBoard(int squareID, int value)
        {
            if (cells.Count == 0)
            {
                StartGame();
            }
            cells[squareID].SetCell((CellType)value);
        }
    }
}