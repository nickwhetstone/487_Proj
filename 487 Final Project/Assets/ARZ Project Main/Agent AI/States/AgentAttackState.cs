﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAttackState : IAgentState {
	private readonly StatePatternAgent agent;
	private float circleTimer;
	public AgentAttackState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Attack ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			if (!agent.agentController.isdead) {
				other.gameObject.SendMessage("ApplyDamage", 5.0F);
			}
		}
	}
	private void Look()
	{
	}

	private void Attack()
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

			if (distance < ( stoppingDistance + giveUpDistance )) {
				agentSpeed = Mathf.Clamp(distance / 10,0f,maxAgentSpeed);
				animSpeed = 3f;
			} else if ( distance > ( stoppingDistance + giveUpDistance ) ) {
				agent.ToState ("ApproachTarget");
			} else {
				
			}
			/*Debug.Log ("distance: " + distance);
			Debug.Log ("agentSpeed: " + agentSpeed);
			Debug.Log ("maxDistance: " + maxDistance);
			Debug.Log ("closestPoint: " + closestPoint);
			Debug.Log ("animSpeed: " + animSpeed);*/

			// assign them
			agentController.SetAgentMove ("attack");
			navAgent.speed = agentSpeed;
			agentController.SetAgentAnimationSpeed (animSpeed);


		} else {
			// what the heck do we crawl towards?
		}




	}

}