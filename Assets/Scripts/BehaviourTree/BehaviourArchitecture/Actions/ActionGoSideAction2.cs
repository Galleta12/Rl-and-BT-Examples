using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionGoSideAction2 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        if(globalBlackBoard.dirToGoSideAction == 2){
            globalBlackBoard.dirToGo = (globalBlackBoard.largeGrounded ? 1f : 0.5f) * 0.6f * globalBlackBoard.agentObject.transform.right;
            return State.Succes;
        }        

        return State.Failure;

    }
    protected override void OnStop()
    {
    
    }

}