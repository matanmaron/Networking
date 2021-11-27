using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L7
{
    public class NetworkManagerTTT : NetworkManager
    {
        [SerializeField] GameObject gameManager;
        [SerializeField] GameObject canvas3D;
        public override void OnStartServer()
        {
            base.OnStartServer();
            if (FindObjectOfType<GameManager>() == null)
            {
                Instantiate(gameManager);
            }
            if (FindObjectOfType<Canvas3D>() == null)
            {
                Instantiate(canvas3D);
            }
        }
    }
}