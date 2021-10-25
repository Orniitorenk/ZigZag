using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }

    private Rigidbody rb;
    public float moveSpeed;

    public bool isPlayerDead;


    private enum CurrentDirection
    {
        Up,
        Left
    }

    private CurrentDirection currentDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentDirection = CurrentDirection.Left;
        isPlayerDead = false;
    }

    private void Update()
    {
        if (!isPlayerDead)
        {
            ShootRay();
            if (Input.GetMouseButtonDown(0))
            {
                StopPlayer();
                ChangeDirection();
            }
        }
        else
        {
            return;
        }
    }

    private void ChangeDirection()
    {
        Movement();
        if(currentDirection == CurrentDirection.Up)
        {
            currentDirection = CurrentDirection.Left;
        }
        else if(currentDirection == CurrentDirection.Left)
        {
            currentDirection = CurrentDirection.Up;
        }
    }

    private void Movement()
    {
        if(currentDirection == CurrentDirection.Up)
        {
            rb.AddForce((Vector3.forward).normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if(currentDirection == CurrentDirection.Left)
        {
            rb.AddForce((Vector3.right).normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void StopPlayer()
    {
        rb.velocity = Vector3.zero;
    }

    private void ShootRay()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit))
        {
            Movement();
        }
        else
        {
            StopPlayer();
            isPlayerDead = true;
            VFX.singleton.InstantiatePlayerDeathParticle(transform, 2);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            VFX.singleton.InstantiateParticleEffect(other, 5f);
            Destroy(other.gameObject);
        }
    }
}
