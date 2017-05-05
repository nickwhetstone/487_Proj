using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AgentCuteState : IAgentState 
{
	private readonly StatePatternAgent agent;

	public AgentCuteState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		
	}

	public void OnTriggerEnter (Collider other)
	{
		
	}
	public void ToAgentCircleState()
	{
	}
	public void ToAgentCuteState()
	{
		// current state
	}
	public void ToAgentPatrolState()
	{
		Debug.Log ("Can't transition to same state");
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
}