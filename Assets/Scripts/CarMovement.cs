using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour {

    public float movementSpeed = 20.0f;
    public Slider acceleration;
	public Rigidbody rigbod;
    private float declerationRate;

    UIManager ui;

    //Wheel Colliders
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    //Wheel friction variables (used to adjust stiffness)
    private WheelFrictionCurve backLeftWheelFriction;
    private WheelFrictionCurve backRightWheelFriction;
    private WheelFrictionCurve frontLeftWheelFriction;
    private WheelFrictionCurve frontRightWheelFriction;

    //Terrain stiffness
   // public float grassStiffness = 0.2f;
   // public float dirtStiffness = 0.3f;
   // public float gravelStiffness = 0.4f;
   // public float asphaltStiffness = 0.5f;

    //Wheel game objects
    public Transform frontLeftWheelMesh;
	public Transform backLeftWheelMesh;

    //Wheel detction 
    private WheelHit wheelL;
    private WheelHit wheelR;

    //Particle System
    public ParticleSystem backLeftParticles;
    public ParticleSystem backRightParticles;

    // Use this for initialization
    void Start ()
    {
        //Set center of mass slightly below the car
		rigbod.centerOfMass = new Vector3 (0, -1, 0);

        //Set each wheel friction curve to the corrosponding wheel
        backLeftWheelFriction = backLeft.forwardFriction;
        backRightWheelFriction = backRight.forwardFriction;
        frontLeftWheelFriction = frontLeft.forwardFriction;
        frontRightWheelFriction = frontRight.forwardFriction;

        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Temporary restart button
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameLevel");
            acceleration.value = 0;
            movementSpeed = 0;
        }

        //Wheel rotation
        frontLeftWheelMesh.Rotate(frontLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        backLeftWheelMesh.Rotate(backLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        //Sounds
        if (SoundManager.Instance.SFX.isPlaying == false)
        {
            if (acceleration.value == 1)
            {
                SoundManager.Instance.SFX.pitch = 0.7f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 2)
            {
                SoundManager.Instance.SFX.pitch = 0.8f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 3)
            {
                SoundManager.Instance.SFX.pitch = 0.9f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 4)
            {
                SoundManager.Instance.SFX.pitch = 1.0f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 5)
            {
                SoundManager.Instance.SFX.pitch = 1.1f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 6)
            {
                SoundManager.Instance.SFX.pitch = 1.2f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 7)
            {
                SoundManager.Instance.SFX.pitch = 1.3f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 8)
            {
                SoundManager.Instance.SFX.pitch = 1.4f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 9)
            {
                SoundManager.Instance.SFX.pitch = 1.5f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
            else if (acceleration.value == 10)
            {
                SoundManager.Instance.SFX.pitch = 1.6f;
                SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.car);
            }
        }
    }

        void FixedUpdate ()
    {
        Movement();

        // left wheel detection
        backLeft.GetGroundHit(out wheelL);
        backRight.GetGroundHit(out wheelR);

        // end line detection
        if (wheelL.collider.gameObject.tag == "Finish")
        {
            //movementSpeed = 0.0f;
            //acceleration.value = 0.0f;
            ui.timerCheck = false;
        }

        //check point 
        if (wheelL.collider.gameObject.tag == "Check")
        {
            ui.checkPointCheck = true;
        }

        if (backLeft.isGrounded && backRight.isGrounded) 
		{
			//Grass terrain
			if (wheelL.collider.gameObject.tag == "Grass" && acceleration.value >= 2) 
			{
                backLeftWheelFriction.stiffness = -(acceleration.value - 2) / (acceleration.maxvalue - acceleration.minValue) + 1; ;
                backRightWheelFriction.stiffness = -(acceleration.value - 2) / (acceleration.maxvalue - acceleration.minValue) + 1; ;
                frontLeftWheelFriction.stiffness = -(acceleration.value - 2) / (acceleration.maxvalue - acceleration.minValue) + 1; ;
                frontRightWheelFriction.stiffness = -(acceleration.value - 2) / (acceleration.maxvalue - acceleration.minValue) + 1;

                //Green
                backLeftParticles.startColor = Color.green;
                backRightParticles.startColor = Color.green;
            }

			//Dirt terrain
			if (wheelL.collider.gameObject.tag == "Dirt" && acceleration.value >= 4) 
			{
                backLeftWheelFriction.stiffness = -(acceleration.value - 4) / (acceleration.maxvalue - acceleration.minValue) + 1;
                backRightWheelFriction.stiffness = -(acceleration.value - 4) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontLeftWheelFriction.stiffness = -(acceleration.value - 4) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontRightWheelFriction.stiffness = -(acceleration.value - 4) / (acceleration.maxvalue - acceleration.minValue) + 1;

                //Brown
                backLeftParticles.startColor = new Color(135, 70, 70, 1.0f);
                backRightParticles.startColor = new Color(135, 70, 70, 1.0f);
            }

			//Gravel terrain
			if (wheelL.collider.gameObject.tag == "Gravel" && acceleration.value >= 6) 
			{
                backLeftWheelFriction.stiffness = -(acceleration.value - 6) / (acceleration.maxvalue - acceleration.minValue) + 1;
                backRightWheelFriction.stiffness = -(acceleration.value - 6) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontLeftWheelFriction.stiffness =  -(acceleration.value - 6) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontRightWheelFriction.stiffness = -(acceleration.value - 6) / (acceleration.maxvalue - acceleration.minValue) + 1;

                //Grey
                backLeftParticles.startColor = Color.grey;
                backRightParticles.startColor = Color.grey;
            }

            //Asphalt terrain
            if (wheelL.collider.gameObject.tag == "Asphalt" && acceleration.value >= 8)
            {
                backLeftWheelFriction.stiffness = -(acceleration.value - 8) / (acceleration.maxvalue - acceleration.minValue) + 1;
                backRightWheelFriction.stiffness = -(acceleration.value - 8) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontLeftWheelFriction.stiffness = -(acceleration.value - 8) / (acceleration.maxvalue - acceleration.minValue) + 1;
                frontRightWheelFriction.stiffness = -(acceleration.value - 8) / (acceleration.maxvalue - acceleration.minValue) + 1;

                //Black
                backLeftParticles.startColor = Color.black;
                backRightParticles.startColor = Color.black;
            }

            frontRight.forwardFriction = backRightWheelFriction;
            frontLeft.forwardFriction = backLeftWheelFriction;
            backLeft.forwardFriction = backLeftWheelFriction;
            backRight.forwardFriction = backRightWheelFriction;
        }
    }

    void Movement ()
    {
        movementSpeed += acceleration.value;
        declerationRate = movementSpeed / 10;

        //Apply force to rear wheels to simulate rear wheel drive
        backLeft.motorTorque = movementSpeed * Time.deltaTime;
        backRight.motorTorque =movementSpeed * Time.deltaTime;

        if (acceleration.value <= 0 && movementSpeed > 0)
        {
            movementSpeed -= declerationRate;
        }
    }
}