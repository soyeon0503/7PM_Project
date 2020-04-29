using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(PathController))] 
[CanEditMultipleObjects]
public class PathControllerEditor : Editor {
	
	public override void OnInspectorGUI ()
	{
		try{
			//Setup the Main Target
			PathController PC = (PathController)target;
			//Setup the Basic Objects we need to find
			PC.MainCamera = EditorGUILayout.ObjectField ("Main Camera", PC.MainCamera, typeof(Camera), true) as Camera;
			
			//Expanded Bool
			PC.isExpanded = EditorGUILayout.Foldout (PC.isExpanded, "Waypoints");
			//Showing the LookAtPoints Controls
			if (PC.isExpanded) {
				//Indent
				EditorGUI.indentLevel++;
				EditorGUI.indentLevel++;
				//Array Size Input
				PC.WaypointsSize = EditorGUILayout.IntField ("Size", PC.WaypointsSize);
				//Sets the Array based on size
				if (PC.Waypoints.Length != PC.WaypointsSize) {
					Transform[] newArray = new Transform[PC.WaypointsSize];	
					for (int i = 0; i < PC.WaypointsSize; i++) {
						if (PC.Waypoints.Length > i) {
							newArray [i] = PC.Waypoints[i];
						}
					}
					PC.Waypoints = newArray;
				}
			
				//For each Look At Point in the array
				for (int x = 0; x < PC.Waypoints.Length; x++) {
					//Show the Look At Object Selector
					PC.Waypoints [x] = EditorGUILayout.ObjectField ("Waypoint " + (x + 1), PC.Waypoints [x], typeof(Transform), true) as Transform;		
					//Get the Distance between this waypoint and the next
					if((x + 1) < PC.Waypoints.Length && PC.Waypoints [x + 1] != null){
						EditorGUILayout.LabelField("Inbetween Distance:", Vector3.Distance(PC.Waypoints[x].transform.position, PC.Waypoints[x+1].transform.position).ToString());
					}
				}
			}
			
			//Set Dirty for save
			if(GUI.changed){
				//Debug.Log("Dirty");
				EditorUtility.SetDirty(PC);	
			}
		}catch(Exception ex){
			//TRY CATCH	
			Debug.Log("Error: " + ex);
		}
	}
	
	//Scene GUI
	void OnSceneGUI(){
		try{
			//Setup the Main Target
			PathController PC = (PathController)target;
			
			//For each Look At Point in the array
			for (int x = 0; x < PC.Waypoints.Length; x++) {
				if ((x + 1) < PC.Waypoints.Length && PC.Waypoints [x + 1] != null) {
					//Draw lines between points
					Handles.color = Color.blue;
					Handles.DrawLine(PC.Waypoints[x].position, PC.Waypoints[x+1].position);
				}
			}
		}catch(Exception ex){
			//TryCatch	
			Debug.Log("Error: " + ex);
		}
		
	}
	
	
	
}
