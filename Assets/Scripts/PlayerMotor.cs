
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
    // y�n ve hareket
    public void Move(Vector3 _velocity)
    {

        velocity = _velocity;

    }
    // mouse sa� sol __ karakter sa� sol  d�nme hereketi
    public void Rotate(Vector3 _rotation)
    {

        rotation = _rotation;
        

    }


    // mouse yukar� a�a�� __burda sadece kameray� yukar� a�a�� d�nme yapca��z.
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
