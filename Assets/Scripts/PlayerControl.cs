using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerControl : NetworkBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        ReversWalk,

    }
    [SerializeField]
    private float speed=3.5f;
    [SerializeField]
    private float rotationSpeed = 1.5f;
    [SerializeField]
    private Vector2 defaultInitialPlanePosition = new Vector2(-4,4);


    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();


    [SerializeField]
    private NetworkVariable<PlayerState> networkplayerState = new NetworkVariable<PlayerState>();


    private CharacterController characterController;


    private Animator animator;


    private Vector3 OldInputPosition=Vector3.zero;
    private Vector3 OldInputRotation=Vector3.zero;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (IsClient && IsOwner)
        {

            transform.position = new Vector3(Random.Range(defaultInitialPlanePosition.x, defaultInitialPlanePosition.y), 0,
                Random.Range(defaultInitialPlanePosition.x, defaultInitialPlanePosition.y));
        }
    }
    private void Update()
    {
        if (IsClient && IsOwner)
        {
            ClientInput();
        }
       ClientMoveAndRotate();
       ClientVisual();

    }
    private void ClientInput()
    {
        //player position and rotation input
        Vector3 inputRotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);


        Vector3 direcrtion = transform.TransformDirection(Vector3.forward);
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 inputPosition = direcrtion * forwardInput;


        if (OldInputPosition != inputPosition ||
            OldInputRotation != inputRotation)
        {
          
            OldInputPosition = inputPosition;
            UpdateClientPositionAndRotationServerRpc(inputPosition*speed,inputRotation*rotationSpeed);
        }
        //plater state change
        if (forwardInput>0)
        {
            UpdatePlayerStateServerRpc(PlayerState.Walk);
        }
        else if(forwardInput < 0)
        {
            UpdatePlayerStateServerRpc(PlayerState.ReversWalk);
        }
        else
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle);
        }

    }
    private void ClientVisual()
    {
        if (networkplayerState.Value==PlayerState.Walk)
        {
            animator.SetFloat("Walk",1);
        }
        else if (networkplayerState.Value==PlayerState.ReversWalk)
        {
            animator.SetFloat("Walk",-1);
        }
        else
        {
            animator.SetFloat("Walk", 0);

        }
    }
    private void ClientMoveAndRotate()
    {
        if (networkPositionDirection.Value!= Vector3.zero)
        {
            characterController.SimpleMove(networkPositionDirection.Value);
        }
        if (networkRotationDirection.Value!= Vector3.zero)
        {
            transform.Rotate(networkRotationDirection.Value,Space.World);
        }
    }


    [ServerRpc]
    public void UpdateClientPositionAndRotationServerRpc(Vector3 newPositionDirection,Vector3 newRotationDirection)
    {
        networkPositionDirection.Value = newPositionDirection;
        networkRotationDirection.Value = newRotationDirection;

    }   
    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState newState)
    {

        networkplayerState.Value = newState;
    }

}


