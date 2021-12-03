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
                var go = Instantiate(gameManager);
                NetworkServer.Spawn(go);
            }
            if (FindObjectOfType<Canvas3D>() == null)
            {
                var go2 = Instantiate(canvas3D);
                NetworkServer.Spawn(go2);
            }
        }
    }
}