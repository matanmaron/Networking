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
            var render = GetComponent<SpriteRenderer>();
            render.sprite = sprites[Random.Range(0, sprites.Count)];
        }
    }
}