using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    private float fallingTime;
    public void OnEnter(Bot t)
    {
        t.agent.isStopped = true;
    }

    public void OnExecute(Bot t)
    {
        fallingTime += Time.deltaTime;
        if (fallingTime > 2f) {
            t.agent.isStopped = false;
            t.ChangeState(new PatrolState()); 
        }
    }

    public void OnExit(Bot t)
    {

    }
}
