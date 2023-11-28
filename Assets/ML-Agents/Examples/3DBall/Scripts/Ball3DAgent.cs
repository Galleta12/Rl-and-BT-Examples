using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class Ball3DAgent : Agent
{
    // [Header("Specific to Ball3D")]
    // public GameObject ball;
    // [Tooltip("Whether to use vector observation. This option should be checked " +
    //     "in 3DBall scene, and unchecked in Visual3DBall scene. ")]
    // public bool useVecObs;
    // Rigidbody m_BallRb;
    // EnvironmentParameters m_ResetParams;

    // public override void Initialize()
    // {
    //     m_BallRb = ball.GetComponent<Rigidbody>();
    //     m_ResetParams = Academy.Instance.EnvironmentParameters;
    //     SetResetParameters();
    // }

    // public override void CollectObservations(VectorSensor sensor)
    // {
    //     if (useVecObs)
    //     {
    //         sensor.AddObservation(gameObject.transform.rotation.z);
    //         sensor.AddObservation(gameObject.transform.rotation.x);
    //         sensor.AddObservation(ball.transform.position - gameObject.transform.position);
    //         sensor.AddObservation(m_BallRb.velocity);
    //     }
    // }

    // public override void OnActionReceived(ActionBuffers actionBuffers)
    // {
    //     var actionZ = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
    //     var actionX = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
           
    //     if ((gameObject.transform.rotation.z < 0.25f && actionZ > 0f) ||
    //         (gameObject.transform.rotation.z > -0.25f && actionZ < 0f))
    //     {
    //         gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
    //     }

    //     if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) ||
    //         (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
    //     {
    //         gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
    //     }
    //     if ((ball.transform.position.y - gameObject.transform.position.y) < -2f ||
    //         Mathf.Abs(ball.transform.position.x - gameObject.transform.position.x) > 3f ||
    //         Mathf.Abs(ball.transform.position.z - gameObject.transform.position.z) > 3f)
    //     {
    //         SetReward(-1f);
    //         EndEpisode();
    //     }
    //     else
    //     {
    //         SetReward(0.1f);
    //     }
    // }

    // public override void OnEpisodeBegin()
    // {
    //     gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    //     gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
    //     gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
    //     m_BallRb.velocity = new Vector3(0f, 0f, 0f);
    //     ball.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 4f, Random.Range(-1.5f, 1.5f))
    //         + gameObject.transform.position;
    //     //Reset the parameters when the Agent is reset.
    //     SetResetParameters();
    // }

    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     var continuousActionsOut = actionsOut.ContinuousActions;
    //     continuousActionsOut[0] = -Input.GetAxis("Horizontal");
    //     continuousActionsOut[1] = Input.GetAxis("Vertical");
    // }

    // public void SetBall()
    // {
    //     //Set the attributes of the ball by fetching the information from the academy
    //     m_BallRb.mass = m_ResetParams.GetWithDefault("mass", 1.0f);
    //     var scale = m_ResetParams.GetWithDefault("scale", 1.0f);
    //     ball.transform.localScale = new Vector3(scale, scale, scale);
    // }

    // public void SetResetParameters()
    // {
    //     SetBall();
    // }


     [Header("Specific to Ball3D")]
    public GameObject ball;
    [Tooltip("Whether to use vector observation. This option should be checked " +
        "in 3DBall scene, and unchecked in Visual3DBall scene. ")]
    public bool useVecObs;
    Rigidbody m_BallRb;
    EnvironmentParameters m_ResetParams;
    
    
    public BehaviourTree tree;

    

   

    public override void Initialize()
    {
        m_BallRb = ball.GetComponent<Rigidbody>();
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        
        //save the important things on the tree parameter.
        SetResetParameters();
    }

    public virtual void Start()
    {
        
      
        
        //clonnign to avoid any potential error when add more objects with the same tree
        tree = tree.Clone();
        //bind the important data

        tree.blackBoardRL.setBallRigidBody(m_BallRb);
        tree.blackBoardRL.setBallObject(ball);
        tree.blackBoardRL.setAgentObject(gameObject);
        
        tree.blackBoardRL.PositiveReward += SetPositiveReward;
        tree.blackBoardRL.NegativeRewardEndEpisode += SetNegativeRewardEndEpisode;
        
        tree.Bind();

    }

   


     public override void OnEpisodeBegin()
    {
        
        
        //Debug.Log("On Episode Begin");
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        m_BallRb.velocity = new Vector3(0f, 0f, 0f);
        ball.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 4f, Random.Range(-1.5f, 1.5f))
            + gameObject.transform.position;
        //Reset the parameters when the Agent is reset.
        SetResetParameters();
    }


    public void SetBall()
    {
        //Set the attributes of the ball by fetching the information from the academy
        m_BallRb.mass = m_ResetParams.GetWithDefault("mass", 1.0f);
        var scale = m_ResetParams.GetWithDefault("scale", 1.0f);
        ball.transform.localScale = new Vector3(scale, scale, scale);

        //set everything again
        
        tree.blackBoardRL.setBallRigidBody(m_BallRb);
        tree.blackBoardRL.setBallObject(ball);
        tree.blackBoardRL.setAgentObject(gameObject);
    }

    public void SetResetParameters()
    {
        SetBall();
    }


    //this is call every second to add observatioons into the on action received
    public override void CollectObservations(VectorSensor sensor)
    {
        if (useVecObs)
        {
            sensor.AddObservation(gameObject.transform.rotation.z);
            sensor.AddObservation(gameObject.transform.rotation.x);
            sensor.AddObservation(ball.transform.position - gameObject.transform.position);
            sensor.AddObservation(m_BallRb.velocity);
        }





    }



    private void SetPositiveReward(){
        SetReward(0.1f);
        
    }
    
    private void SetNegativeRewardEndEpisode(){
        SetReward(-1f);
        EndEpisode();
    }


    //this is my update

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //get output of neural network
        var actionZ = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
        var actionX = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
        //saved on the blackboard
        tree.blackBoardRL.setNeuralOutput(actionZ,actionX);


        //Debug.Log("Loolll");
          
        if(tree){

            tree.Update();
        }
           
    

    
    }

       public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = -Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}
