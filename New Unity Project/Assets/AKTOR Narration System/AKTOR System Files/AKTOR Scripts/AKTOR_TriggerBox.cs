using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AKTOR_TriggerBox : MonoBehaviour {

	[Tooltip("Specify the gameobject with AKTOR_System to notify about the player activating this Trigger")]
	public GameObject AKTORToNotify;
	[Tooltip("To know what object is a player, we must specify the Player's unique Tag here.")]
	public string PlayerTag = "Player";
	[Tooltip("Instead of just opening a dialogue when player enters the trigger, you can set a button he has to press while standing in the trigger to enter the dialogue. If so, specify the input name here. If you want the dialogue to be triggered instantly upon player entering the trigger, leave this field empty.")]
	public string ButtonToInteract = "Use";
	[Tooltip("You can enable an object which holds UI Text which will tell the player that he must [press INPUT to interact]. It can be a UI Canvas or the UI Text itself.")]
	public GameObject InteractionPromptCanvas;
	[Tooltip("This is the UI Text object which will spell the Interaction Prompt text, like [Press Something to interact]")]
	public Text InteractionPrompt;
	[Tooltip("You can remotely find the UI Text object which will get the Interaction Prompt content when Player enters Triggerbox")]
	public string FindInteractionPromptByTag;
	[Tooltip("The content of the interaction prompt, like [Press somthing to interact]")]
	public string InteractionPromptText;
	[Tooltip("If your interaction prompt is set somewhere in the World Space, should it always look at the camera?")]
	public bool InteractionPrompRotatesTowardsCamera;
	[Tooltip("If the above is true, assign the camera to look at. You can also remotely find it below.")]
	public Transform Cam;
	[Tooltip("You can find the camera for InteractionPrompRotatesTowardsCamera feature remotely. Just assign it's unique Tag here.")]
	public string FindCameraByTag = "Active Camera";
	
	//Work Variables
	private bool ReadyToActivate;
	[HideInInspector]
	public bool ChatActive;
	
	
	void Start()
	{
		if(string.IsNullOrEmpty(FindInteractionPromptByTag) == false)
		{
			InteractionPrompt = GameObject.FindWithTag(FindInteractionPromptByTag).GetComponent<Text>();
		}
		
		if(InteractionPromptCanvas != null)
		{
			InteractionPromptCanvas.SetActive(false);
		}
		
		//in case that user doesn't want to have an interaction prompt text (automatic trigger), but forgets to clear the Interaction Prompt object field. For user's comfort and less issues. :)  
		if(InteractionPromptCanvas != null && InteractionPrompt != null && string.IsNullOrEmpty(ButtonToInteract) == true)
			{
				InteractionPrompt.text = "";
			}
	}
	
	
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.CompareTag(PlayerTag) && ChatActive == false)
		{
			if(string.IsNullOrEmpty(ButtonToInteract) == true)
			{
				TriggerActivated();
			}
			if(string.IsNullOrEmpty(ButtonToInteract) == false)
			{
				EnableInteractionPrompt();
			}
		}
	}
	
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.CompareTag(PlayerTag))
		{
			if(string.IsNullOrEmpty(ButtonToInteract) == false)
			{
				ReadyToActivate = false;
			}
			DisableInteractionPrompt();
		}
	}
	
	public void EnableInteractionPrompt()
	{
		if(  InteractionPrompt != null)
			{
				ReadyToActivate = true;
				if(InteractionPromptCanvas != null)
				{
				InteractionPromptCanvas.SetActive(true);
				}
				InteractionPrompt.text = InteractionPromptText;
				if(InteractionPrompRotatesTowardsCamera == true)
				{
					if(Cam == null && string.IsNullOrEmpty(FindCameraByTag) == false)
					{
					Cam = GameObject.FindWithTag(FindCameraByTag).transform;
					}
				if(InteractionPromptCanvas != null)
				{
				InteractionPromptCanvas.transform.LookAt(Cam.position);
				}
				}
			}
			
			
	}
	
	public void DisableInteractionPrompt()
	{
		if(InteractionPromptCanvas!= null)
		{
		InteractionPromptCanvas.SetActive(false);
		}
		if(InteractionPrompt != null)
		{
			InteractionPrompt.text = "";
		}
	}

	
	
	void Update()
	{
		if(ReadyToActivate == true && ChatActive == false)
		{
			if(InteractionPrompRotatesTowardsCamera == true)
			{
				if(Cam == null && string.IsNullOrEmpty(FindCameraByTag) == false)
				{
					Cam = GameObject.FindWithTag(FindCameraByTag).transform;
				}
				if(InteractionPromptCanvas != null)
				{
				InteractionPromptCanvas.transform.LookAt(Cam.position);
				}
			}
			if(string.IsNullOrEmpty(ButtonToInteract) == false)
			{
				if(Input.GetButtonDown(ButtonToInteract))
				{
				TriggerActivated();
				ReadyToActivate = false;
				DisableInteractionPrompt();
				}
			}
		}
	}
	
	
	public void TriggerActivated()
	{
		AKTORToNotify.SendMessage("StartDialogue");
	}
}
