using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement
    //Variables for movement
    public static CharacterController controller;

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
    //Variables for health, battery, stress, stamina, etc.

    //Stamina
    [Header("Stamina Settings")]
    [HideInInspector]public float currentStamina;
    public float maxStamina = 100f;
    public float StaminaFullDrainInSeconds = 20f;
    public StaminaBar staminaBar;

    //Stress
    [Header("Stress Settings")]
    [HideInInspector]public float currentStress;
    public float maxStress = 100f;
    public float timeToMaxStress = 20f;
    public float timeToMinStress = 15f;
    public static bool stressUp = EnemyController.isPlayerDetected;
    public StressMeter stressMeter;

    //Health
    [Header("Health Settings")]
    [HideInInspector]public float currentPlayerHealth;
    public float maxPlayerHealth = 2f;
    
    //Battery
    [Header("Battery Settings")]
    [HideInInspector]public float currentBatteryCharge;
    [HideInInspector]public static int currentBatteries;
    public float maxBatteryCharge = 100f;
    public float batteryLifeInSeconds = 500f;
    public BatteryBar batteryBar;

    #endregion

    #region torch
    //Variables for the flashlight
    public GameObject shakePopUp;
    [HideInInspector]public int torchShakes;
    public int maxTorchShakes = 1;
    public Light Torch;

    public static bool torchOn;
    #endregion

    Camera cam;
    public AudioSource footStepS;

    private void Start()
    {
        cam = Camera.main;
        //Sets the current battery to max at the start of the game
        currentBatteryCharge = maxBatteryCharge;
        currentStamina = maxStamina;
        //Sets the current stress level to min at the start of the game
        stressMeter.SetMinStress(currentStress);
        //Sets the current stamina level to max at the start of the game
        staminaBar.SetMaxStamina(maxStamina);
        //Sets the current health to max at the start of the game
        currentPlayerHealth = maxPlayerHealth;
        //Sets the battery bar to the max battery at the start of the game
        batteryBar.SetMaxBattery(maxBatteryCharge);
        //Sets the torch shakes to max at the start of the game
        torchShakes = maxTorchShakes;
        shakePopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        #region flashlight-battery
        
        //Checks if player presses F, and if the current battery charge is greater than 0. It then toggles the flashlight depending on these factors.
        if (Input.GetButtonDown("Flashlight") && currentBatteryCharge > 0)
        {
            torchOn = !torchOn;
            FindObjectOfType<AudioManager>().Play("torchToggle");
        }

        //Cheks if player presses Q, if the current battery charge is 0, and if the amount of "torch shakes" the player can make at the moment is greater than 0.
        if(currentBatteryCharge == 0 && torchShakes > 0 && Input.GetButtonDown("FlashlightShake"))
        {
            currentBatteryCharge += 10f;
            torchShakes -= 1;
            batteryBar.SetBattery(currentBatteryCharge);
            shakePopUp.SetActive(false);
            FindObjectOfType<AudioManager>().Play("torchShake");
            torchOn = false;   
        }

        //Checks if the player presses R, the current amount of batteries the player has is greater than 0 and if the current charge of the battery is 0.
        if(currentBatteryCharge == 0 && currentBatteries > 0 && Input.GetButtonDown("newBattery"))
        {
            currentBatteryCharge = maxBatteryCharge;
            currentBatteries -= 1;
            torchShakes = maxTorchShakes;
            batteryBar.SetBattery(maxBatteryCharge);
            FindObjectOfType<AudioManager>().Play("flashRecharge");
            torchOn = false;
        }

        if (torchOn)
        {
            Torch.enabled = true;
            currentBatteryCharge -= Time.deltaTime * (100 / batteryLifeInSeconds);
            //Sets the current battery charge level to the battery bar, if the flashlight is turned on.
            batteryBar.SetBattery(currentBatteryCharge);
        }
        else
        {
            Torch.enabled = false;
        }

        //Clamps the battery charge to not go under 0 and not go over 100
        currentBatteryCharge = Mathf.Clamp(currentBatteryCharge, 0, 100);
        
        //If the current battery charge is 0, the flashlights toggles of, and cant be toggled back on. If current battery charge is other than 0, the flashlights works as usual.
        if (currentBatteryCharge == 0)
        {
            //Makes the transition from on to off smoother
            Torch.intensity = Mathf.Lerp(Torch.intensity, 0f, Time.deltaTime * 2);
            //This checks the amount of available torch shakes the player has, and if they're greater than 0, it shows the popup to shake the flashlight.
            if(torchShakes > 0)
            {
                shakePopUp.SetActive(true);
            }
        }
        else
        {
            //Makes the transition from on to off smoother
            Torch.intensity = Mathf.Lerp(Torch.intensity, 1f, Time.deltaTime * 2);
        }
        #endregion
        
        #region stress

        currentStress = Mathf.Clamp(currentStress, 0, 100);

        if (EnemyController.isPlayerDetected)
        {
            currentStress += Time.deltaTime * (100 / timeToMaxStress);
            //Sets the current stress charge level to the stress bar, if the enemy sees the player.
            stressMeter.SetStress(currentStress);
        }
        else 
        {
            currentStress -= Time.deltaTime * (100 / timeToMinStress);
            stressMeter.SetStress(currentStress);
        }

        #endregion
        
        #region stamina

        currentStamina = Mathf.Clamp(currentStamina, 0, 100);

        if (Input.GetButton("Sprint") && x > 0 | z > 0)
        {
            currentStamina -= Time.deltaTime * (100 / StaminaFullDrainInSeconds);
            //Sets the current stress charge level to the stress bar, if the enemy sees the player.
            staminaBar.SetStamina(currentStamina);
        }
        else 
        {
            currentStamina += Time.deltaTime * (100 / StaminaFullDrainInSeconds);
            staminaBar.SetStamina(currentStamina);
        }
        #endregion

        #region health


        #endregion

        //Checks if the player currently is on the ground, and sets the isGrounded boolean to false or true depending on if it is or not.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDisctance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        //Controls sprinting
        if (Input.GetButton("Sprint") && currentStamina > 0)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = 6f;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Jumping
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


   /* private void FixedUpdate()
    {
        playFootSteps();
    }

    private void playFootSteps()
    {
        if (velocity.x > 0.1 && velocity.x < speed + 0.1f)
        {
            footStepS.enabled = true;
            footStepS.loop = true;
            footStepS.pitch = 
        }
        if (velocity.x < 0.1)
        {
            footStepS.enabled = false;
            footStepS.loop = false;
        }
    }*/
}
