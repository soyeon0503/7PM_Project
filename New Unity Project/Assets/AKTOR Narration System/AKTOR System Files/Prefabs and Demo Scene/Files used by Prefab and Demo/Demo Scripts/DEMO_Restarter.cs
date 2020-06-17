using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEMO_Restarter : MonoBehaviour {

	public void DialogueEnd()
	{
		SceneManager.LoadScene(0);
	}
}
