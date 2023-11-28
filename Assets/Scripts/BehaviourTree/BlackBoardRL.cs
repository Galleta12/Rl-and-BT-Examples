using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlackBoardRL 
{
    
   
    Rigidbody ballRigid;
    
    public GameObject ballObject;
    
    public GameObject agentObject;
    
    //vector on action received from mlAgents
    public float floatActionZ;
    public  float floatActionX;


    public event Action PositiveReward;
    public event Action NegativeRewardEndEpisode;


    
    public void setAgentObject(GameObject agent){
        agentObject = agent;
    }
    
    public GameObject getAgentObject(){
        return agentObject;
    }
    
    
    public void setBallRigidBody(Rigidbody rigidbody){
        ballRigid = rigidbody;
    }
    
    public void setBallObject(GameObject bObject){
        ballObject = bObject;
    }
    
    public GameObject getBallObject(){
        return ballObject;
    }
    
    public Rigidbody getBallRigidBody(){
        return ballRigid;
    }


    public void setNeuralOutput(float z, float x){
            floatActionZ=z;
            floatActionX=x;
    }


    public void OnPositiveReward() {
        PositiveReward?.Invoke();
    }

    public void OnNegativeRewardEndEpisode() {
        NegativeRewardEndEpisode?.Invoke();
    }


}
