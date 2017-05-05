using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPatrolState : IAgentState 
{
	private readonly StatePatternAgent agent;
	private int nextWayPoint;

	public AgentPatrolState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Patrol ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
		}// ToAlertState ();
	}
	public void ToAgentCuteState()
	{
		// current state
	}
	public void ToAgentCircleState()
	{
	}
	public void ToAgentPatrolState()
	{
		Debug.Log ("Can't transition to same state");
	}
	public void ToAgentChaseState()
	{
		agent.currentState = agent.agentChaseState;
	}
	public void ToAgentAttackState()
	{
	}
	public void ToAgentEatState()
	{
	}
	public void ToAgentAlertState()
	{
	}
	public void ToAgentDeathState()
	{
	}
	public void ToAgentSleepState()
	{
	}
	private void Look()
	{
		// agent.agentController;
		RaycastHit hit;

		if (Physics.Raycast (agent.eyes.transform.position, agent.eyes.transform.forward, out hit, agent.sightRange) && hit.collider.CompareTag ("Agent")) {

			if (agent.agentType == "follower" && hit.collider.transform.GetComponent<StatePatternAgent> ().agentType == "leader") { 
				agent.chaseTarget = hit.transform;
				ToAgentChaseState ();
			}
		}
	}

	void Patrol ()
	{
		agent.meshRendererFlag.material.color = Color.green;
		agent.navMeshAgent.destination = agent.wayPoints [nextWayPoint].position;
		agent.navMeshAgent.Resume ();
		agent.navMeshAgent.speed = 0.1f;
		agent.agentController.SetAgentMove ("walk");

		SphereCollider spCollider = agent.gameObject.GetComponent<SphereCollider> ();
		if(spCollider) {
			agent.navMeshAgent.stoppingDistance = spCollider.radius + 0.2f;
		} else {
			agent.navMeshAgent.stoppingDistance = 2f;
		}
		if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance && !agent.navMeshAgent.pathPending) {
			nextWayPoint =(nextWayPoint + 1) % agent.wayPoints.Length;
		}
	}
}