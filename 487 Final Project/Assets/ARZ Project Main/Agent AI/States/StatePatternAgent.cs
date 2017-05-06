using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternAgent : MonoBehaviour {
		public float searchingTurnSpeed = 120f;
		public float searchingDuration = 10f;
		public float circleDuration = 4f;
		public float sightRange = 20f;
		public Transform[] wayPoints;
		public Transform eyes;
		public Vector3 offset = new Vector3 (0,.5f,0);
		public MeshRenderer meshRendererFlag;
		public string defaultState = "patrol";
		public Transform defaultEatObject;
		public string agentType = "leader"; 
		public Transform defaultLeader;

		/*
		 *   An Agent Can Be:
		 * 
		 * 	 leader
		 *   follower
		 * 
		 */ 

		[HideInInspector] public Transform chaseTarget;
		[HideInInspector] public Transform eatObject;
		public IAgentState currentState;
		[HideInInspector] public AgentCuteState agentCuteState;
		[HideInInspector] public AgentPatrolState agentPatrolState;
		[HideInInspector] public AgentChaseState agentChaseState;
		[HideInInspector] public AgentCircleState agentCircleState;
		[HideInInspector] public AgentAttackState agentAttackState;
		[HideInInspector] public AgentEatState agentEatState;
		[HideInInspector] public AgentAlertState agentAlertState;
		[HideInInspector] public AgentDeathState agentDeathState;
		[HideInInspector] public AgentSleepState agentSleepState;
	[HideInInspector] public AgentApproachTargetState agentApproachTargetState;
		[HideInInspector] public NavMeshAgent navMeshAgent;
		[HideInInspector] public AgentController agentController;

		private void Awake()
		{
			agentCuteState = new AgentCuteState (this);
			agentPatrolState = new AgentPatrolState (this);
			agentChaseState = new AgentChaseState (this);
			agentCircleState = new AgentCircleState (this);
			agentAttackState = new AgentAttackState (this);
			agentEatState = new AgentEatState (this);
			agentAlertState = new AgentAlertState (this);
			agentDeathState = new AgentDeathState (this);
			agentSleepState = new AgentSleepState (this);
		agentApproachTargetState = new AgentApproachTargetState (this);

			navMeshAgent = GetComponent<NavMeshAgent> ();
			agentController = GetComponent<AgentController> ();
		}

		// Use this for initialization
		void Start () 
		{
			ToState ("ApproachTarget");
			chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;


			/*
				if (defaultLeader) {
				// TODO: Make a follow state
					chaseTarget = defaultLeader;
					currentState = agentChaseState;
				} else if (defaultState == "patrol") {
					currentState = agentPatrolState;
				} else if (defaultState == "eat") {
					eatObject = defaultEatObject;
					currentState = agentEatState;
				}
			*/
		}

		// Update is called once per frame
		void Update () 
		{
		Debug.Log (currentState);
				currentState.UpdateState ();
		}

		private void OnTriggerEnter(Collider other)
		{
		/*
			if (currentState == agentSleepState) {
			} else {
				if (other.gameObject.CompareTag ("Agent")) {
					StatePatternAgent otherAgent = other.gameObject.GetComponent<StatePatternAgent> ();
					if (otherAgent.agentController.isdead) {
						BoxCollider collider = gameObject.GetComponent<BoxCollider> ();
						collider.size = new Vector3 (collider.size.x + 2f, collider.size.y + 2f, collider.size.z + 2f);
						otherAgent.agentController.doNothing = true;
						otherAgent.navMeshAgent.Stop ();

						eatObject = other.gameObject.transform;
						currentState = agentEatState;
					}
				} else if (other.gameObject.CompareTag ("deadPlayer")) {
					eatObject = other.gameObject.transform;
					currentState = agentEatState;
				} else { 
					currentState.OnTriggerEnter (other);
				}
			}
			*/
		}
	public void Kill() {
		/*
		if (!agentController.isdead) {
			agentController.SetAgentMove ("kill");
			currentState = agentDeathState;
			navMeshAgent.Stop ();
		}
		*/
	}
	public void Sleep() {
		/*
		if (!agentController.isdead) {
			agentController.SetAgentMove ("sleep");
			currentState = agentSleepState;
			navMeshAgent.Stop ();
		}
		*/
	}
	public void ToState(string stateName) {
		Debug.Log ("Changing to state: " + stateName);
		switch (stateName) {
		case "ApproachTarget":
				currentState = agentApproachTargetState; 
				break;			
			case "Patrol": 
				currentState = agentPatrolState;
				break;
			default: 
				break;
		}
	}
}
