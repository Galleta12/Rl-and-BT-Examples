using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionAgentJump : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        if((globalBlackBoard.jumpingTime <=0f) && globalBlackBoard.smallGrounded){
            Jump();
            return State.Succes;
        }
        return State.Failure;

    }
    protected override void OnStop()
    {
    
    }


    private void Jump()
    {
        globalBlackBoard.jumpingTime = 0.2f;
        globalBlackBoard.m_JumpStartingPos = globalBlackBoard.m_AgentRb.position;
    }

}