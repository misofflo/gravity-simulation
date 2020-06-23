using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	// VARIABLES/OBJECTS

		//public variables
		public float runningSpeed, walkingSpeed, currentSpeed;
		public float speedSmoothTime = .2f, turnSmoothTime = .2f;
		public float jumpForce = 200, jumpBoost = 300;
		public int obstacleNum;
		public bool toggleRunning = true, running, falling;

		//private variables
		float turnSmoothVelocity, speedSmoothVelocity;
		

		//public GameObjects
		public Transform GroundRayOrigin, FrontRayOrigin;
		public CapsuleCollider[] colliders = new CapsuleCollider[2];
		public Collider[] obstacles;
		
		//private GameObjects
		Rigidbody rb;
		Animator anim;
		Transform cameraTransform;

	// FUNCTIONS

	// start function
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		//anim = GetComponent<Animator>();
		cameraTransform = Camera.main.transform;
		//obstacles = new Collider[obstacleNum];
	}

	// update function
	void Update()
	{
		//falling = RayCastManager.falling;

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		// movement execution
		running = ((toggleRunning && (inputDir.x != 0 || inputDir.y != 0)) ? Running(true) : Running());
		float targetSpeed = ((running) ? runningSpeed : walkingSpeed);
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
		if ((running == false) && inputDir.magnitude == 0)
			currentSpeed = 0;

		// transforming in Space.World
		transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World); 

		/*// setting animation speed
		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
		anim.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

		// jump execution
		if (Input.GetKeyDown(KeyCode.Space)) Jump();
		if (Input.GetKeyDown(KeyCode.LeftControl) && running) Slide(running);
		anim.SetBool("Falling", RayCastManager.falling);*/
	}

	// jump function
	/*void Jump()
	{
		if (RayCastManager.grounded)
		{
			rb.AddForce(Vector3.up * jumpForce);
			rb.AddForce(transform.forward * jumpBoost);
			anim.SetTrigger("jump");
		}
	}*/

	// slide function
	void Slide(bool running)
	{
		anim.SetTrigger("slide");
		//colliders[0].enabled = false;
	}

	// runnning functions
	bool Running()
	{
		return Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0);
	}
	bool Running(bool toggle)
	{
		bool _running;
		_running = ((Input.GetKeyDown(KeyCode.LeftShift)) ? !running : running);
		return _running;
	}
}