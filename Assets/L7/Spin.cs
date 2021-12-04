using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L7
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] CellType Cell;
        const int multiplier = 30;
        void Update()
        {
            switch (Cell)
            {
                case CellType.X:
                    transform.Rotate(multiplier * Vector3.up * Time.deltaTime);
                    break;
                case CellType.O:
                    transform.Rotate(multiplier * Vector3.forward * Time.deltaTime);
                    break;
                case CellType.None:
                default:
                    break;
            }
        }
    }
}