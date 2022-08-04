using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	public float speed = 5.0f;

	public Transform orientation;
	public Rigidbody rb;
	Vector3 moveDirection;
	// Start is called before the first frame update
	void Start()
    {
		//rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	
	}

	private void FixedUpdate()
	{
		//if (OVRInput.GetDown(OVRInput.))
		//{

		//}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		//Vector3 moveDirection = Camera.main.transform.forward * z + Camera.main.transform.right * x;
		//transform.position += moveDirection * speed * Time.deltaTime;

		// calculate movement direction
		
		moveDirection = orientation.forward * z + orientation.right * x;
		Debug.Log(moveDirection);
		rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
	}
}
