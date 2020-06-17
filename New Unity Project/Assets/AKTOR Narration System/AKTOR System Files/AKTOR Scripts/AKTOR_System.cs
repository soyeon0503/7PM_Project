using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AKTOR_System : MonoBehaviour {

	
	[Header("Basic Variables")]
	[Tooltip("As a general rule, each variable has an in-depth description in a mouse-over tooltip. Also, if you do not want to use some of the System's feature, then simply leave a given variable empty. ")]
	public string INFO = "Mouse-over this variable for important information!";
	[Tooltip("In most cases you will want to disable your Player's control during dialogue. This depends heavily on your project, so from AKTOR side you can call a function to any object you want from this NPC's Screenplay. Check out AKTOR_Screenplay script and it's [SendFunctionOnDialogueStart] feature.")]
	public string PlayerControlINFO = "Mouse-over this variable for important information!";
	[Tooltip("If you want to use any sound (like dialogue, sound effects or toon gibberish) during your dialogues, specify the AudioSource of this AKTOR here")]
	public AudioSource Sound;
	[Tooltip("If you want to use any NPC animations (or any other animations to be honest), specify the Animator here.")]
	public Animator NPCAnimations;
	
	
	[Header("Focus Features")]
	[Space(10)]
	[Tooltip("AKTOR System can move and rotate the Player, the NPC and Camera according to your will, but keep in mind - if you plan on using any Camera Shake effects or other custom features which will be moving the Player, the NPC or the Camera, then it's better to store the moved object in an empty gameobject as a child - then use AKTOR to move the parent or the child, and your custom scripts to use the other one (parent or child), so these two won't interfere with the position or rotation of each other's object. (in other case, when you use Camera Shake during an AKTOR camera zoom, the camera may then return to wrong position.")]
	public string CAUTION_A = "Mouse-over this variable for important information!";
	[Tooltip("If you want to move the camera during dialogue, specify it's object here. Or you can just find it remotely in a variable below.")]
	public Transform Cam;
	[Tooltip("If you want to find the Camera remotely to move it during dialogue, specify it's Tag here. (only one object - the camera itself -  should have this tag!")]
	public string FindCameraByTag = "Active Camera";
	[Tooltip("If you want to move the Player during dialogue, specify it's object here. Or you can just find it remotely in a variable below.")]
	public Transform Player;
	[Tooltip("If you want to find the Player remotely to move it during dialogue, specify it's Tag here. (only one object - the Player itself -  should have this tag!")]
	public string PlayerTag = "Player";
	[Tooltip("The Camera will automatically look at this object during dialogue. An empty Game Object will suffice.")]
	public Transform CameraFocusPoint;
	[Tooltip("How fast the camera should look at the Focus Point?")]
	public float CameraFocusSpeed = 10.0f;
	[Tooltip("The Player will automatically look at this object during dialogue. An empty Game Object will suffice.")]
	public Transform PlayerFocusPoint;
	[Tooltip("Prevent the player from rotating in X or Z axis during dialogue. Rule of thumb: if you plan on using humanoid character, set it to True. If you plan on using some abstract character (like a flying drone), set it to False.")]
	public bool OnlyHorizontalPlayerFocus = true;
	[Tooltip("How fast the Player turn towards the Focus Point? It's generaly better to set it really high (near-instant motion)")]
	public float PlayerFocusSpeed = 10.0f;
	[Tooltip("Where do you want to move the player during dialogue? Set an empty Game Object here to specify this position.")]
	public Transform PlayerPositionOnFocus;
	[Tooltip("How fast the Player should be moved to this position on Focus? It's generaly better to set it really high (near-instant motion)")]
	public float PlayerMoveToFocusPositionSpeed = 30.0f;
	[Tooltip("Prevent the player from moving in Y axis during dialogue. It's handy if do not want to set the PlayerPositionOnFocus to a pinpoint Y axis position tomatch the height your player is moving on.")]
	public bool OnlyHorizontalPlayerPush = true;
	[Tooltip("Should the player return to the previous position after the end of the dialogue?")]
	public bool ReturnPlayerToPreviousPositionAfterDialogue;
	[Tooltip("Where should the camera move to during this dialogue?")]
	public Transform CameraPositionOnFocus;
	[Tooltip("How fast the Camera should be moved to this position on Focus?")]
	public float CameraMoveToFocusPositionSpeed = 30.0f;
	[Tooltip("This is the NPC Model which should rotate towards it's focus point during dialogue (like towards the Player when speaking to him).")]
	public Transform NPCFocusingObject;
	[Tooltip("The point that the NPC will look at during dialogues. You can find it remotely with Tag, or assign it by hand one variable below.")]
	public string FindNPCFocusPointByTag;
	[Tooltip("The point that the NPC will look at during dialogues. You can find it remotely with Tag one variable above, or assign it by here by hand.")]
	public Transform NPCFocusPoint;
	[Tooltip("Prevent the NPC from rotating in X or Z axis during dialogue. Rule of thumb: if you plan on using humanoid character, set it to True. If you plan on using some abstract character (like a flying drone), set it to False.")]
	public bool OnlyHorizontalNPCFocus = true;
	[Tooltip("How fast the NPC turns towards the Focus Point?")]
	public float NPCFocusSpeed = 10.0f;
	[Tooltip("Should the NPC return to the original rotation after dialogue ends?")]
	public bool ReturnNPCToOriginalRotationAfterDialogue;
	
	
	[Header("Responses")]
	[Tooltip("Instead of going through all the Responses in chosen AKTOR Screenplay, you can set this True to force this NPC to pick just one, random response at each activation. Good for a generic response for a shopkeeper's greeting or a reaction to player casually passing by.")]
	public bool SingleRandomResponse;
	[Tooltip("When a dialogue is activated, AKTOR will speak the texts of this Screenplay one by one. Think lines of the dialogue or pages of a letter.")]
	public AKTOR_Screenplay CurrentScreenplay;
	[Tooltip("You can change the content which will be spoken by NPC at any time. Just assign alternative AKTOR Screenplays here and call SetNewScreenplay(<NumberOfAKTOR_Screenplay>) to change it. Just don't change it in the middle of the dialogue! Do it at the end of it or when dialogue is not active. ")]
	public AKTOR_Screenplay[] OtherScreenplays;
	[Tooltip("This AKTOR_Screenplay will become active when the CurrentScreenplay's dialogue has ended (in aa [I have already spoken everything I wanted to say] fashion). If this if left empty, the CurrentScreenplay will be looped.")]
	public AKTOR_Screenplay FinalScreenplay;
	
	[Header("Chat Box Elements")]
	[Tooltip("You can choose any UI Text elements for this AKTOR to use, but keep in mind to never assign the same UI Elements to be used by two AKTOR objects at the same time. Separately one by one (so ending a dialogue with first NPC and then starting a dialogue with another) it will work fine, but two NPCs at once for example may interfere with the flow of each other.")]
	public string CAUTION_B = "Mouse-over this variable for important information!";
	[Tooltip("This will be the text shown as NPC Name in NPC Name UI Text fields (if you plan on using it. In case of books or other holo tapes, it can be a title of the document for example")]
	public string NPCName;
	[Tooltip("Assign the Text UI which will show this NPC's Name. You can find it remotely one variable below.")]
	public Text NPCNameLabel;
	[Tooltip("Find remotely the Text UI which will show the NPC's name. Just assign a custom tag to it and specify it here.")]
	public string FindNPCNameLabelByTag;
	[Tooltip("Specify if you want to play an animation of opening and closing the dialogue box on  Dialogue Start and End. There are three animation states you need to create: ChatBox_Intro for Dialogue Start, ChatBox_Outro for dialogue end and a special ChatBox_Off with all the chatbox just turned off.")]
	public Animator ChatBoxAnimation;  
	[Tooltip("The UI Text with main content of the dialogue (or a book text). You can specify it here or find it by Tag below")]
	public Text ChatBoxContent;
	[Tooltip("Find the Text UI with the main content of the dialogue by it's tag?")]
	public string FindChatBoxContentByTag;
	[Tooltip("Do you want the chat box/dialogue box to look at camera (or any other assigned object)?")]
	public bool ChatBoxLooksAtCamera = true;
	[Tooltip("If you want to force the Chatbox to look at camera during dialogue (or at any other object), assign the ChatBoxTransform here. It's generally better to assign the whole Canvas here (set to World Space).")]
	public Transform ChatBoxTransform;
	[Tooltip("Set the Text UI which will show the custom Skip Prompt text ([Press Something To Skip], [Continue - SPACE] etc.). You can also remotely find it by tag below.")]
	public Text SkipPrompt;
	[Tooltip("You can also find the Skip Prompt by it's unique Tag here.")]
	public string FindSkipPromptByTag;
	[Tooltip("The content of your skip prompt.")]
	public string SkipPromptTextContent = "Press <Skip Input> to continue";
	
	[Header("Misc Variables")]
	[Tooltip("Time delay in seconds between the end of the dialogue and the execution of features marked as On Dialogue End. Purely for cosmetic purpose, because things look better when they happen a short moment after the dialogue window disappears. :) ")]
	public float DialogueEndDelay = 0.50f;
	[Tooltip("This object is the one which holds the UI Canvas of Chatbox. This gameObject can be turned on and off when needed in a few other features of this system (like on Dialogue start and end).")]
	public GameObject ChatBoxObject;
	[Tooltip("Do you want to disable Chat Box when the dialogue ends and the above delay passes?")]
	public bool DisableChatBoxAfterDialogueEndDelay = true;
	[Tooltip("If you wish for the player to be able to skip each dialogue line with a button, set it's Input name here (from Input Manager). If you don't want him to be able to skip, just leave this field blank.")]
	public string SkipButton = "Skip";
	[Tooltip("If you plan on using a Trigger Box, set the chosen AKTOR Trigger Box here.")]
	public AKTOR_TriggerBox TriggerBox;
	[Tooltip("If you are using Interaction Prompt of trigger box, you might want to reenable it after the dialogue is over. Otherwise NPC will be silent after the end of first dialogue and the player won't be able to interact with it again.")]
	public bool ReactivateInteractionPromptOnDialogueEnd = true;
	
	

	//Work Variables
	[HideInInspector]
	public int DialogueStep;
	[HideInInspector]
	public bool Focusing;
	[HideInInspector]
	public bool Defocusing;
	[HideInInspector]
	public bool ChatBoxActive;
	
	private Vector3 OriginalCameraLocalPosition;
	private Vector3 OriginalPlayerLocalPosition;
	private Transform OriginalCameraParent;
	private Transform OriginalPlayerParent;
	private Quaternion OriginalCameraLocalRotation;
	private Quaternion OriginalPlayerLocalRotation;
	private Quaternion OriginalNPCLocalRotation;
	private Quaternion TempQuaternion;
	private Vector3 TempFocusDirection;
	private Vector3 TempPosition;
	private bool EndDialogueSequence;
	private float t;
	private bool AllSequencesClear = true;
	private float SkipTimer;
	
	
	void Start()
	{
		AllSequencesClear= true;
		if(string.IsNullOrEmpty(FindChatBoxContentByTag) == false)
		{
			ChatBoxContent = GameObject.FindWithTag(FindChatBoxContentByTag).GetComponent<Text>();
		}
		if(string.IsNullOrEmpty(FindNPCNameLabelByTag) == false)
		{
			NPCNameLabel = GameObject.FindWithTag(FindNPCNameLabelByTag).GetComponent<Text>();
		}
		if(string.IsNullOrEmpty(FindSkipPromptByTag) == false)
		{
			SkipPrompt = GameObject.FindWithTag(FindSkipPromptByTag).GetComponent<Text>();
		}
		if(string.IsNullOrEmpty(FindNPCFocusPointByTag) == false)
		{
			NPCFocusPoint = GameObject.FindWithTag(FindNPCFocusPointByTag).transform;
		}
		
		
	}
	
	void OnEnable()
	{
			
			if(Cam == null && string.IsNullOrEmpty(FindCameraByTag) == false )
			{
				Cam = GameObject.FindWithTag(FindCameraByTag).transform;
			}
			if(Player == null && string.IsNullOrEmpty(PlayerTag) == false )
			{
				Player = GameObject.FindWithTag(PlayerTag).transform;
			}
			if(string.IsNullOrEmpty(FindNPCFocusPointByTag) == false)
		{
			NPCFocusPoint = GameObject.FindWithTag(FindNPCFocusPointByTag).transform;
		}
			
			//Let's make sure that Chat Box is not enabled right off the bat.
			if(ChatBoxAnimation != null)
			{
			ChatBoxAnimation.Play("ChatBox_Off");
			}
			//And let's reset the Dialogue Steps. Better safe than sorry!
			DialogueStep = 0;
	}
	
	
	
	
	
	public void StartDialogue()
	{
		if(AllSequencesClear == true)
		{
			
		//Let's start the dialogue
		if(ChatBoxObject != null)
		{
			ChatBoxObject.SetActive(true);
		}
		if(TriggerBox != null)
		{
			TriggerBox.ChatActive = true;
		}
		if(NPCNameLabel != null)
		{
		NPCNameLabel.text = NPCName;
		}
		if(ChatBoxAnimation != null)
		{
		ChatBoxAnimation.Play("ChatBox_Intro");
		}
		ChatBoxActive = true;
		Focus();
		ContinueDialogue();
		
		//And let's call all the features on dialogue start!						
		for (int i = 0; i< CurrentScreenplay.EnableOnDialogueStart.Length; i++)
		{
			CurrentScreenplay.EnableOnDialogueStart[i].SetActive(true);
		}
		for (int ii = 0; ii< CurrentScreenplay.DisableOnDialogueStart.Length; ii++)
		{
			CurrentScreenplay.DisableOnDialogueStart[ii].SetActive(false);
		}
		
		for (int n = 0; n< CurrentScreenplay.SendFunctionOnDialogueStart.Length; n++)
		{
			
			CurrentScreenplay.SendFunctionOnDialogueStart[n].SendMessage("DialogueStart",SendMessageOptions.DontRequireReceiver);
		}
		
		if(SkipPrompt != null)
		{
			SkipPrompt.text = SkipPromptTextContent;
		}
		
		}
		AllSequencesClear= false;
		
	}
	
	
	
	
	public void ContinueDialogue()
	{
		
		
		if(SingleRandomResponse == true)
		{
				DialogueStep = Random.Range(0,CurrentScreenplay.Responses.Length);
				
		}
		
		
		if(NPCAnimations != null)
		{
			if( DialogueStep < CurrentScreenplay.AnimationStateNames.Length )
			{
				if(CurrentScreenplay.AnimationStateNames[DialogueStep] != null && string.IsNullOrEmpty(CurrentScreenplay.AnimationStateNames[DialogueStep]) == false )
				{
				NPCAnimations.Play(CurrentScreenplay.AnimationStateNames[DialogueStep]);
				}
			}
		}
		
		if(DialogueStep < CurrentScreenplay.Responses.Length && ChatBoxContent != null)
		{			
			ChatBoxContent.text = CurrentScreenplay.Responses[DialogueStep];			
				
		}
		if(DialogueStep < CurrentScreenplay.ResponseVoiceLines.Length && Sound != null && CurrentScreenplay.ResponseVoiceLines[DialogueStep] != null)
				{
					Sound.Stop();
					Sound.clip = CurrentScreenplay.ResponseVoiceLines[DialogueStep];
					Sound.Play();
				}
		if(DialogueStep >= (CurrentScreenplay.Responses.Length))
		{
			EndDialogue();
		}
		else
		{
		DialogueStep += 1;
		}
	}
	
	public void EndDialogue()
	{
		
		//Let's end the dialogue
		if(Sound != null)
		{
		Sound.Stop();
		}
		if(ChatBoxAnimation != null)
		{
		ChatBoxAnimation.Play("ChatBox_Outro");
		}
		if(ChatBoxContent != null)
		{
		ChatBoxContent.text = "";
		}
		if(SkipPrompt != null)
		{
			SkipPrompt.text = "";
		}
		if(NPCNameLabel != null)
		{
			NPCNameLabel.text = "";
		}
		ChatBoxActive = false;
		if(TriggerBox != null)
		{
			TriggerBox.ChatActive = false;
		}
		
		Defocus();
		DialogueStep = 0;
		
		EndDialogueSequence = true;
		
	}
	
	
	
	public void Focus()			
	{
		if(Cam != null)
		{
				OriginalCameraLocalPosition = Cam.localPosition;
				OriginalCameraLocalRotation = Cam.localRotation;
				OriginalCameraParent = Cam.parent;
				if(CameraPositionOnFocus != null)
				{
					Cam.parent = null;
				}				
		}
		if(Player != null)
		{
				OriginalPlayerLocalPosition = Player.localPosition;
				OriginalPlayerLocalRotation = Player.localRotation;
				OriginalPlayerParent = Player.parent;
				if(PlayerPositionOnFocus != null)
				{
					Player.parent = null;
				}
		}
		if(NPCFocusingObject != null)
		{
			OriginalNPCLocalRotation = NPCFocusingObject.localRotation;
		}
		


		Focusing = true;
	}
	
	
	public void Defocus()
	{
		Defocusing = true;
		Focusing = false;
	}
	
	
	
	
	void Update()
	{
		//if player presses a button to Skip to the next text.
		if(ChatBoxActive == true && string.IsNullOrEmpty(SkipButton) == false)
		{
			
				if(Input.GetButtonDown(SkipButton))
				{
					if(SingleRandomResponse == false)
					{
						ContinueDialogue();
					}
					if(SingleRandomResponse == true)
					{
						EndDialogue();
					}
				}
		}
		
		if(ChatBoxActive == true && CurrentScreenplay.AutoSkipLinesAfterTime == true )
		{
			if(DialogueStep <= CurrentScreenplay.ResponseDuration.Length)
			{
				
			SkipTimer += Time.deltaTime;
				
			
			
				
				if(SkipTimer >= (CurrentScreenplay.ResponseDuration[DialogueStep-1] ))
					{
						if(SingleRandomResponse == false)
						{
						SkipTimer = 0;
						ContinueDialogue();
						}
						if(SingleRandomResponse == true)
						{
							SkipTimer = 0;
							EndDialogue();
						}
					}
				}
			
			
			
		
		}
	}
	
	
	
		
	void LateUpdate()
	{
		
		//Focus features! - in LateUpdate to not clog the  generaly more important Update in most of game projects.
		if(Focusing == true)
		{
			if(Cam != null && CameraPositionOnFocus!= null)
			{
				Cam.position = Vector3.Lerp(Cam.position, CameraPositionOnFocus.position, Time.deltaTime * CameraMoveToFocusPositionSpeed);
			}
			if(Cam != null && CameraFocusPoint!= null)
			{
				TempQuaternion = Quaternion.LookRotation(CameraFocusPoint.position - Cam.position);
				Cam.rotation = Quaternion.Lerp(Cam.rotation, TempQuaternion, CameraFocusSpeed * Time.deltaTime);	
			}
			
			
			
			if(Player != null && PlayerPositionOnFocus!= null)
			{
				if(OnlyHorizontalPlayerPush)
				{
					TempPosition.x = PlayerPositionOnFocus.position.x;
					TempPosition.y = Player.position.y;
					TempPosition.z = PlayerPositionOnFocus.position.z;
					Player.position = Vector3.Lerp(Player.position, TempPosition, Time.deltaTime * PlayerMoveToFocusPositionSpeed);
				}
				if(OnlyHorizontalPlayerPush == false)
				{
					Player.position = Vector3.Lerp(Player.position, PlayerPositionOnFocus.position, Time.deltaTime * PlayerMoveToFocusPositionSpeed);
				}
			}
			if(Player != null && PlayerFocusPoint!= null)
			{
				if(OnlyHorizontalPlayerFocus == true )
				{	
					TempPosition = PlayerFocusPoint.position;
					TempPosition.y = Player.position.y;
					TempQuaternion = Quaternion.LookRotation(TempPosition - Player.position);
					Player.rotation = Quaternion.Lerp(Player.rotation, TempQuaternion, PlayerFocusSpeed * Time.deltaTime);	
				}
				if(OnlyHorizontalPlayerFocus == false)
				{
				TempQuaternion = Quaternion.LookRotation(PlayerFocusPoint.position - Player.position);
				Player.rotation = Quaternion.Lerp(Player.rotation, TempQuaternion, PlayerFocusSpeed * Time.deltaTime);	
				}
			}
			if(NPCFocusPoint != null && NPCFocusingObject != null)
			{
				if(OnlyHorizontalNPCFocus == true )
				{	
					TempPosition = NPCFocusPoint.position;
					TempPosition.y = NPCFocusingObject.position.y;
					TempQuaternion = Quaternion.LookRotation(TempPosition - NPCFocusingObject.position);
					NPCFocusingObject.rotation = Quaternion.Lerp(NPCFocusingObject.rotation, TempQuaternion, NPCFocusSpeed * Time.deltaTime);	
				}
				if(OnlyHorizontalNPCFocus == false)
				{
				TempQuaternion = Quaternion.LookRotation(NPCFocusPoint.position - NPCFocusingObject.position);
				NPCFocusingObject.rotation = Quaternion.Lerp(NPCFocusingObject.rotation, TempQuaternion, NPCFocusSpeed * Time.deltaTime);	
				}
			}
			
			
		}
		
		
		
		if(Defocusing == true)				
		{									
			
				
			if(Player!=null )
			{
				Player.parent = OriginalPlayerParent;
			}
			if(Player != null && PlayerPositionOnFocus!= null)
			{
				if(ReturnPlayerToPreviousPositionAfterDialogue == true)
				{
					Player.localPosition = Vector3.Lerp(Player.localPosition, OriginalPlayerLocalPosition, Time.deltaTime * PlayerMoveToFocusPositionSpeed);
				}
			}
			if(Player != null && PlayerFocusPoint!= null)
			{
				Player.localRotation = OriginalPlayerLocalRotation;
			}
			
			
			if(NPCFocusingObject!= null && NPCFocusPoint!= null && ReturnNPCToOriginalRotationAfterDialogue == true)
			{
				NPCFocusingObject.localRotation = OriginalNPCLocalRotation;
			}
			
			
			
			
			
				if(Cam != null)
			{
				Cam.parent = OriginalCameraParent;
			}
			if(Cam != null && CameraPositionOnFocus!= null)
			{
				Cam.localPosition = OriginalCameraLocalPosition;
			}
			if(Cam != null && CameraFocusPoint!= null)
			{
				Cam.localRotation = OriginalCameraLocalRotation;
			}
			
			//Let's also make the Chatbox look at the camera also when it's getting defocused.
		if(ChatBoxLooksAtCamera == true && ChatBoxTransform != null)
		{	
			ChatBoxTransform.LookAt(Cam);
		}
			
			Defocusing = false;
			
		}
		
		
		// Should the chatbox follow the camera when it's active?
		if(ChatBoxActive == true && ChatBoxLooksAtCamera == true && ChatBoxTransform != null)
		{	
			ChatBoxTransform.LookAt(Cam);
		}
		
		
		if(EndDialogueSequence == true)
		{
			t+=Time.deltaTime;
			if(t >= DialogueEndDelay)
			{
				//And let's call all the On Dialogue End features
				for (int iii = 0; iii< CurrentScreenplay.EnableOnDialogueEnd.Length; iii++)
				{
					CurrentScreenplay.EnableOnDialogueEnd[iii].SetActive(true);
				}
				for (int iiii = 0; iiii< CurrentScreenplay.DisableOnDialogueEnd.Length; iiii++)
				{
					CurrentScreenplay.DisableOnDialogueEnd[iiii].SetActive(false);
				}
		
				for (int nn = 0; nn< CurrentScreenplay.SendFunctionOnDialogueEnd.Length; nn++)
				{
					CurrentScreenplay.SendFunctionOnDialogueEnd[nn].SendMessage("DialogueEnd",SendMessageOptions.DontRequireReceiver);
				}
		
		
				if(FinalScreenplay != null)
				{
					CurrentScreenplay = FinalScreenplay;
				}
		
				if(ReactivateInteractionPromptOnDialogueEnd == true)
				{
					TriggerBox.EnableInteractionPrompt();
				}
				t=0;
				
				if(DisableChatBoxAfterDialogueEndDelay == true)
				{
					if(ChatBoxObject == null)
					{
						Debug.LogWarning("Umm, pardon me but I see that you wish to disable the chat box on dialogue end with NPC named "+ NPCName+ " but the ChatBoxObject is not set. I can't disable it. :( Either set ChatBoxObject or set DisableChatBoxAfterDialogueEndDelay to false. Or ignore this warning, no harm done. :)");
					}
					if(ChatBoxObject != null)
					{
						ChatBoxObject.SetActive(false);
					}
				}
				EndDialogueSequence = false;
				AllSequencesClear = true;
			}
			
		}
	}
	
	
	
	
	public void SetNewScreenplay(int number)
	{
		CurrentScreenplay = OtherScreenplays[number];
	}
	
	
	
	
}