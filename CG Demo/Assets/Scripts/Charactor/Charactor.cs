using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public float disToFloor;
    public float AimDirection { get; set; }
    public bool OnGround
    {
        get => Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, disToFloor);
    }

    public void Move(Vector2 speed)
    {
        if (speed.magnitude > 0)
        {
            animator.SetBool("Move", true);
            animator.SetFloat("SpeedX", speed.x);
            animator.SetFloat("SpeedY", speed.y);
        }
        else
        {
            animator.SetBool("Move", false);
        }
        currentSpeed = new Vector3(speed.x, 0, speed.y) * moveSpeed;
    }

    public void Jump()
    {
        if (OnGround && !IsInvoking("AddJumpForce"))
        {
            Invoke("AddJumpForce", 0.2f);
            animator.SetTrigger("Jump");
        }
    }

    public void Fire(int mode)
    {
        animator.SetInteger("Atk", mode);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aim = transform.rotation.eulerAngles;
        aim.y = AimDirection;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, Quaternion.Euler(aim), rotateSpeed * Time.deltaTime);
        transform.position += Quaternion.Euler(0, AimDirection, 0) * currentSpeed * Time.deltaTime;
    }

    private void AddJumpForce()
    {
        rigidbody.AddForce(transform.up * jumpForce);
    }

    private new Rigidbody rigidbody;
    private Vector3 currentSpeed;
    private Animator animator;
}
