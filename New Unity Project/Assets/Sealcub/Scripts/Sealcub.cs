using UnityEngine;
using System.Collections;

public class Sealcub : MonoBehaviour {
    Animator sealcub;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        sealcub = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.S))
        {
            sealcub.SetBool("idle", true);
            sealcub.SetBool("walk", false);
            sealcub.SetBool("walkleft", false);
            sealcub.SetBool("walkright", false);
            sealcub.SetBool("upside", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            sealcub.SetBool("walk", true);
            sealcub.SetBool("idle", false);
            sealcub.SetBool("walkleft", false);
            sealcub.SetBool("walkright", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            sealcub.SetBool("walkleft", true);
            sealcub.SetBool("walkright", false);
            sealcub.SetBool("walk", false);
            sealcub.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            sealcub.SetBool("walkleft", false);
            sealcub.SetBool("walkright", true);
            sealcub.SetBool("walk", false);
            sealcub.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            sealcub.SetBool("upside", true);
            sealcub.SetBool("idle", false);
            sealcub.SetBool("backward", false);
            sealcub.SetBool("walk", false);
            sealcub.SetBool("walkleft", false);
            sealcub.SetBool("walkright", false);
            StartCoroutine("scratch");
            scratch();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            sealcub.SetBool("backward", true);
            sealcub.SetBool("scratch", false);
            sealcub.SetBool("upside", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            sealcub.SetBool("call", true);
            sealcub.SetBool("idle", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            sealcub.SetBool("die", true);
            sealcub.SetBool("idle", false);
        }
    }
    IEnumerator idle()
    {
        yield return new WaitForSeconds(1.0f);
        sealcub.SetBool("backward", false);
        sealcub.SetBool("call", false);
        sealcub.SetBool("idle", true);
    }
    IEnumerator scratch()
    {
        yield return new WaitForSeconds(1.0f);
        sealcub.SetBool("upside", false);
        sealcub.SetBool("scratch", true);
        sealcub.SetBool("backward", false);
    }
}
