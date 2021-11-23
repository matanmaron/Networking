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
        [SyncVar] [HideInInspector] public CellType CellValue = CellType.None;
        [SyncVar] public int cellID;

        private void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (!GameManager.Instance.ActiveGame)
            {
                return;
            }
            if (hasAuthority)
            {
                GameManager.Instance.CmdAskToMove(cellID);
            }
        }

        public void Clean()
        {
            CellValue = CellType.None;
            _cellText.text = string.Empty;
        }

        public void SetText(string cellText)
        {
            _cellText.text = cellText;
        }
    }
}
