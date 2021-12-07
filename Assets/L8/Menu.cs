using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace L8
{
    public class Menu : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("L8GameList");
        }
    }
}