using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionGoForward2 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        if(globalBlackBoard.dirToGoForwardAction==2){
            globalBlackBoard.dirToGo =  (globalBlackBoard.largeGrounded ? 1f : 0.5f) * -1f * globalBlackBoard.agentObject.transform.forward;
            return State.Succes;
        }        

        return State.Failure;

    }
    protected override void OnStop()
    {
    
    }

}