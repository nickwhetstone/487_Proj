using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeathState : IAgentState {
	private readonly StatePatternAgent agent;
	private bool isDead = false;
	private float deathTimer = 0f;

	public AgentDeathState (StatePatternAgent statePatternAgent)
	{
		agent = statePatternAgent;
	}
	void OnCollisionEnter (Collision other) { }

	public void UpdateState()
	{
		if (!isDead) {
			ARZ_Player_Control.killCount++;
			agent.agentController.SetAgentAnimationSpeed (1f);
			agent.agentController.SetAgentMove ("kill");
			agent.navMeshAgent.speed = 0f;
			isDead = true;

		} else {
			deathTimer += Time.deltaTime;
		}
		if (deathTimer > 5f) {
			Revive ();
		}
	}

	public void OnTriggerEnter (Collider other)
	{

	}
	void Revive() {
		Debug.Log ("Revive");
		isDead = false;
		deathTimer = 0f;
		agent.ToState ("ApproachTarget");
	}

}