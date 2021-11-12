using Mirror;
using UnityEngine;

public class Sphere : NetworkBehaviour
{
    Vector3 _lastPlace;
    private void Start()
    {
        _lastPlace = transform.position;
        if (this.isLocalPlayer && this.hasAuthority)
        {
            Camera.main.transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (this.isLocalPlayer && this.hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _lastPlace = transform.position;
                transform.Translate(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _lastPlace = transform.position;
                transform.Translate(Vector3.back);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _lastPlace = transform.position;
                transform.Translate(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _lastPlace = transform.position;
                transform.Translate(Vector3.right);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            transform.position = _lastPlace;
        }
    }
}
