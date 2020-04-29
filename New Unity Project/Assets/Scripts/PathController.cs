using UnityEngine;
using UnityEditor;
using System.Collections;

public class PathController : MonoBehaviour
{
	
	public Camera MainCamera;
	public Transform[] Waypoints;
	public bool isExpanded;
	public int WaypointsSize;
		
	void OnGUI ()
	{
		if (GUI.Button (new Rect ((Screen.width / 2) - 125, (Screen.height/2) - 25, 250, 50), "Start Path")) {
			Animate ();	
		}
	}
	
	void Start ()
	{
		if(WaypointsSize > 0){
			//Set the Main Camera to the First Position
			MainCamera.transform.position = Waypoints [0].transform.position;
		}
	}
	
	//Public Function to call
	public void Animate ()
	{
		if(ValidateWaypoints()){
			StartCoroutine (BeginPath ());
		}
	}
	
	IEnumerator BeginPath ()
	{
		foreach (Transform Tran in Waypoints) {
			if(Tran == null){
				Debug.Log("WARNING : Waypoint transform is not setup in path controller.  Please assign a transform to the waypoint!");
				break;
			}
			
			//Get the bool if we are going to use Look at Object
			bool _UseLookAtObj = Tran.GetComponent<Waypoint> ().UseLookAtObject;
			//Get the Rotation Speed for the WayPoint
			float _RotationSpeed = Tran.GetComponent<Waypoint> ()._RotateSpeed;
			//Get the Approach Speed for the Waypoint
			float _ApproachSpeed = Tran.GetComponent<Waypoint> ()._ApproachSpeed;
			//Get the Transistion Distance for the Waypoint
			float _TransDistance = Tran.GetComponent<Waypoint> ()._TransitionDistance;
			if (_TransDistance == 0 || _TransDistance < 0) {
				Debug.Log ("Warning: Transition Distance has to be greater than 0!");
			}
			//Set the Current Distance from the Camera to the Second WayPoint
			float _Distance = Vector3.Distance (MainCamera.transform.position, Tran.position);
				
				
			//While we are away from the next WayPoint
			while (_Distance >= _TransDistance) {
				//Draw the Line between to next Waypoint
				Debug.DrawLine (MainCamera.transform.position, Tran.transform.position, Color.green);
					
				//Recalc the distance between the Camera and Waypoint
				_Distance = Vector3.Distance (MainCamera.transform.position, Tran.position);
					
					
				if (_UseLookAtObj) {
					//Override - If the WayPoint specifies we want to look at a certain object
					Debug.DrawLine (MainCamera.transform.position, Tran.GetComponent<Waypoint> ().LookAtObject.transform.position, Color.cyan);
					var rotation = Quaternion.LookRotation (Tran.GetComponent<Waypoint> ().LookAtObject.transform.position - MainCamera.transform.position);
					MainCamera.transform.rotation = Quaternion.Slerp (MainCamera.transform.rotation, rotation, Time.deltaTime * _RotationSpeed);
				} else {
					//Default - If the WayPoint Specifies we want to Look at the next waypoint
					var rotation = Quaternion.LookRotation (Tran.position - MainCamera.transform.position);
					MainCamera.transform.rotation = Quaternion.Slerp (MainCamera.transform.rotation, rotation, Time.deltaTime * _RotationSpeed);
				}
					
				//Move the Camera to the Next Position
				if (Tran.GetComponent<Waypoint> ().UseConstantSpeed) {
					//This will keep the camera at a constant speed as it reaches the waypoint
					MainCamera.transform.position = Vector3.MoveTowards (MainCamera.transform.position, Tran.position, Time.deltaTime * _ApproachSpeed);
				} else {
					//This will slow the camera down as it reaches the waypoint
					MainCamera.transform.position = Vector3.Lerp (MainCamera.transform.position, Tran.position, Time.deltaTime * _ApproachSpeed);
				}
				yield return new WaitForSeconds(.01f);
			}
		}
	}
	
	//Waypoint Validation
	private bool ValidateWaypoints ()
	{
		if(WaypointsSize == 0){
			Debug.Log("WARNING : No Waypoints assigned! Adjust the size of the waypoints array in the Path Controller!");
			return false;
			//break;
		}
		
		int i = 0;
		foreach (Transform Tran in Waypoints) {
			i++;
			if (Tran.GetComponent<Waypoint> ()._ApproachSpeed < 0) {
				Debug.Log ("WARNING : Approach Speed cannot be negative! Check Waypoint #" + i);
				return false;
				//break;
			}
			if (Tran.GetComponent<Waypoint> ()._RotateSpeed < 0) {
				Debug.Log ("WARNING : Rotation Speed cannot be negative! Check Waypoint #" + i);
				return false;
				//break;
			}
			if (Tran.GetComponent<Waypoint> ()._TransitionDistance < 0) {
				Debug.Log ("WARNING : Transistion Distance cannot be negative! Check Waypoint #" + i);
				return false;
				//break;
			}
			//If the Waypoints Use LAO is set to true and is not the starting position, we want to make sure
			//that the LAO is setup, otherwise, revert to default of looking at the next waypoint
			if (Tran.GetComponent<Waypoint> ().UseLookAtObject && Tran.GetComponent<Waypoint> ()._index != 0) {
				if (Tran.GetComponent<Waypoint> ().LookAtObject == null) {
					Debug.Log ("WARNING : Waypoint #" + i + " has no look at object assigned, reverting to default!");
					Tran.GetComponent<Waypoint> ().UseLookAtObject = false;	
				}
			}
		}
		
		return true;
	}
	
	
	
	
}
