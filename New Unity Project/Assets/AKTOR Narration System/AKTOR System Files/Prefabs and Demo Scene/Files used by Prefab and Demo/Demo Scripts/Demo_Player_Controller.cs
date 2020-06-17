using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Player_Controller : MonoBehaviour {




	public string INFO = "This script simply moves around the player using WSAD keys (default Vertical and Horizontal axis)";
	public bool PlayerHasControl = true;
	private CharacterController c;
	private float vertical;
	private float horizontal;
	private float originalY;
	private Vector3 temppos;
	// Update is called once per frame
	void Start()
	{
		if(c == null)
		{
			c = GetComponent<CharacterController>();
		}
		originalY = c.transform.position.y;
	}
	
	void Update () 
	{
		
			vertical = Input.GetAxisRaw("Vertical");
			horizontal = Input.GetAxisRaw("Horizontal");
			
			
			if(horizontal != 0 && PlayerHasControl == true)
			{
				transform.Rotate(transform.up * horizontal * Time.deltaTime * 90f);
			}
			if(vertical != 0 && PlayerHasControl == true)
			{
				c.Move(transform.forward * Time.deltaTime * vertical * 3f);
			}
			
			temppos = c.transform.position;
			temppos.y = originalY;
			c.transform.position = temppos;
	}
	
	public void DialogueStart()
	{
		PlayerHasControl = false;
	}
	
	public void DialogueEnd()
	{
		PlayerHasControl = true;
	}
}
