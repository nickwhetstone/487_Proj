using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentApproachTargetState : IAgentState {
	private readonly StatePatternAgent agent;
	public bool crawlBack = false;


	public AgentApproachTargetState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		if (!crawlBack) {
			// inch to player
			ApproachTarget();
		} else {


		}
	}
	public void ApproachTarget() {
		if (agent.chaseTarget != null) {
			Transform target = agent.chaseTarget;
			NavMeshAgent navAgent = agent.navMeshAgent;
			AgentController agentController = agent.agentController;

			// make the target the agents destination
			navAgent.destination = target.position;

			// toggle our actual speed and our animation speed based on the distance to target

			// get speeds
			float distance = navAgent.remainingDistance;
			float agentSpeed = 0;
			float animSpeed = 0;
			float closestPoint = 6f;
			float maxDistance = closestPoint + distance + 4f;

			if (distance > closestPoint) {
				agentSpeed = Mathf.Clamp(distance / 5,0f,3f);
				// Debug.Log(2.Remap(1, 3, 0, 10));    // 5

				animSpeed = distance.Remap(closestPoint,maxDistance, 0.1F, 3F);
			} else {
				 agentSpeed = 0;
				 animSpeed = 0;
			}

			// assign them
			navAgent.speed = agentSpeed;
			agentController.SetAgentAnimationSpeed (animSpeed);
			agentController.SetAgentMove ("walk");

		} else {
			// what the heck do we crawl towards?
		}
	}
	public void OnTriggerEnter (Collider other)
	{
		// are we in contact with anyone?
	}
}
public static class ExtensionMethods {

	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

}