using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    public AudioSource footStepS;

    public float speed =6f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDisctance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public static bool isGrounded;

    public GameObject Torch;

    public static bool torchOn = true;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            torch();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

            }

        }
    }

   


    private void FixedUpdate()
    {
        playFootSteps();
    }

    void torch()
    {
        if (torchOn)
        {
            Torch.SetActive(false);
            torchOn = false;
        }
        else
        {
            Torch.SetActive(true);
            torchOn = true;
        }
        FindObjectOfType<AudioManager>().Play("torchToggle");
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
