using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text startText;
	public float speed;
	Color flashingColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		flashingColor.a = Mathf.PingPong (Time.time * speed, 1.0f);

		startText.material.color = flashingColor;
			//.SetColor("Text_Start", flashingColor);
	
	}
}
