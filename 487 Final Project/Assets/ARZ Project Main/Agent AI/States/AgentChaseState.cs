using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentChaseState : IAgentState {
	private readonly StatePatternAgent agent;

	public AgentChaseState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Chase ();
	}

	public void OnTriggerEnter (Collider other)
	{

	}

	public void ToAgentCuteState()
	{
		// current state
	}
	public void ToAgentPatrolState()
	{
		agent.currentState = agent.agentPatrolState;
	}
	public void ToAgentCircleState()
	{
		agent.currentState = agent.agentCircleState;
	}
	public void ToAgentChaseState()
	{
		Debug.Log ("Can't transition to same state");
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
			agent.chaseTarget = hit.transform;
		}
		else
		{
			ToAgentAlertState ();
		}

	}

	private void Chase()
	{
		agent.meshRendererFlag.material.color = Color.red;
		agent.navMeshAgent.destination = agent.chaseTarget.position;
		agent.navMeshAgent.stoppingDistance = 2f;

		if (agent.navMeshAgent.remainingDistance <= (agent.navMeshAgent.stoppingDistance + 1.3f)) {
			// are we as close as we can be at this point?
			agent.agentController.SetAgentMove ("idle1");
			ToAgentCircleState ();
		} else {
			agent.agentController.SetAgentMove ("run");
		}
		agent.navMeshAgent.Resume ();
	}


}