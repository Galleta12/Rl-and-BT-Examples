using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents;
public class EvaluateRewardsSimpleAction : ActionNode
{
    
    public GameObject agentObject;
    public GameObject ballObject;
    protected override void OnStart()
    {
        
        
        ballObject = blackBoardRL.getBallObject();
        agentObject = blackBoardRL.getAgentObject();
    }

        //the dubug log node can not fail
    protected override State OnUpdate()
    {
            
            
        if ((ballObject.transform.position.y - agentObject.transform.position.y) < -2f ||
            Mathf.Abs(ballObject.transform.position.x - agentObject.transform.position.x) > 3f ||
            Mathf.Abs(ballObject.transform.position.z - agentObject.transform.position.z) > 3f)
        {
            
            blackBoardRL.OnNegativeRewardEndEpisode();

            return State.Failure;
            
        
        }
            
            blackBoardRL.OnPositiveReward();
            
            return State.Succes;
            
    }
        protected override void OnStop()
        {
            
        }
}
