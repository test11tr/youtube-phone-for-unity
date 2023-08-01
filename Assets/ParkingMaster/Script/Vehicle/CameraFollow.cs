using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using test11.Managers;

public class CameraFollow : MonoBehaviour {

	public LevelManager _levelManager;
	[Range(1, 10)]
	public float followSpeed = 2;
	[Range(1, 10)]
	public float lookSpeed = 5;
	Vector3 initialPlayerPosition;
	public Vector3 offsetIsoCamera = new Vector3(-12.5799999f,14.6999998f,15.8599997f);

	void Start(){
		if (_levelManager == null)
		{
			_levelManager = GameObject.FindGameObjectWithTag("Level").transform.GetComponent<LevelManager>();
		}
		initialPlayerPosition = _levelManager.SpawnedPlayerVehicle.transform.position;		

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void FixedUpdate()
	{
		Vector3 Target;
		Vector3 currentOffset;

		Target = _levelManager.SpawnedPlayerVehicle.transform.position;
		
		currentOffset = offsetIsoCamera;
		//Look at car
		Vector3 _lookDirection = - currentOffset;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
		//Move to car
		Vector3 _targetPos = currentOffset + Target;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
		
	}
}