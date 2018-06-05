using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

	public float walkSpeed = 1;
	public float runSpeed = 1;
	public float turnSpeed = 10;
	public float jumpForce = 100;
	[Range(0, 100)]
	public float gravityMultiplier = 1;
	public Transform camera;
	public Transform groundCheck;
	public LayerMask groundLayer;

	int jumpCount;
	float targetSpeed;
	float vertical;
	float horizontal;
	float mouseX;
	float mouseY;
	bool isGrounded;
	Rigidbody rb;
	
	void Awake()
	{
		Cursor.visible = false;
		Time.fixedDeltaTime = 1f / 125; // set physics clock to 125 Hz
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {

		// default movement speed
		targetSpeed = walkSpeed;

		if (Input.GetKey (KeyCode.LeftShift))
			targetSpeed = runSpeed;

		if (isGrounded && jumpCount >= 1)
			jumpCount = 0;

		if (Input.GetButtonDown ("Jump")) {
			if (jumpCount < 2) {
				rb.AddForce (Vector3.up * jumpForce, ForceMode.VelocityChange);
				jumpCount++;
			}
		}

		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
	}

	void FixedUpdate()
	{
		isGrounded = Physics.CheckSphere (groundCheck.position, groundCheck.localScale.x / 2, groundLayer.value);

		rb.AddForce (Vector3.down * gravityMultiplier);

		// forward/back
		transform.Translate (vertical * Vector3.forward * Time.fixedDeltaTime * targetSpeed);
		// strafe
		transform.Translate (horizontal * Vector3.right * Time.fixedDeltaTime * walkSpeed);
		// horizontal roation
		transform.Rotate (mouseX * Vector3.up * Time.fixedDeltaTime * turnSpeed);
		// vertical camera movement
		camera.Rotate (mouseY * Vector3.right * Time.fixedDeltaTime * -turnSpeed);
	}

	public void Knockout(float delay)
	{
		CancelInvoke ();
		Invoke ("Revive", delay);
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		this.enabled = false;
	}
	
	void Revive()
	{
		this.enabled = true;
		transform.rotation = Quaternion.identity;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
	}
}









