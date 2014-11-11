using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	//Global Variables

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private	float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate()
	{
		targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
		Debug.DrawLine(follow.position, targetPosition, Color.magenta);


		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);


		transform.LookAt(follow);

		              }
}
