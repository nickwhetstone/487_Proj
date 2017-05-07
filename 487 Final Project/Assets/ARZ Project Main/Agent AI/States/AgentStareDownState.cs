using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentStareDownState : IAgentState 
{
	private readonly StatePatternAgent agent;
	private float stareTimer = 0f;

	public AgentStareDownState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		StareDown ();
	}

	public void OnTriggerEnter (Collider other)
	{
		
	}
	private void StareDown()
	{
		if (agent.chaseTarget != null) {
			Transform target = agent.chaseTarget;
			NavMeshAgent navAgent = agent.navMeshAgent;
			AgentController agentController = agent.agentController;
			float stoppingDistance = agent.stoppingDistance;
			float giveUpDistance = agent.giveUpDistance;
			float maxAgentSpeed = agent.maxAgentSpeed;


			// make the target the agents destination
			navAgent.destination = target.position;


			// get speeds
			float distance = Vector3.Distance(navAgent.transform.position, target.position); //navAgent.remainingDistance;
			float agentSpeed = 0;
			float animSpeed = 0;
			float maxDistance = stoppingDistance + distance + 4f;

			stareTimer += Time.deltaTime;

			if (stareTimer >= agent.stareDuration) {
				stareTimer = 0f;
				agent.ToState ("Attack");
			} else if ( distance > ( stoppingDistance + giveUpDistance ) ) {
				agent.ToState ("ApproachTarget");
			} else {
				// Strafe and look towards player
				agentSpeed = Mathf.Clamp(distance / 5,0f,maxAgentSpeed);

			}

			// assign them
			agentController.SetAgentMove ("idle");
			navAgent.speed = agentSpeed;
			agentController.SetAgentAnimationSpeed (1f);


		} else {
			// what the heck do we crawl towards?
		}




	}
}