using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] public PathManager pathManager;

    List<waypoints> thePath;
    waypoints target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isMoving;
    bool isCrouching;
    bool isSprinting;
    bool isEmoting;


    void Start()
    {
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
        isCrouching = false;
        animator.SetBool("isCrouching", isCrouching);
        isSprinting = false;
        animator.SetBool("isSprinting", isSprinting);
        isEmoting = false;
        animator.SetBool("isEmoting", isEmoting);

        thePath = pathManager.Getpath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            isMoving = !isMoving;
            animator.SetBool("isMoving", isMoving);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
            animator.SetBool("isSprinting", isSprinting);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            animator.SetBool("isCrouching", isCrouching);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEmoting = !isEmoting;
            animator.SetBool("isEmoting", isEmoting);
        }

        if (isMoving)
        {
            rotateTowardsTarget();
            moveForward();
        }
        if (isSprinting)
        {
            rotateTowardsTarget();
            moveForward();
        }
        if (isCrouching)
        {
            rotateTowardsTarget();
            moveForward();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        target = pathManager.GetNextTarget();
    }
}
