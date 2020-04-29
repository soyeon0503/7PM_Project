using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Waypoint))] 
[CanEditMultipleObjects]
public class WaypointEditor : Editor
{
	//Dropdown Init
	string[] options = new string[3]{"Starting Position", "Middle Position", "Ending Position"};
	
	public override void OnInspectorGUI ()
	{
		try {
			//Get the Class we want to mod
			Waypoint WaypointClass = (Waypoint)target;
			//Create the dropdown menu
			WaypointClass._index = EditorGUILayout.Popup (WaypointClass._index, options);
		
			//Switch for position selected
			switch (WaypointClass._index) {
			case 0:
			//Starting Position
			//Dont forget to set the class as dirty so the settings save
			
				break;
			case 1:
			//Middle positions
				WaypointClass.UseLookAtObject = EditorGUILayout.Toggle ("Use Look At Object", WaypointClass.UseLookAtObject);
				if (WaypointClass.UseLookAtObject) {
					WaypointClass.LookAtObject = EditorGUILayout.ObjectField ("Look At Object", WaypointClass.LookAtObject, typeof(GameObject), true) as GameObject;	
				}
				WaypointClass.UseConstantSpeed = EditorGUILayout.Toggle ("Use Constant Speed", WaypointClass.UseConstantSpeed);
				//WaypointClass._ApproachSpeed = EditorGUILayout.FloatField ("Approach Speed", WaypointClass._ApproachSpeed);
				WaypointClass._ApproachSpeed = EditorGUILayout.Slider("Approach Speed",WaypointClass._ApproachSpeed, 0,25);
				//WaypointClass._RotateSpeed = EditorGUILayout.FloatField ("Rotate Speed", WaypointClass._RotateSpeed);
				WaypointClass._RotateSpeed = EditorGUILayout.Slider("Rotate Speed", WaypointClass._RotateSpeed, 0, 25);
				//WaypointClass._TransitionDistance = EditorGUILayout.FloatField ("Transition", WaypointClass._TransitionDistance);
				WaypointClass._TransitionDistance = EditorGUILayout.Slider("Transition Distance", WaypointClass._TransitionDistance, 0, 100);
			
				break;
			case 2:
			//Ending Position
				WaypointClass.UseLookAtObject = EditorGUILayout.Toggle ("Use Look At Object", WaypointClass.UseLookAtObject);
				if (WaypointClass.UseLookAtObject) {
					WaypointClass.LookAtObject = EditorGUILayout.ObjectField ("Look At Object", WaypointClass.LookAtObject, typeof(GameObject), true) as GameObject;	
				}
				WaypointClass.UseConstantSpeed = EditorGUILayout.Toggle ("Use Constant Speed", WaypointClass.UseConstantSpeed);
				//WaypointClass._ApproachSpeed = EditorGUILayout.FloatField ("Approach Speed", WaypointClass._ApproachSpeed);
				WaypointClass._ApproachSpeed = EditorGUILayout.Slider("Approach Speed",WaypointClass._ApproachSpeed, 0,25);
				//WaypointClass._RotateSpeed = EditorGUILayout.FloatField ("Rotate Speed", WaypointClass._RotateSpeed);
				WaypointClass._RotateSpeed = EditorGUILayout.Slider("Rotate Speed", WaypointClass._RotateSpeed, 0, 25);
				//WaypointClass._TransitionDistance = EditorGUILayout.FloatField ("Transition", WaypointClass._TransitionDistance);
				WaypointClass._TransitionDistance = EditorGUILayout.Slider("Transition Distance", WaypointClass._TransitionDistance, 0, 100);
			
				break;
			}
		
			//If changed, make it dirty for save
			if (GUI.changed) {
				EditorUtility.SetDirty (WaypointClass);
			}	
			
		} catch (Exception ex) {
			Debug.Log ("Error: " + ex);	
		}
			
	}	
}
