using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.ANIM_IDLE);
        if(t!=null)
        t.agent.isStopped = true;
    }

    public void OnExecute(Bot t)
    {

    }

    public void OnExit(Bot t)
    {

    }

}
