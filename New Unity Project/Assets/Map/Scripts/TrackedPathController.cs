using UnityEngine;
using System.Collections;

public class TrackedPathController : MonoBehaviour
{
	
	/*NOTES:
	 * In this path controller, we are going to be looking at different LAOs between Waypoints.
	 * Therefore we need to specify where in the distance path we want to look at these LOAs.
	 * In most cases this will be a linear process from point A to point B
	 */
	
	public Camera MainCamera;
	public Transform StartPoint;
	public Transform EndPoint;
	public bool UseConstantSpeed;
	public bool UseLookAtObjects;
	public GameObject[] LookPoints;
	public int LookPointSize;
	public float TransistionError;
	public float _Distance;
	public bool isExpanded;
	
	void OnGUI ()
	{
		if (GUI.Button (new Rect ((Screen.width / 2) - 125, (Screen.height / 2) - 25, 250, 50), "Start Path")) {
			Animate ();	
		}
		
	}
	
	void Start ()
	{
		MainCamera.transform.position = StartPoint.position;
		_Distance = Vector3.Distance (MainCamera.transform.position, EndPoint.position);
	}
	
	//Public Function to start path animation
	public void Animate ()
	{
		if (LookPointSize == 0 || UseLookAtObjects == false) {
			StartCoroutine (BeginPathWithoutLAO ());	
		} else {
			if(LAOsValid()){
				StartCoroutine (BeginPath ());	
			}else{
				Debug.Log("WARNING : Please check to make sure all your LAOs are setup in the inspector.  Check your Tracked Path Controller LAOs Objects!");
			}
		}
	}
	
	IEnumerator BeginPath ()
	{
		//Set the Current Distance from the Camera to the Second WayPoint
		_Distance = Vector3.Distance (MainCamera.transform.position, EndPoint.position);
					
		//Get the Transistion Distance for the Waypoint
		float _TransDistance = EndPoint.GetComponent<Waypoint> ()._TransitionDistance;
			
		//Get the Approach Speed
		float _ApproachSpeed = EndPoint.GetComponent<Waypoint> ()._ApproachSpeed;
			
		//Setup the Current LAO
		GameObject _LAO = new GameObject ();
				
				
		//While the camera moves forward
		while (_Distance > _TransDistance) {
					
			//Update Distance
			_Distance = Vector3.Distance (MainCamera.transform.position, EndPoint.position);
					
			//Find the point we should be looking at based on distance
			//Remember, we are going down in distance.
			foreach (GameObject Point in LookPoints) {
				if (Point.GetComponent<LookAtObject> ()._StartDistance > _Distance && _Distance > Point.GetComponent<LookAtObject> ()._EndDistance) { 
					_LAO = Point;
					_LAO.name = Point.name;
				}
			}
			
			
			if (_LAO.name == "New Game Object") {
				Debug.Log ("WARNING : Look At Object is NULL : Check the Starting and Ending Distance");
				yield return new WaitForSeconds(.01f);
				break;
			} else {
			
				//Get some Vars off the Look At Point
				float _RotationSpeed = _LAO.GetComponent<LookAtObject> ()._RotationSpeed;
					
				//Rotate the Camera
				Debug.DrawLine (MainCamera.transform.position, _LAO.transform.position, Color.cyan);
				var rotation = Quaternion.LookRotation (_LAO.transform.position - MainCamera.transform.position);
				MainCamera.transform.rotation = Quaternion.Slerp (MainCamera.transform.rotation, rotation, Time.deltaTime * _RotationSpeed);
					
				//Move the Camera towards the next waypoint
				Debug.DrawLine (MainCamera.transform.position, EndPoint.transform.position, Color.blue);
				_ApproachSpeed = EndPoint.GetComponent<Waypoint> ()._ApproachSpeed;
				
				if (UseConstantSpeed) {
					//This will keep the camera at a constant speed as it reaches the waypoint
					MainCamera.transform.position = Vector3.MoveTowards (MainCamera.transform.position, EndPoint.position, Time.deltaTime * _ApproachSpeed);
				} else {
					//This will slow the camera down as it reaches the waypoint
					MainCamera.transform.position = Vector3.Lerp (MainCamera.transform.position, EndPoint.position, Time.deltaTime * _ApproachSpeed);
				}
				
				yield return new WaitForSeconds(.01f);	
			}	
		}
	}
	
	//Do not use the LAOs, just move the camera from point to point
	IEnumerator BeginPathWithoutLAO ()
	{
		//Set the Current Distance from the Camera to the Second WayPoint
		_Distance = Vector3.Distance (MainCamera.transform.position, EndPoint.position);
					
		//Get the Transistion Distance for the Waypoint
		float _TransDistance = EndPoint.GetComponent<Waypoint> ()._TransitionDistance;
			
		//Get the Approach Speed
		float _ApproachSpeed = EndPoint.GetComponent<Waypoint> ()._ApproachSpeed;
			
		while (_Distance > _TransDistance) {
			//Move the Camera towards the next waypoint
			Debug.DrawLine (MainCamera.transform.position, EndPoint.transform.position, Color.blue);
			_ApproachSpeed = EndPoint.GetComponent<Waypoint> ()._ApproachSpeed;
				
			if (UseConstantSpeed) {
				//This will keep the camera at a constant speed as it reaches the waypoint
				MainCamera.transform.position = Vector3.MoveTowards (MainCamera.transform.position, EndPoint.position, Time.deltaTime * _ApproachSpeed);
			} else {
				//This will slow the camera down as it reaches the waypoint
				MainCamera.transform.position = Vector3.Lerp (MainCamera.transform.position, EndPoint.position, Time.deltaTime * _ApproachSpeed);
			}
			yield return new WaitForSeconds(.01f);	
		}
	}
	
	//Validate the LAOs
	private bool LAOsValid(){
		bool ret = true;
		
		foreach(GameObject Obj in LookPoints){
			if(Obj == null){
				ret = false;
			}
		}
		
		return ret;
	}
	
}
