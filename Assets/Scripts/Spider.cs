﻿using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	//public vars
	public float MovementRadius = 10f;
	public float MovementSpeed = 3f;
	public float AttackVisionRange = 3f;
	public float AttackRange = 1f;
	public float AttackDamage = 10f;
	public float TimeBetweenAttacks = .5f;
	
	//private vars
	Vector3 startPos;
	Vector3 newPos;
	float pointRange = .5f;
	Vector3 flatTransform;
	Vector3 flatNewPos;
	float distanceToNewPos;
	float distanceToPlayer;
	GameObject player;
	bool Attacking = false;
	float lastAttack = 0f;
	
	// Use this for initialization
	void Start () {
		
		//set defaults
		startPos = transform.position;
		newPos = startPos;
		player = GameObject.Find("Player");
	}	
	
	// Update is called once per frame
	void Update () {
		
		flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
		flatNewPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
		distanceToPlayer = Vector3.Distance(flatTransform, flatNewPos);
		
		if (AttackVisionRange > distanceToPlayer)
		{
			Attacking = true;
			
			
			if (AttackRange > distanceToPlayer)
			{
				//Attack player
				if (lastAttack > TimeBetweenAttacks)
				{
					player.SendMessage("TakeDamage",AttackDamage,SendMessageOptions.DontRequireReceiver);
					Debug.Log("Player Attacked!");
					lastAttack = 0f;
				}
				else
				{
					lastAttack += Time.deltaTime;	
				}
			}
			else
			{
				//Move towards the player
				transform.LookAt(player.transform);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				//rigidbody.AddForce (transform.forward *MovementSpeed );
				rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
			}			
		}
		else
		{
			lastAttack = 0f;
			Attacking = false;
			flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
			flatNewPos = new Vector3(newPos.x, 0, newPos.z);
			distanceToNewPos = Vector3.Distance(flatTransform, flatNewPos);
			
			if (pointRange > distanceToNewPos)
			{
				//Find a new random position within the movement radius
				newPos = startPos + (Random.insideUnitSphere * MovementRadius);
				transform.LookAt(newPos);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
			else
			{
				//not at new position, so move some towards it.
				//transform.LookAt(newPos);
				//transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				transform.LookAt(newPos);
				
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				//rigidbody.AddForce (transform.forward *MovementSpeed );
				rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
			}
		}
		
		
	}
}
