using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionEvaluateJumpTime1 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
       
        if (globalBlackBoard.jumpingTime > 0f)
        {
            globalBlackBoard.m_JumpTargetPos =
                new Vector3(globalBlackBoard.m_AgentRb.position.x,
                    globalBlackBoard.m_JumpStartingPos.y + globalBlackBoard.wallJumpSettings.agentJumpHeight,
                    globalBlackBoard.m_AgentRb.position.z) + globalBlackBoard.dirToGo;
            MoveTowards(globalBlackBoard.m_JumpTargetPos, globalBlackBoard.m_AgentRb, globalBlackBoard.wallJumpSettings.agentJumpVelocity,
                globalBlackBoard.wallJumpSettings.agentJumpVelocityMaxChange);
        }
        
        
        return State.Succes;
     

    }
    protected override void OnStop()
    {
    
    }


    private  void MoveTowards(
        Vector3 targetPos, Rigidbody rb, float targetVel, float maxVel)
    {
        var moveToPos = targetPos - rb.worldCenterOfMass;
        var velocityTarget = Time.fixedDeltaTime * targetVel * moveToPos;
        if (float.IsNaN(velocityTarget.x) == false)
        {
            rb.velocity = Vector3.MoveTowards(
                rb.velocity, velocityTarget, maxVel);
        }
    }


}