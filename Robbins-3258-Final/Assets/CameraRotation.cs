using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float _sensitivity = 5f;
    GameObject _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        
        transform.RotateAround(_player.transform.position, -Vector3.up, rotateHorizontal * _sensitivity);
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * _sensitivity);
    }
}
