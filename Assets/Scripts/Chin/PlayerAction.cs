using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	public float speed = 5.0f;

	public Rigidbody rb;
	Vector3 moveDirection;

	private GunAction gunAction;
	// Start is called before the first frame update
	void Start()
	{
		//rb.GetComponent<Rigidbody>();
		gunAction = GetComponentInChildren<GunAction>();
	}

	// Update is called once per frame
	void Update()
	{
		gunAction.HandleInput();
	}

	private void FixedUpdate()
	{
		HandleMovement();
		//if (OVRInput.GetDown(OVRInput.))

		// calculate movement direction

		//moveDirection = orientation.forward * z + orientation.right * x;
		//Debug.Log(moveDirection);
		//rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
	}

	Vector2 GetMoveDiretionInput()
	{
		Vector2 moveInputAxis = Vector2.zero;
#if UNITY_EDITOR
		moveInputAxis.x = Input.GetAxis("Horizontal");
		moveInputAxis.y = Input.GetAxis("Vertical");
#else
		moveInputAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
		moveInputAxis = moveInputAxis * 2 / 1;
#endif
		return moveInputAxis;
	}

	void HandleShooting()
	{
		

	}

	void HandleMovement()
	{
		Vector2 moveInputAxis = GetMoveDiretionInput();

		Vector3 moveDirection = Camera.main.transform.forward * moveInputAxis.y + Camera.main.transform.right * moveInputAxis.x;
		moveDirection.y = 0;
		transform.position += moveDirection * speed * Time.fixedDeltaTime;
	}
}
