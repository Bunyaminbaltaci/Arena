using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    private PlayerMotor motor;

    [SerializeField]
    private float lookSensivity=2f;
     void Start()
    {
        motor = GetComponent<PlayerMotor>();

    }
     void Update()
    {
        // karakter WASD hareketleri tuþ deðiþikliðini input systemden yapabilirsin.
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        Vector3 _movHorizontal = transform.right*_xMov;
        Vector3 _movVertical = transform.forward*_zMov;
         
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * Speed;
        motor.Move(_velocity);
        // mouse sað-sol dönme hareketleri .
        float _xRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f,_xRot,0f)*lookSensivity;
        //Mouse yukarý-aþaðý dönme hareketi.  
        float _yRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_yRot,0f, 0f)*lookSensivity;
        motor.Rotate(_rotation);
        motor.RotateCamera(_cameraRotation);


    }
}
