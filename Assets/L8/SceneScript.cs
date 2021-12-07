using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace L8
{
    public class SceneScript : NetworkBehaviour
    {
        public Text canvasStatusText;
        public PlayerScript playerScript;
        public SceneReference sceneReference;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }

        public void ButtonSendMessage()
        {
            if (playerScript != null)
                playerScript.CmdSendPlayerMessage();
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "L8MyScene")
                    NetworkManager.singleton.ServerChangeScene("L8OtherScene");
                else
                    NetworkManager.singleton.ServerChangeScene("L8MyScene");
            }
            else
                Debug.Log("You are not Host.");
        }
    }
}