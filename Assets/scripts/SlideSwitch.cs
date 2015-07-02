using UnityEngine;
using System.Collections;

public class SlideSwitch : MonoBehaviour {

    public GameObject panelOn;
    public GameObject panelOff;
    private bool on = true;
    private Animator anim;

	// Use this for initialization
	void Start () {
        panelOff.SetActive(false);
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleSwitch()
    {
        on = !on;
        anim.SetBool("On?", on);
        panelOff.SetActive(!panelOff.activeSelf);
        panelOn.SetActive(!panelOn.activeSelf);
    }
}
