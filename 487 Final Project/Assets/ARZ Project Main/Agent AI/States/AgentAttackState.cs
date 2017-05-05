using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Attack()
	{
		agent.meshRendererFlag.material.color = Color.white;
		agent.navMeshAgent.destination = agent.chaseTarget.position;
		agent.navMeshAgent.stoppingDistance = 0.7f;

		agent.agentController.SetAgentMove ("attack");

		agent.navMeshAgent.Resume ();

		if( agent.navMeshAgent.remainingDistance > (agent.navMeshAgent.stoppingDistance + 2) ) {
			ToAgentChaseState ();
		}
	}

}