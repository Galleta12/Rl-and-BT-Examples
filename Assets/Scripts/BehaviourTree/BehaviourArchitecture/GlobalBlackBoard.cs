using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GlobalBlackBoard{



    public event Action SetRewardEndEpisode;

    public float fallingForce;
    public GameObject agentObject;
    //rigid body
    public Rigidbody m_AgentRb;
    
    public Rigidbody m_ShortBlockRb;
    //for the setting of the wall jump
    public WallJumpSettings wallJumpSettings;
    
    //for handling the agent moves
    public Vector3 m_JumpTargetPos;
    public Vector3 m_JumpStartingPos;


    public float jumpingTime;
    
    //data regarding rl output
    public bool smallGrounded;
    public bool largeGrounded;
    public Vector3 dirToGo;
    public Vector3 rotateDir;
    public int dirToGoForwardAction;
    public int rotateDirAction ;
    public int dirToGoSideAction;
    public int jumpAction;

    

    public void OnSetRewardEndEpisode() {
        SetRewardEndEpisode?.Invoke();
    }
}