using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState
{
    public void OnEnter(Bot t)
    {
      if(t.BrickCount() > 0)
        t.BuildBridge();
    }

    public void OnExecute(Bot t)
    {
        if (t.BrickCount() == 0)
        {
            t.ChangeState(new PatrolState());
        }
        else
        {
            t.BuildBridge();
        }
         
        
    }

    public void OnExit(Bot t)
    {
        
    }

}
