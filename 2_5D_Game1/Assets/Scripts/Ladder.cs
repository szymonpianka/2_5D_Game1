using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LADDER : MonoBehaviour 

{

	public Transform characterController;
	bool inside = false;
	public float speedUpDown = 3.2f;
    public PlayerMovement playerController;

void Start()
{
	
    playerController = gameObject.GetComponent<PlayerMovement>();
	inside = false;
}

void OnTriggerEnter(Collider col)
{
	if(col.gameObject.tag == "Ladder")
	{
		playerController.enabled = false;
		inside = !inside;
	}
}

void OnTriggerExit(Collider col)
{
	if(col.gameObject.tag == "Ladder")
	{
		playerController.enabled = true;
		inside = !inside;
	}
}
		
void Update()
{
	if(inside == true && Input.GetKey("w"))
	{
			characterController.transform.position += Vector3.up / speedUpDown;
	}
	
	if(inside == true && Input.GetKey("s"))
	{
			characterController.transform.position += Vector3.down / speedUpDown;
	}
}

}