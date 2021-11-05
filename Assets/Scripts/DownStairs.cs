using UnityEngine;

public class DownStairs : MonoBehaviour
{
    [SerializeField] public Transform target1;  // The position of the 1st player 
    [SerializeField] public Transform camera1;  // The position of the 1st camera
    [SerializeField] public Transform target2;  // The position of the 2nd player 
    [SerializeField] public Transform camera2;  // The position of the 2nd camera

    private bool isTriggered = false;  // True whenever the player is in a trigger zone

    private void OnTriggerEnter2D(Collider2D collision)  // Turn isTriggered on when entering a trigger zone
    {
        isTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)  // Turn isTriggered off when exiting a trigger zone
    {
        isTriggered = false;
    }
    void Update()
    {
        if (isTriggered == true)  // check if the player is in... WAIT, it's totally bugged
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                target1.transform.position = new Vector3(target1.position.x-5, transform.position.y-5, target1.position.z);
                camera1.transform.position = new Vector3(camera1.position.x-5, transform.position.y-5, camera1.position.z);
                isTriggered = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                target2.transform.position = new Vector3(target2.position.x-5, transform.position.y-5, target2.position.z);
                camera2.transform.position = new Vector3(camera2.position.x-5, transform.position.y-5, camera2.position.z);
                isTriggered = false;
            }
        }
    }
}
