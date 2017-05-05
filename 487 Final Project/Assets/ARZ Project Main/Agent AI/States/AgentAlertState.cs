using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAlertState : IAgentState {
	private readonly StatePatternAgent agent;
	private float searchTimer;

	public AgentAlertState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Search ();
	}

	public void OnTriggerEnter (Collider other)
	{

	}

	public void ToAgentCuteState()
	{
	}
	public void ToAgentCircleState()
	{
	}
	public void ToAgentPatrolState()
	{
		searchTimer = 0f;
		agent.currentState = agent.agentPatrolState;
	}
	public void ToAgentAttackState()
	{

	}
	public void ToAgentChaseState()
	{
		searchTimer = 0f;
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
		if (Physics.Raycast (agent.eyes.transform.position, agent.eyes.transform.forward, out hit, agent.sightRange) && hit.collider.CompareTag ("Player")) {
			agent.chaseTarget = hit.transform;
			ToAgentChaseState();
		}
	}

	private void Search()
	{
		agent.meshRendererFlag.material.color = Color.cyan;
		agent.navMeshAgent.Stop ();
		agent.transform.Rotate (0, agent.searchingTurnSpeed * Time.deltaTime, 0);
		searchTimer += Time.deltaTime;

		if (searchTimer >= agent.searchingDuration)
			ToAgentPatrolState ();
	}


}

