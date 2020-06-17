using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKTOR_Screenplay : MonoBehaviour {

	[Tooltip("These are the text lines shown in the Chat Box (or other chosen UI Text object) each time player continues dialogue or when it get's continued automatically. If you wish to make a silent vista or voice-only response (without text), just fill the desired amount of text Responses with white space (single spacebar).")]
	public string[] Responses;
	[Tooltip("Optional Sound files played to each text response. Remember that the number of each sound is connected to the number of each text in order! Also keep in mind that the sounds are an addition to texts. Text responses can work without voice lines, but voice lines can't work without texts. If you wish to have voice lines only, fill the desired amount of Text Responses with a white space (single spacebar).")]
	public AudioClip[] ResponseVoiceLines;
	[Tooltip("You can force the dialogue lines to skip automatically after certain amounts of time (so the player doesn't need to click through dialogue manually). You can set each Time for each Response line below, in Response Duration field. Handy if you want for example only voice-over responses which happen one by one automatically.")]
	public bool AutoSkipLinesAfterTime;
	[Tooltip("If you want dialogues to continue to the next line automatically after certain time, set how long each responseline should take before it automatically get's to the next one (or get's to the dialogue end)")]
	public float[] ResponseDuration;
	[Tooltip("You can play individual NPC animation on each Response. You can set the names of animation states for the NPC Animator (assigned in AKTOR_System of the given NPC). Each animation corresponds to a Response of given number.")]
	public string[] AnimationStateNames;
	[Header("Features on Dialogue Start")]
	[Tooltip("Enable or disable any objects you want on Dialogue Start, like shop windows, canvases, npc effects - anything you like. Just assign gameobjects here.")]
	public GameObject[] EnableOnDialogueStart; 
		[Tooltip("Enable or disable any objects you want on Dialogue Start, like shop windows, canvases, npc effects - anything you like. Just assign gameobjects here.")]
	public GameObject[] DisableOnDialogueStart;
	
	[Tooltip("Upon starting the Dialogue, a function called DialogueStart() will be called onto all of these objects automatically. You can use it for your custom functions - like to disable player's control over the character for the length of the dialogue!)")]
	public GameObject[] SendFunctionOnDialogueStart;
	
	[Header("Features on Dialogue End")]
		[Tooltip("Enable or disable any objects you want on Dialogue End, like shop windows, canvases, npc effects - anything you like. Just assign gameobjects here.")]
	public GameObject[] EnableOnDialogueEnd; 
			[Tooltip("Enable or disable any objects you want on Dialogue End, like shop windows, canvases, npc effects - anything you like. Just assign gameobjects here.")]
	public GameObject[] DisableOnDialogueEnd;
	[Tooltip("Upon Ending the Dialogue, a function called DialogueEnd() will be called onto all of these objects automatically. You can use it in any way you wish. This is also your chance to re-enable player's control over the character with your custom scripts!)")]
	public GameObject[] SendFunctionOnDialogueEnd;
	
	
	void Start()
	{
		if(Responses.Length == 0)
		{
			Debug.LogWarning("Excuse me! The object called "+gameObject.name + "seems that has an AKTOR Screenplay script but possesses zero Responses. Remember that if you want an interaction with no text (like a silent vista or a voice-over), the appropriate amount of Responses still must be assigned (for silence, just assign a single spacebar to them). If the zero responses is on purpose though, ignore this warning.");
		}
	}
}
