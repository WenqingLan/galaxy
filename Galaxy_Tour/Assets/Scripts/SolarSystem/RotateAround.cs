using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

	public Transform target; // the object to rotate around
	public int speed; // the speed of rotation
	
	void Start() {
		if (target == null) 
		{
			target = this.gameObject.transform;
			Debug.Log ("RotateAround target not specified. Defaulting to parent GameObject");
		}
		if (string.Equals(target.name, "SunSphere")){
			var go2 = new GameObject { name = "Circle2" };
			go2.transform.position = new Vector3(0, 0, 0);
			Debug.Log(this.gameObject.transform.position.x);
			go2.DrawCircle(Mathf.Abs(this.gameObject.transform.position.x), 0.2f);
		}
		
	}

	// Update is called once per frame
	void Update () {
		// RotateAround takes three arguments, first is the Vector to rotate around
		// second is a vector that axis to rotate around
		// third is the degrees to rotate, in this case the speed per second
		transform.RotateAround(target.transform.position,target.transform.up,speed * Time.deltaTime);
	}

}
