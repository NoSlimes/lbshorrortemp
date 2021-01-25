using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement
    public CharacterController controller;

    public float speed =6f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDisctance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public static bool isGrounded;
    #endregion

    #region Health, Battery, Stress etc.
    public float maxBattery = 100f;
    public float batteryLifeInSeconds = 500f;
    public float currentBattery;
    public float maxPlayerHealth = 2f;
    public float currentPlayerHealth;
    public static int currentBatteries;
    //Battery

    public BatteryBar batteryBar;

    #endregion

    #region torch
    public GameObject shakePopUp;
    public int torchShakes;
    public int maxTorchShakes = 1;
    public Light Torch;

    public static bool torchOn;
    #endregion

    Camera cam;
    public AudioSource footStepS;

    private void Start()
    {
        cam = Camera.main;
        currentBattery = maxBattery;
        currentPlayerHealth = maxPlayerHealth;
        batteryBar.SetMaxBattery(maxBattery);
        torchShakes = maxTorchShakes;
        shakePopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region flashlight
        
        if (Input.GetKeyDown(KeyCode.F) && currentBattery > 0)
        {
            torchOn = !torchOn;
            FindObjectOfType<AudioManager>().Play("torchToggle");
        }

        if(currentBattery == 0 && torchShakes > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            currentBattery += 10f;
            torchShakes -= 1;
            batteryBar.SetBattery(currentBattery);
            shakePopUp.SetActive(false);
            FindObjectOfType<AudioManager>().Play("torchShake");
            torchOn = false;   
        }

        if(currentBattery == 0 && currentBatteries > 0 && Input.GetKeyDown(KeyCode.R))
        {
            currentBattery = 100f;
            currentBatteries -= 1;
            FindObjectOfType<AudioManager>().Play("flashRecharge");
            torchShakes = 1;
            torchOn = false;
        }

        if (torchOn)
        {
            Torch.enabled = true;
            currentBattery -= Time.deltaTime * (100 / batteryLifeInSeconds);
            batteryBar.SetBattery(currentBattery);
        }
        else
        {
            Torch.enabled = false;
        }

        currentBattery = Mathf.Clamp(currentBattery, 0, 100);
        
        if (currentBattery == 0)
        {
            Torch.intensity = Mathf.Lerp(Torch.intensity, 0f, Time.deltaTime * 2);
            if(torchShakes > 0)
            {
                shakePopUp.SetActive(true);
            }
        }
        else
        {
            Torch.intensity = Mathf.Lerp(Torch.intensity, 1f, Time.deltaTime * 2);
        }
        #endregion

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDisctance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = 6f;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        playFootSteps();
    }

    private void playFootSteps()
    {
        if (velocity.x > 0.1 && velocity.x < speed + 0.1f)
        {
            footStepS.enabled = true;
            footStepS.loop = true;
            //footStepS.pitch = 
        }
        if (velocity.x < 0.1)
        {
            footStepS.enabled = false;
            footStepS.loop = false;
        }
    }
}
