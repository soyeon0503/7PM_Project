using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_Play_Animation : MonoBehaviour {


	public string INFO = "This here script just holds a function to play a mecanim animaiton with a function call from another object";
	
	public Animator a;
	public string animationState;
	
	// Update is called once per frame
	public void DialogueEnd() {
		a.Play(animationState);
	}
	public void DialogueStart() {
		a.Play(animationState);
	}
}
