using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSleepState : IAgentState {
	private readonly StatePatternAgent agent;
	private float sleepTimer;

	public AgentSleepState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		sleepTimer++;
		if (sleepTimer >= 30f) {
			ToAgentPatrolState ();
		}
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
		sleepTimer = 0f;
		agent.currentState = agent.agentPatrolState;
	}
	public void ToAgentAttackState()
	{

	}
	public void ToAgentChaseState()
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
}

