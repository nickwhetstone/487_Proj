using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCircleState : IAgentState {
	private readonly StatePatternAgent agent;
	private float circleTimer;

	public AgentCircleState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Circle ();
	}

	public void OnTriggerEnter (Collider other)
	{

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

	}
	public void ToAgentAttackState()
	{
		circleTimer = 0f;
		agent.currentState = agent.agentAttackState;
	}
	public void ToAgentChaseState()
	{
		circleTimer = 0f;
		agent.currentState = agent.agentChaseState;
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
		RaycastHit hit;
		Vector3 enemyToTarget = (agent.chaseTarget.position + agent.offset) - agent.eyes.transform.position;
		if (Physics.Raycast (agent.eyes.transform.position, enemyToTarget, out hit, agent.sightRange) && hit.collider.CompareTag ("Player")) {
			// agent.chaseTarget = hit.transform;
		}
		else
		{
			// ToAlertState();
		}

	}

	private void Circle()
	{
		agent.meshRendererFlag.material.color = Color.blue;
		agent.navMeshAgent.destination = agent.chaseTarget.position;

		// I did not have time to implement this with the agent strafing around the player
		// The strafing animation looks so cool for this agent, its a shame I had to settle
		// for the walk animation around the player
		agent.agentController.SetAgentMove ("walk");
		agent.navMeshAgent.Resume ();

		circleTimer += Time.deltaTime;

		if (circleTimer >= agent.circleDuration) {
			ToAgentAttackState ();
		} else if( agent.navMeshAgent.remainingDistance > (agent.navMeshAgent.stoppingDistance + 2) ) {
			ToAgentChaseState ();
		}
	}

}