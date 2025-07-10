using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    [SerializeField]
    public float frequency = 2.0f;      //  How often a random platform is chosen
    [SerializeField]
    public float activeTime = 1.0f;     //  How long will the platform operate before it is deactivated
    [SerializeField]
    public float deactiveTime = 1.0f;   //  How long the platform will be deactivated

    private GameObject[] childrenObjects;       //  Platforms or total objects
    private int randomObject;
    private float timer;
    private bool isGenerated;

    private void Start()
    {
        //  Get all gameobject children
        childrenObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenObjects[i] = transform.GetChild(i).gameObject;
        }
        timer = frequency;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            RandomFloor();
            if (isGenerated)
            {
                //  If the platform was alreary deactivated, the reset the time
                timer = frequency;
            }
        }
        else
        {
            //  Wait time to choose another platform
            timer -= Time.deltaTime;
        }
    }

    private void RandomFloor()
    {
        //  Get a random number of the gameobject children
        randomObject = Random.Range(0, childrenObjects.Length);
        //  Get tge component of the platform
        LavaPlatform platform = transform.GetChild(randomObject).GetComponent<LavaPlatform>();

        if (platform)
        {
            //  Check if the platform is busy
            if (!platform.isWorking)
            {
                //  Start the deativation
                StartCoroutine(platform.DeactivePlatform(activeTime, deactiveTime));
                isGenerated = true;
            }
        }
    }
}
