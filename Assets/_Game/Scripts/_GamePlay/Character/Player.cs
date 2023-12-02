using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private LayerMask BRIDGE;
    [SerializeField] private LayerMask BRIDGE_BRICK;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    private Vector3 direction;
    private Vector3 moveVector;
    private Vector3 slopeNormal;

    private float maxSlopeAngle = 40f;
    private float moveSpeed = 7f;
    private float rotateSpeed = 10f;

    private bool isGrounded;
    private bool isCantMove = false;



    void Update()
    {

        if (isCantMove || GameManager.Instance.IsState(GameState.Finish)) return;
        //Debug.DrawLine(transform.position,transform.position + transform.forward,Color.red);
        isGrounded = CheckGround();

        if (Input.GetMouseButton(0))
        {
            Move();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Stop();
        }
    }
    public override void OnInit()
    {
        base.OnInit();

    }
    public void Move()
    {
        ChangeAnim(Const.ANIM_RUN);
        moveVector.x = variableJoystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = variableJoystick.Vertical * moveSpeed * Time.deltaTime;
        direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);

        if (direction != Vector3.zero)
        {
            RaycastHit hit;

            if (isGrounded )
            {
               
                if (!CheckNextBrick()&&BrickCount()==0)
                {
                    moveVector = new Vector3(moveVector.x, 0, 0);
                }
                transform.rotation = Quaternion.LookRotation(direction);
                rb.MovePosition(rb.position + moveVector);
                

            }

            if (Physics.Raycast(transform.position + Vector3.up * 5f, Vector3.down * 5f, out hit, 10f, BRIDGE))
            {

                if (moveVector.z > 0)
                {

                    if (listBricks.Count > 0)
                    {
                        //remove brick and move on

                    }
                    else
                    {
                        if (!CheckNextBrick())
                        {

                            direction = new Vector3(moveVector.x, 0f, 0f);
                        }
                    }

                }
                MoveOnBridge(direction, hit);

            }
            else
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            // Move on the platform

        }


    }
    public bool CheckNextBrick()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position,new Vector3(0, 0, 1), out hit, 1f, BRIDGE_BRICK))
        {
            return hit.collider.GetComponent<Brick>().CheckColor(color);
        }
        else return true;


    }

    private void MoveOnBridge(Vector3 direction, RaycastHit hit)
    {
        slopeNormal = hit.normal;
        Vector3 moveDirection = Vector3.zero;
        float slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);

        if (slopeAngle <= maxSlopeAngle)
        {
            moveDirection = Vector3.ProjectOnPlane(direction, slopeNormal).normalized;


            // Áp dụng quay đối tượng dựa trên hướng di chuyển và bề mặt dốc
            //rb.MoveRotation(Quaternion.LookRotation(moveDirection, slopeNormal));
        }

        if (moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveDirection);
        else transform.rotation = Quaternion.LookRotation(direction);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }
    private void Stop()
    {
        ChangeAnim(Const.ANIM_IDLE);
    }
    public override void Falling()
    {
        base.Falling();
        isCantMove = true;
        StartCoroutine(ResetFalling());
    }
    private IEnumerator ResetFalling()
    {
        yield return new WaitForSeconds(2f);
        ChangeAnim(Const.ANIM_IDLE);
        isCantMove = false;
    }

    public void OnVictory()
    {
        ClearBrick();
        GameManager.Instance.ChangeState(GameState.Finish);
        transform.position = LevelManager.Instance.currentLevel.finishBox.position;
        StartCoroutine(OnFinishGame());

    }
    private IEnumerator OnFinishGame()
    {
        ChangeAnim(Const.ANIM_DANCE);
        yield return new WaitForSeconds(4f);
        LevelManager.Instance.OnFinish();


    }

}
