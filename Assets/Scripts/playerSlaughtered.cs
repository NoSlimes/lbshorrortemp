using UnityEngine;

public class playerSlaughtered : MonoBehaviour
{
    public static bool attacked;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G)){
            
        }
    }
}
