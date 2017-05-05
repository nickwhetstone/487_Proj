using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEatState : IAgentState {
	private readonly StatePatternAgent agent;

	public AgentEatState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}

	public void UpdateState()
	{
		Look ();
		Eat ();
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
		agent.currentState = agent.agentPatrolState;
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
	private void Look()
	{
		GameObject plyr = GameObject.FindGameObjectWithTag ("Player");
		if (5f > Vector3.Distance (agent.navMeshAgent.gameObject.transform.position, plyr.transform.position)) {
			agent.chaseTarget = plyr.transform;
			ToAgentChaseState ();
		}
	}
	public void ToAgentDeathState()
	{
	}
	public void ToAgentSleepState()
	{
	}
	private void Eat()
	{
		agent.meshRendererFlag.material.color = Color.magenta;
		agent.navMeshAgent.destination = agent.eatObject.position;
		agent.navMeshAgent.stoppingDistance = 0.1f;
		agent.agentController.SetAgentMove ("eat");

		// make them smaller, and us bigger
		/*
		agent.eatObject.transform.localScale += new Vector3(-0.01F, -0.01F, -0.01F);
		agent.agentController.scale += 0.01F;
		if (agent.agentController.scale > 0.34f) {
			agent.agentController.scale = 0.34f;
		}
		// are we done eating?
		if (agent.eatObject.transform.localScale.x < 0) {
			ToAgentPatrolState ();
		}
		*/
		agent.navMeshAgent.Resume ();
	}

}