using UnityEngine;
using System.Collections;

public class RotatedOnGyro : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
    {
        if(SystemInfo.supportsGyroscope)
            transform.eulerAngles = new Vector3(0, 0, Input.acceleration.z);
	}
}
