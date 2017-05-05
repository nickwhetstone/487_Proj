using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentState
{

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToAgentCuteState();

	void ToAgentPatrolState();

	void ToAgentChaseState();

	void ToAgentCircleState();

	void ToAgentAttackState();

	void ToAgentEatState();

	void ToAgentAlertState();

	void ToAgentDeathState();

}

