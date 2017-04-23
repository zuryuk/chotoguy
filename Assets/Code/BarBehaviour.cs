using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour {
	private float fillAmount;
	private Image content;
	private float waitTime = 2f;
	public float MaxValue { get; set; }
	public float Value {
		set
		{
			fillAmount = Map (value, MaxValue);
		}
	}
	// Use this for initialization
	void Start () {
		content = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	
	}
	private void HandleBar(){
		if (content.fillAmount > fillAmount) {
			Debug.Log (content.fillAmount);
			Debug.Log (fillAmount);
			content.fillAmount -= (1.0f - fillAmount) / waitTime * Time.deltaTime;
		}
	}
	private float Map(float value, float maxValue){
		return value / maxValue;
	}
}
