using UnityEngine;
using UnityEditor;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public int _index;  //Index for the Selected type of Waypoint(Start,Middle,End)
	public float _ApproachSpeed;  //Speed in which the camera approaches
	public float _RotateSpeed;  //Speed in which the camera will rotate
	public float _TransitionDistance;  //The Distance between the Camera and the Waypoint at which it moves to another waypoint
	
	public bool UseLookAtObject;  //Override the default and look at another object
	public GameObject LookAtObject;  //Look at Object
	
	public bool UseConstantSpeed;  //Determine which type of movement we want to use (Lerp or Move Towards)
}
