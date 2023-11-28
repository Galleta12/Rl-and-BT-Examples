using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionEvaluateJumpTime2 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
       
        if (!(globalBlackBoard.jumpingTime > 0f) && !globalBlackBoard.largeGrounded)
        {
            globalBlackBoard.m_AgentRb.AddForce(
                Vector3.down * globalBlackBoard.fallingForce, ForceMode.Acceleration);
        }
        
        return State.Succes;
     

    }
    protected override void OnStop()
    {
    
    }



}