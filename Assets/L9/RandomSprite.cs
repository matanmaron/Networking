using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L9
{
    public class RandomSprite : NetworkBehaviour
    {
        [SerializeField] List<Sprite> sprites;
        void Start()
        {
            RPCChangeSprite(Random.Range(0, sprites.Count));
        }

        [ClientRpc]
        private void RPCChangeSprite(int index)
        {
            var render = GetComponent<SpriteRenderer>();
            render.sprite = sprites[index];
        }
    }
}