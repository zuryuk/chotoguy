using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat
{
	[SerializeField]
	private BarBehaviour bar;
	private float maxVal;
	public float MaxVal{
		get {
			return maxVal;
		}
		set{
			this.maxVal = value;
			bar.MaxValue = value;
		}
	}
	private float curVal;
	public float Currval{
		get {
			return curVal;}
		set{
			this.curVal = value;
			bar.Value = value;}
	}


}

