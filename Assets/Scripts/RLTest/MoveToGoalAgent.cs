using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class MoveToGoalAgent : Agent
{

    [SerializeField] private Transform target;



    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
       
    
    }



    public override void Heuristic(in ActionBuffers actionsOut)
    {
       ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
       continuousActions[0] = Input.GetAxisRaw("Horizontal");
       continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //base.OnActionReceived(actions);
        //Debug.Log(actions.ContinuousActions[0]);

        float moveX = actions.ContinuousActions[0];
        float movez = actions.ContinuousActions[1];


        float moveSpeed = 1f;

        transform.position += new Vector3(moveX,0,movez) * Time.deltaTime * moveSpeed;

    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "wall"){
            Debug.Log("Helloda");
            SetReward(-1f);
            EndEpisode();

        }
        else if(other.gameObject.tag == "target"){
            Debug.Log("reward");
            SetReward(+1f);
            EndEpisode();
        }
        
    }

}
