using UnityEngine;

public class AgentController : MonoBehaviour
{
	public float ang,velZ,velY;
	public bool onwater,isdead;
	public AudioClip Smallstep,Smallsplash,Bite,Comp1,Comp2,Comp3,Comp4,Comp5; // TODO

	Animator anim;
	AudioSource source;
	Rigidbody rg;

	public bool AI = false;
	public string agentMove = "";
	public bool doNothing = false;

	//***************************************************************************************
	//Get components
	void Awake ()
	{
		source = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		rg=GetComponent<Rigidbody>();
	}


	//***************************************************************************************
	//Colliders
	void OnTriggerStay(Collider coll)
	{
		if(coll.transform.name=="Water") { anim.speed=0.75f; onwater=true; } //Is on water ?
	}
	void OnTriggerExit(Collider coll)
	{
		if(coll.transform.name=="Water") { anim.speed=1.0f; onwater=false; }
	}


	//***************************************************************************************
	//Play sound
	float currframe=0, lastframe=0;
	void Update ()
	{
		//Get current animation frame
		if(anim.GetAnimatorTransitionInfo(0).normalizedTime!=0.0f || currframe==15f) { currframe=0.0f; lastframe=-1; }
		else currframe = Mathf.Round((anim.GetCurrentAnimatorStateInfo (0).normalizedTime % 1.0F) * 15);
	}
	void PlaySound(string name,int time)
	{
		// TODO

		if((time==currframe)&&(currframe!=lastframe))
		{
			switch (name)
			{
			case "Step":  source.pitch=Random.Range(1.0F, 1.25F); source.PlayOneShot(onwater?Smallsplash:Smallstep,Random.Range(0.3F, 0.6F));
				lastframe=currframe; break;
			case "Bite":   source.pitch=Random.Range(1.0F, 1.25F); source.PlayOneShot(Bite,0.5f);
				lastframe=currframe; break;
			case "Die":   source.pitch=Random.Range(0.8F, 1.0F); source.PlayOneShot(onwater?Smallsplash:Smallstep,1.0f);
				lastframe=currframe; isdead=true; break;
			case "Call":   source.pitch=Random.Range(1.0F, 1.25F); source.PlayOneShot(Comp4,1.0f);
				lastframe=currframe; break;
			case "Atk": int rnd1 = Random.Range (0,3); source.pitch=Random.Range(1.0F, 1.75F);
				if(rnd1==0)source.PlayOneShot(Comp2,1.0f);
				else if(rnd1==1)source.PlayOneShot(Comp3,1.0f);
				else if(rnd1==2) source.PlayOneShot(Comp5,1.0f);
				lastframe=currframe; break;
			case "Growl": int rnd2 = Random.Range (0,5); source.pitch=Random.Range(1.0F, 1.75F);
				if(rnd2==0)source.PlayOneShot(Comp1,1.0f);
				else if(rnd2==1)source.PlayOneShot(Comp2,1.0f);
				else if(rnd2==2)source.PlayOneShot(Comp3,1.0f);
				else if(rnd2==3)source.PlayOneShot(Comp4,1.0f);
				else if(rnd2==4)source.PlayOneShot(Comp5,1.0f);
				lastframe=currframe; break;
			}
		}
	}
	public void SetAgentMove(string move) {
		if (agentMove == move) {
			// we are already doing this move no?
		} else {
			agentMove = move;
			UpdateAgent ();
		}
	}
	//Animation controller
	public void UpdateAgent() {
		if (AI) //CPU
		{
			//Moves
			/*
			 *  NOTE: These need to be hooked up to the right animations in the animation controller
			 * 
			 *   jump
			 * 	 run
			 *   walk
			 *   stepBack
			 *   strafeRight
			 * 	 strafeLeft
			 * 	 attack
			 *   idle1 -> 4
			 *   eat
			 *   drink
			 *   sleep
			 *   kill
			 * 
			 * 
			 */ 
			Debug.Log("Agent Move: " + agentMove);
			if (agentMove == "jump") anim.SetInteger ("State", 2); //Jump
			else if (agentMove == "run") anim.SetInteger ("State", 3); //Run
			else if (agentMove == "walk") anim.SetInteger ("State", 1); //Walk
			else if (agentMove == "stepBack") anim.SetInteger ("State", -1); //Steps Back
			else if (agentMove == "strafeRight") anim.SetInteger ("State", 10); //Strafe+
			else if (agentMove == "strafeLeft")anim.SetInteger ("State", -10); //Strafe-
			else anim.SetInteger ("State", 0); //Idle

			//Turn
			if(agentMove == "strafeRight" && velZ!=0)ang = Mathf.Lerp(ang,-4.0f,0.025f);
			else if(agentMove == "strafeLeft" && velZ!=0) ang = Mathf.Lerp(ang,4.0f,0.025f);
			else ang = Mathf.Lerp(ang,0.0f,0.05f);

			//Attack
			if (agentMove == "attack") anim.SetBool ("Attack", true);
			else anim.SetBool ("Attack", false);

			//Idles
			if (agentMove == "idle1") anim.SetInteger ("Idle", 1); //Idle 1
			else if (agentMove == "idle2") anim.SetInteger ("Idle", 2); //Idle 2
			else if (agentMove == "idle3") anim.SetInteger ("Idle", 3); //Idle 3
			else if (agentMove == "idle4") anim.SetInteger ("Idle", 4); //Idle 4
			else if (agentMove == "eat") anim.SetInteger ("Idle", 5); //Eat
			else if (agentMove == "drink") anim.SetInteger ("Idle", 6); //Drink
			else if (agentMove == "sleep") anim.SetInteger ("Idle", 7); //Sleep
			else if (agentMove == "kill") anim.SetInteger ("Idle", -1); //Kill
			else anim.SetInteger ("Idle", 0);

		}
	}
	//***************************************************************************************
	//Animation controller
	void FixedUpdate ()
	{
		if (!AI)//Human
		{
			//Moves
			if (Input.GetKey (KeyCode.Space)) anim.SetInteger ("State", 2); //Jump
			else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) anim.SetInteger ("State", 3); //Run
			else if (Input.GetKey (KeyCode.W)) anim.SetInteger ("State", 1); //Walk
			else if (Input.GetKey (KeyCode.S)) anim.SetInteger ("State", -1); //Steps Back
			else if (Input.GetKey (KeyCode.A)) anim.SetInteger ("State", 10); //Strafe+
			else if (Input.GetKey (KeyCode.D))anim.SetInteger ("State", -10); //Strafe-
			else anim.SetInteger ("State", 0); //Idle

			//Turn
			if(Input.GetKey(KeyCode.A)&& velZ!=0)ang = Mathf.Lerp(ang,-4.0f,0.025f);
			else if(Input.GetKey(KeyCode.D)&& velZ!=0) ang = Mathf.Lerp(ang,4.0f,0.025f);
			else ang = Mathf.Lerp(ang,0.0f,0.05f);

			//Attack
			if (Input.GetKey (KeyCode.Mouse0)) anim.SetBool ("Attack", true);
			else anim.SetBool ("Attack", false);

			//Idles
			if (Input.GetKey (KeyCode.Alpha1)) anim.SetInteger ("Idle", 1); //Idle 1
			else if (Input.GetKey (KeyCode.Alpha2)) anim.SetInteger ("Idle", 2); //Idle 2
			else if (Input.GetKey (KeyCode.Alpha3)|| Input.GetKey (KeyCode.E)) anim.SetInteger ("Idle", 3); //Idle 3
			else if (Input.GetKey (KeyCode.Alpha4)) anim.SetInteger ("Idle", 4); //Idle 4
			else if (Input.GetKey (KeyCode.Alpha5)) anim.SetInteger ("Idle", 5); //Eat
			else if (Input.GetKey (KeyCode.Alpha6)) anim.SetInteger ("Idle", 6); //Drink
			else if (Input.GetKey (KeyCode.Alpha7)) anim.SetInteger ("Idle", 7); //Sleep
			else if (Input.GetKey (KeyCode.Alpha8)) anim.SetInteger ("Idle", -1); //Kill
			else anim.SetInteger ("Idle", 0);
		}


