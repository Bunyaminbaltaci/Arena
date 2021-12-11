
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour    
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 Camerarotation = Vector3.zero;

    private Rigidbody rb;
     void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    // yön ve hareket
    public void Move(Vector3 _velocity)
    {

        velocity = _velocity;

    }
    // mouse sað sol __ karakter sað sol  dönme hereketi
    public void Rotate(Vector3 _rotation)
    {

        rotation = _rotation;
        

    }


    // mouse yukarý aþaðý __burda sadece kamerayý yukarý aþaðý dönme yapcaðýz.
    public void RotateCamera(Vector3 _camerarotation)
    {

        Camerarotation = _camerarotation;
        

    }
     void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

    }
    void PerformMovement()
    {
        if (velocity !=Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity*Time.fixedDeltaTime);
        }
    }
    void PerformRotation()
    {
      
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));
        if (cam !=null)
        {
            cam.transform.Rotate(-Camerarotation);
        }
        
    }
}
