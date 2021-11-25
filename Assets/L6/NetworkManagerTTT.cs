using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L6
{
    public class NetworkManagerTTT : NetworkManager
    {
        [SerializeField] GameObject GameManager;
        public override void OnStartServer()
        {
            base.OnStartServer();
            if (FindObjectOfType<GameManager>() == null)
            {
                Instantiate(GameManager);
            }
        }
    }
}