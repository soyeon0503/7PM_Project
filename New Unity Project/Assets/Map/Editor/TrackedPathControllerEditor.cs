using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(TrackedPathController))] 
[CanEditMultipleObjects]
public class TrackedPathControllerEditor : Editor
{
	int Size;
	float Distance;
		
	public override void OnInspectorGUI ()
	{
		try {
			//Setup the Main Target
			TrackedPathController TPC = (TrackedPathController)target;
			//Setup the Basic Objects we need to find
			TPC.MainCamera = EditorGUILayout.ObjectField ("Main Camera", TPC.MainCamera, typeof(Camera), true) as Camera;
			//Starting Waypoint
			TPC.StartPoint = EditorGUILayout.ObjectField ("Starting Waypoint", TPC.StartPoint, typeof(Transform), true) as Transform;
			//Ending Waypoint
			TPC.EndPoint = EditorGUILayout.ObjectField ("Ending Waypoint", TPC.EndPoint, typeof(Transform), true) as Transform;
			//Use Constant Speed
			TPC.UseConstantSpeed = EditorGUILayout.Toggle ("Use Constant Speed", TPC.UseConstantSpeed);
			//Use Look At Objects
			TPC.UseLookAtObjects = EditorGUILayout.Toggle ("Use Look At Objects", TPC.UseLookAtObjects);
		
			if (TPC.UseLookAtObjects) {
				//Expanded Bool
				TPC.isExpanded = EditorGUILayout.Foldout (TPC.isExpanded, "Look At Objects");
				//Showing the LookAtPoints Controls
				if (TPC.isExpanded) {
					//Indent
					EditorGUI.indentLevel++;
					EditorGUI.indentLevel++;
					//Array Size Input
					TPC.LookPointSize = EditorGUILayout.IntField ("Size", TPC.LookPointSize);
					//Sets the Array based on size
					if (TPC.LookPoints.Length != TPC.LookPointSize) {
						GameObject[] newArray = new GameObject[TPC.LookPointSize];	
						for (int i = 0; i < TPC.LookPointSize; i++) {
							if (TPC.LookPoints.Length > i) {
								newArray [i] = TPC.LookPoints [i];
							}
						}
						TPC.LookPoints = newArray;
					}
			
					//For each Look At Point in the array
					for (int x = 0; x < TPC.LookPoints.Length; x++) {
						//Show the Look At Object Selector
						TPC.LookPoints [x] = EditorGUILayout.ObjectField ("Look At Object " + (x + 1), TPC.LookPoints [x], typeof(GameObject), true) as GameObject;	
						//Check to see if we are out of index and if the next point is available
						if ((x + 1) < TPC.LookPoints.Length && TPC.LookPoints [x + 1] != null) {
							//If the End Distance matches the Start Position, show it in the editor					
							if (TPC.LookPoints [x].GetComponent<LookAtObject> ()._EndDistance == TPC.LookPoints [x + 1].GetComponent<LookAtObject> ()._StartDistance) {
								EditorGUILayout.Toggle ("Transition Match", true);
							} else {
								//Debug.Log("WARNING : Ending Distance does NOT match Starting Distance!  Check your transistions!");
								EditorGUILayout.Toggle ("Transition Match", false);
							}
						}
					}
				}
			}
		
			//Undent
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			//Find the distance between the start and end waypoint
			if (TPC.StartPoint != null && TPC.EndPoint != null) {
				Distance = Vector3.Distance (TPC.StartPoint.transform.position, TPC.EndPoint.transform.position);
				EditorGUILayout.LabelField ("Travel Distance", Distance.ToString ());
			}
		
			//Set it to Dirty so we can keep the information saved 
			if (GUI.changed) {
				EditorUtility.SetDirty (TPC);	
			}
		} catch (Exception ex) {
			//TryCatch
			Debug.Log ("Error: " + ex);
		}
	}
	
	
	//Scene View Debugging
	void OnSceneGUI ()
	{
		try {
			TrackedPathController TPC = (TrackedPathController)target;
			//Draw a Line Between Start Point and End Point 
			if (TPC.StartPoint != null && TPC.EndPoint != null) {
				Handles.color = Color.blue;
				Handles.DrawLine (TPC.StartPoint.transform.position, TPC.EndPoint.transform.position);
			}
			
				//Change the handle color
				Handles.color = Color.red;
				//Go through the look points and place a cube on the Camera track where the camera will begin to rotate
				for (int x = 0; x < TPC.LookPoints.Length; x++) {
					if ((x + 1) < TPC.LookPoints.Length && TPC.LookPoints [x + 1] != null) {
						Vector3 CubePos = LerpByDistance (TPC.StartPoint.transform.position, TPC.EndPoint.transform.position, (Distance - TPC.LookPoints [x].GetComponent<LookAtObject> ()._EndDistance));
						Handles.CubeCap (0, CubePos, TPC.transform.rotation, .25f);
					}
				}
			
			
		} catch (Exception ex) {
			//TryCatch
			Debug.Log ("Error: " + ex);
		}
		
	}
	
	//Get the Rotate Position based off the start waypoint, end waypoint, and the distance that is set by the user
	public Vector3 LerpByDistance (Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize (B - A) + A;
		return P;
	}
	

}
