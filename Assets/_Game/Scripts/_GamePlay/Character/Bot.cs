using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{

    private IState currentState;
    private Transform target;
    private Transform des;
    public NavMeshAgent agent;

    private  Platform platform;
    public int i = 0;
    

    private void Update()
    {
        
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if (!GameManager.Instance.IsState(GameState.GamePlay))  ChangeState(new IdleState());
    }

    
    public bool CheckDistance()
    {
        return Vector3.Distance(transform.position, target.position) < 1.2f;
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new PatrolState());

    }
    public void SetDes(Transform destination)
    {
        this.des = destination;
    }
    public void FindBrick()
    {
        if (platform.FindNeareastBrick(transform, color) != null)
        {
            target = platform.FindNeareastBrick(transform, color).transform;
            agent.SetDestination(target.position);
        }
        else
        {
            if (BrickCount() > 0) this.ChangeState(new BuildBridgeState());
            else this.ChangeState(new IdleState());
        }
            
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void BuildBridge()
    {
        agent.SetDestination(des.position);
    }
    public void SetPlatform(Platform nextPlatform)
    {
        this.platform = nextPlatform;
    }
    public override void Falling()
    {
        base.Falling();
        this.ChangeState(new FallingState());

    }
  
    /*--------------------------------------- 
        CHECK COLLISION OF PLAYER AND BOT 
     * ---------------------------------------*/
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(Const.PLAYER_TAG))
        {
            Player crt = other.GetComponent<Player>();
            if (this.BrickCount() > crt.BrickCount())
            {
                crt.Falling();
            }
            else
            {
                this.Falling();
            }
        }
        if (other.CompareTag(Const.BOT_TAG))
        { 
            Bot crt = other.GetComponent<Bot>();
            if (this.BrickCount() > crt.BrickCount())
            {
                crt.Falling();
            }
        }
    }
}
