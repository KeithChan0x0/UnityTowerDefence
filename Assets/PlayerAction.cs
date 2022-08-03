using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	
	}

	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.W))
		{
			Debug.Log("OK");
			Vector3 moveDirection = Camera.main.transform.forward;
			moveDirection.y = 0.0f;
			transform.position += moveDirection * speed * Time.deltaTime;
		}
	}
}
