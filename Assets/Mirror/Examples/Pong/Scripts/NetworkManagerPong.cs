using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.Pong
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerPong : NetworkManager
    {
        public Transform leftRacketSpawn;
        public Transform leftRacketSpawn2;
        public Transform rightRacketSpawn;
        public Transform rightRacketSpawn2;
        List<GameObject> balls = new List<GameObject>();

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Transform start;
            // add player at correct spawn position
            if (numPlayers > 1)
            {
                start = numPlayers == 2 ? leftRacketSpawn2 : rightRacketSpawn2;
            }
            else
            {
                start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;

            }
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            // spawn ball if two players
            if (numPlayers == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    var ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
                    NetworkServer.Spawn(ball);
                    balls.Add(ball);
                }
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // destroy ball
            if (balls != null)
                foreach (var ball in balls)
                {
                    NetworkServer.Destroy(ball);
                }
            balls.Clear();
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
