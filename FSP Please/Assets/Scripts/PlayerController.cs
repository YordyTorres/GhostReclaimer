using UnityEngine;
using System.Collections;

//so that when added, it adds PlayerMotor & RigidBody//
[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

//serialize field keeps it public but protected
	[SerializeField]
	private float speed = 5f; //speed of movement//
	[SerializeField]
	private float lookSensitivity = 3f; //mouse look around sensitivity//

	[SerializeField]
	private float thForce = 1000f; //thruster force. for flying//

	private PlayerMotor motor;  //reference to PlayerMotor//

	void Start ()
	{
		motor = GetComponent<PlayerMotor> ();
	}

	void Update ()
	{
		//Calculate movement velocity as a 3D vector//
		float xMov = Input.GetAxisRaw("Horizontal"); //to perform movement on x axis//
		float zMov = Input.GetAxisRaw("Vertical");  //to perform movement on y axis//

		//calculation for which direction the player is moving in each respective axis with (0,0,0) being no movement at all//
		Vector3 movHor = transform.right * xMov;  //to perform movement on x axis//
		Vector3 movVer = transform.forward * zMov; //to perform movement on y axis//


		//final movement vector//
		Vector3 _velocity = (movHor + movVer).normalized * speed;

		//apply movement//
		motor.Move(_velocity);

		//calculate rotation as a 3d vector. For turning around.//
		float yRot = Input.GetAxisRaw("Mouse X"); //for turning left and right//

		Vector3 _rotation = new Vector3 (0f, yRot, 0f) * lookSensitivity;

		//apply rotation//
		motor.Rotate(_rotation);

		//calculate camera rotation as a 3d vector. For turning around.//
		float xRot = Input.GetAxisRaw("Mouse Y"); //for turning left and right//

		Vector3 _cameraRotation = new Vector3 (xRot, 0f, 0f) * lookSensitivity;

		Vector3 _thForce = Vector3.zero;
		//apply camera rotation//
		motor.RotateCamera(_cameraRotation);

		//Calculate the thForce based on player input// 
		if (Input.GetButton ("Jump")) 
		{
			_thForce = Vector3.up * thForce;
		}

		// Apply thruster force//
		motor.ApplyThruster (_thForce);

	}

}