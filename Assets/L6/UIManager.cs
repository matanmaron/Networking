using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button[] _buttons;
    // Start is called before the first frame update
    void Start()
    {
        uint i = 0;
        foreach (var btn in _buttons)
        {
            btn.GetComponent<Text>().text = string.Empty;
            btn.onClick.AddListener(delegate { OnButtonClick(i++); });
        }
    }

    public void SetButton(uint squreID, bool isX)
    {

    }

    private void OnButtonClick(uint squreID)
    {

    }
}
