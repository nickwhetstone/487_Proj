using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeathState : IAgentState {
	private readonly StatePatternAgent agent;
	private float searchTimer;

	public AgentDeathState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		
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

