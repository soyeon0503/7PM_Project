using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToUsePrefabs : MonoBehaviour {

[TextArea(5,20)]
public string INFO = "Some DEMO Prefabs are using some of the Remotely Find features (to find an existing Game Objects in the scene, outside of the prefab itself, like cinematic subtitles or cinematic blackbars. To ensure that everything works well, it is heavily adviced to run the chosen prefab in the provided Demo Scene first to learn how things work. Opening up such Prefab in a Scene without these required objects in the world will produce errors (because they could not be found). To use these prefabs in your scenes change the Remotely Find features to either empty, or to the objects which exist in your scenes (like your own in-scene subtitles or NPC name object)";
}
