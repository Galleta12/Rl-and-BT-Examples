using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBallRotate1 : ActionNode
{
    public float actionZ;
    public GameObject agentObject;
    protected override void OnStart()
    {
     
        actionZ = blackBoardRL.floatActionZ;
        agentObject = blackBoardRL.getAgentObject();
    }

        //this action cannot fail
    protected override State OnUpdate()
    {

        if ((agentObject.transform.rotation.z < 0.25f && actionZ > 0f) ||
            (agentObject.transform.rotation.z > -0.25f && actionZ < 0f))
        {
            agentObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }
        
        return State.Succes;
            
    }
    protected override void OnStop()
    {
            
    }
}
