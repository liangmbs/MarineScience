using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopWindowControl : MonoBehaviour {

	//initialize the window
	public GameObject window;
	//public Renderer button;


	//initialize Button
	public GameObject controler;
	RectTransform rt;

	
	//define if it is open;
	public bool open = false;

    private float homeX;
    public float slideAmount = 555;
    public float slideTime = .6f;
    public float slideTimer = 0;

	void Start() 
    {
		rt = (RectTransform)window.transform;
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());
        float homeX = rt.anchoredPosition.x;
	}

    void Update()
    {
        if (slideTimer < 0)
        {
            slideTimer = 0;
        }
        if (slideTimer > 0)
        {
            slideTimer -= Time.deltaTime;
        }
    }

	void OnGUI(){
		if (open == true) {
			controler.transform.rotation = Quaternion.Euler(0,0,180);
			rt.anchoredPosition = new Vector2
                (Mathf.Lerp(-slideAmount, slideAmount, 1 - (slideTimer / slideTime)),
                 rt.anchoredPosition.y);
		}

		if (open == false) {
			controler.transform.rotation = Quaternion.Euler(0,0,0);
            rt.anchoredPosition = new Vector2
                (Mathf.Lerp(slideAmount, -slideAmount, 1 - (slideTimer / slideTime)),
                 rt.anchoredPosition.y);
		}
	}


	void Controler(){
		open = !open;
        slideTimer = slideTime;
	}

}