		//***************************************************************************************
		//Motion and sound

		// TODO: Add these to ---> UpdateAgent()

		//Stop
		if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|StandA") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandA"))
		{
			velZ = Mathf.Lerp(velZ,0.0f,0.5f);
			transform.Translate (0, 0, velZ*anim.speed);
		}

		//Forward
		else if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|Walk") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Walk"))
		{
			velZ = Mathf.Lerp(velZ,0.2f,0.025f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (0, 0, velZ*anim.speed);

			//Sound
			PlaySound("Step",8); PlaySound("Step",7);
		}

		//Backward
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Walk-") ||
			anim.GetNextAnimatorStateInfo (0).IsName("Comp|Walk-"))
		{
			velZ = Mathf.Lerp(velZ,0.2f,0.025f);
			transform.rotation *= Quaternion.Euler(0,-ang,0);
			transform.Translate (0, 0, -velZ*anim.speed);

			//Sound
			PlaySound("Step",8); PlaySound("Step",7);
		}

		//Running
		else if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|Run") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Run") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunGrowl") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunAttackB") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunJumpDown") ||
			(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunAttackA") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.9) ||
			(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunJumpUp") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.4) ||
			(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|JumpAttack") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.5 && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.9))
		{
			velZ = Mathf.Lerp(velZ,0.3f,0.1f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (0, 0, velZ*anim.speed);

			//Sound
			if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Run")){ PlaySound("Step",4); PlaySound("Step",12); }
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunGrowl")) { PlaySound("Growl",2); PlaySound("Step",4); PlaySound("Step",12); }
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunJumpDown")) { PlaySound("Step",2);PlaySound("Step",3); PlaySound("Step",12); }
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunJumpUp")) PlaySound("Atk",2);
			else { PlaySound("Atk",2); PlaySound("Step",4);  PlaySound("Bite",9); PlaySound("Step",12); }
		}

		//Strafe-/Turn
		else if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|Strafe-") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Strafe-"))
		{
			velZ = Mathf.Lerp(velZ,0.001f,0.1f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (velZ*anim.speed, 0, 0);

			//Sound
			PlaySound("Step",8); PlaySound("Step",7);
		}

		//Strafe+/Turn
		else if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|Strafe+") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Strafe+"))
		{
			velZ = Mathf.Lerp(velZ,0.001f,0.1f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (-velZ*anim.speed, 0, 0);

			//Sound
			PlaySound("Step",8); PlaySound("Step",7);
		}

		//Jump up
		else if ((anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|RunJumpUp") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.4) ||
			(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandJumpUp") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.4))
		{

			velY = Mathf.Lerp(0.4f,0.0f,0.1f);
			velZ = Mathf.Lerp(velZ,0.0f,0.001f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (0, velY, velZ*anim.speed);
		}

		//In Air Attack
		else if (anim.GetNextAnimatorStateInfo (0).IsName("Comp|JumpLoopAttack") ||
			anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|JumpLoopAttack"))
		{
			velY = Mathf.Lerp(velY,-0.8f,0.01f);
			velZ = Mathf.Lerp(velZ,0.0f,0.001f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (0, velY, velZ*anim.speed);

			//Sound
			PlaySound("Atk",2); PlaySound("Bite",9);
		}

		//In Air
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|JumpLoop"))
		{
			velY = Mathf.Lerp(velY,-0.8f,0.05f);
			velZ = Mathf.Lerp(velZ,0.0f,0.001f);
			transform.rotation *= Quaternion.Euler(0,ang,0);
			transform.Translate (0, velY, velZ*anim.speed);
		}

		//Stop
		else
		{
			velZ = Mathf.Lerp(velZ,0.0f,0.5f);
			velY = Mathf.Lerp(velY,0.0f,1.0f);
			transform.Translate (0, velY, velZ*anim.speed);

			//Sound
			if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandJumpUp")) PlaySound("Atk",0);
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandJumpDown")) { PlaySound("Step",1);PlaySound("Step",2); }
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|JumpAttack")) PlaySound("Atk",0);
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|GroundAttack")) { PlaySound("Atk",2); PlaySound("Bite",4);}
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|AttackA")) { PlaySound("Atk",2); PlaySound("Bite",9);}
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|AttackB")) { PlaySound("Atk",2); PlaySound("Bite",9);}
			else if(anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|EatA")) PlaySound("Bite",2);
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandB")) PlaySound("Growl",1);
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandC")) PlaySound("Growl",1);
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandD")) PlaySound("Growl",1);
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|StandE")) {PlaySound("Call",1);  PlaySound("Call",4); PlaySound("Call",8);}
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Die") && !isdead) { PlaySound("Atk",1); PlaySound("Die",12); }
			else if (anim.GetCurrentAnimatorStateInfo (0).IsName("Comp|Rise")) { PlaySound("Growl",1); isdead=false;}
		}

	}


	//***************************************************************************************
	//Bone rotations, model modification and stick to the terrain
	void LateUpdate()
	{
		if (doNothing) { 
		} else {

			//Disable collision and freeze position
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|SleepLoop") ||
				anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|Sleep+") ||
				anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|Sleep-") ||
				anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|Die"))
				rg.isKinematic = true;
			else
				rg.isKinematic = false;
			rg.velocity = Vector3.zero;
			rg.freezeRotation = true;

			//Stick and slip on terrain
			RaycastHit hit;
			int terrainlayer = 1 << 8; //terrain layer only
			if (anim.GetInteger ("Idle") != 100 && Physics.Raycast (transform.position + transform.up, -transform.up, out hit, Mathf.Infinity, terrainlayer)) {
				//jump, disable stick to the terrain
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|StandJumpUp") &&
					!anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|RunJumpUp") &&
					!anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|JumpLoop") &&
					!anim.GetCurrentAnimatorStateInfo (0).IsName ("Comp|JumpLoopAttack"))
					transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, hit.point.y, 0.25f), transform.position.z);

				//is on ground ?
				if (Mathf.Round (transform.position.y * 10 - hit.point.y * 10) <= 0) {
					anim.SetBool ("Onground", true); 
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (Vector3.Cross (transform.right, hit.normal), hit.normal), 0.1f);
				} else {
					anim.SetBool ("Onground", false);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, transform.eulerAngles.y, 0), 0.1f);
				}

				//slip on sloped terrain and avoid
				float xs = 0, zs = 0;
				if (Mathf.DeltaAngle (transform.eulerAngles.x, 0.0f) > 25.0f || Mathf.DeltaAngle (transform.eulerAngles.x, 0.0f) < -25.0f ||
					Mathf.DeltaAngle (transform.eulerAngles.z, 0.0f) > 25.0f || Mathf.DeltaAngle (transform.eulerAngles.z, 0.0f) < -25.0f) {
					xs = Mathf.Lerp (xs, -Mathf.DeltaAngle (transform.eulerAngles.x, 0.0f) / 10, 0.01f);
					zs = Mathf.Lerp (zs, Mathf.DeltaAngle (transform.eulerAngles.z, 0.0f) / 10, 0.01f);
					if (zs > 0)
						ang = Mathf.Lerp (ang, 2.0f, 0.05f);
					else
						ang = Mathf.Lerp (ang, -2.0f, 0.05f);
					transform.Translate (zs, 0, xs);
				}
			}
		}
	}
}