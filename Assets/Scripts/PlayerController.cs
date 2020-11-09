using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform camTransform;
    //public GameObject gun;
    //public ParticleSystem muzzleFlash;

 
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G)) {
            ClientSend.PlayerThrowItem(camTransform.forward);
        }

    }

    private void FixedUpdate()
    {
        SendInputToServer();


    }

    private void SendInputToServer() {
        bool[] _inputs = new bool[] {

            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.Alpha1),
            Input.GetKey(KeyCode.Alpha2),
            Input.GetKey(KeyCode.Alpha3),
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
