using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float DirectionDampTime = .25f;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	[SerializeField]
	private float directionalSpeed = 3.0f;

	private float speed = 0.0f;
	private float direction = 0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;


	// Use this for initialization




	void Start () {
		animator = GetComponent<Animator>();

		if(animator.layerCount >= 2)
		{

			animator.SetLayerWeight(1,1);
		}
	}

	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		speed = new Vector2(horizontal, vertical).sqrMagnitude;

		animator.SetFloat("Speed", speed);
		animator.SetFloat("Direction", horizontal, DirectionDampTime, Time.deltaTime);

		StickToWorldSpace(this.transform, gamecam.transform, ref DirectionDampTime, ref speed);
	}


	public void StickToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;

		Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

		speedOut = stickDirection.sqrMagnitude;

		//get camera rotation
		Vector3 CameraDirection = camera.forward;
		CameraDirection.y = 0.0f; //kill?
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

		//convert joystick input in Worldspace coordinates
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);

		float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		angleRootToMove /= 180f;

		directionOut = angleRootToMove * directionalSpeed;

	}


}
