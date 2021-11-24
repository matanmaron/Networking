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

        private void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (CellValue!= CellType.None)
            {
                Debug.Log("wrong move!");
                return;
            }
            CellValue = GameManager.Instance.Turn;
            _cellText.text = CellValue.ToString();
            GameManager.Instance.AfterTurnChecks();
        }

        public void Clean()
        {
            CellValue = CellType.None;
            _cellText.text = string.Empty;
        }
    }
}