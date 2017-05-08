using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeathState : IAgentState {
	private readonly StatePatternAgent agent;
	private float searchTimer;
	private bool isDead = false;

	public AgentDeathState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}
	void OnCollisionEnter (Collision other) { }

	public void UpdateState()
	{
		if (!isDead) {
			agent.agentController.SetAgentAnimationSpeed (1f);
			agent.agentController.SetAgentMove ("kill");
			agent.navMeshAgent.speed = 0f;

			isDead = true;
		}
	}

	public void OnTriggerEnter (Collider other)
	{

	}

}