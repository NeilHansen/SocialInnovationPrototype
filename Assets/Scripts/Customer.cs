using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {

    public CustomerMovePath customerPath;
    public GameManager gameManager;
    public bool leaveWhenMeterReachZero = false;

    public Sprite[] currentSituation;
    public Sprite[] sprite;
    public Sprite[] newSituationSprites;
    public Image status;

    public bool isMoving = true;
    private float mass = 1.0f;
    private float speed = 3.0f;
    private bool isLooping = false;
    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;
    private Vector3 playerDirection;
    private Vector3 velocity;

    //Spherecast stuff
    public GameObject currentHitObject;
    private float currentHitDistance;
    public float maxRaycastDistance;
    public float sphereRadius;
    public LayerMask layerMask;
    private Vector3 origin;
    private Vector3 direction;

    //For dialogue
    private SliderCanvas[] playerCanvas;
    private int talkTimes = 0;
    GameManager Gm;

    public enum Attitude
    {
        SuperSad, Sad, Cry,
        Smile, BigSmile, Love,
        Angry, Confused, Surprised,
    }
    
    Attitude currentAttitude;
    Attitude positiveRespond;
    Attitude negativeRespond;

    public float positiveMultiplier = 2.0f;
    public float negativeMultiplier = 0.5f;

    // Use this for initialization
    void Start () {
        Gm = FindObjectOfType<GameManager>();
        customerPath = FindObjectOfType<CustomerMovePath>();
        gameManager = FindObjectOfType<GameManager>();
        playerCanvas = FindObjectsOfType<SliderCanvas>();
        status = FindObjectOfType<Image>();
        pathLength = customerPath.Length;
        curPathIndex = 0;
        velocity = transform.forward;

        if (gameObject.tag == "SpecialCustomer")
        {
            SetUpDefaultAttitude();
            foreach (SliderCanvas sC in playerCanvas)
            {
                sC.positiveButton.onClick.AddListener(PositiveRespond);
                sC.negativeButton.onClick.AddListener(NegativeRespond);
            }
        }
        int rando = Random.Range(0, 2);
        if(rando != 1)
        {
            currentSituation = newSituationSprites;
        }
        else
        {
            currentSituation = sprite;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        CheckSpherecast();
        

        if (gameObject.tag == "SpecialCustomer")
        {
            TriggerDialogue();
            CheckCustomerStatus();
        }
        
        if (currentHitDistance < 2.5f && (currentHitObject.gameObject.tag == "Customer" || currentHitObject.gameObject.tag == "SpecialCustomer"))
            isMoving = false;

        if (currentHitDistance > 2.5f)
            isMoving = true;

        switch (curPathIndex)
        {
            case 1:
                speed = 5.0f;
                break;
            case 2:
                speed = 0.3f;
                break;
            case 3:
                speed = 10.0f;
                break;
        }

        if(isMoving)
            AutoMove();

        if (transform.position.z <= -12.0f)
        {
            if (gameObject.transform.parent == null)
                Destroy(gameObject);
            else
                Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void TriggerDialogue()
    {
        
        foreach (SliderCanvas sC in playerCanvas)
        {
            if(Vector3.Distance(sC.gameObject.transform.position, transform.position) < 5.0f && talkTimes < 2)
            {
                sC.positiveButton.gameObject.SetActive(true);
                sC.negativeButton.gameObject.SetActive(true);
            }
            else
            {
                sC.positiveButton.gameObject.SetActive(false);
                sC.negativeButton.gameObject.SetActive(false);
            }
        }
            
        
    }

    void SetUpDefaultAttitude()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                currentAttitude = Attitude.SuperSad;
                break;
            case 1:
                currentAttitude = Attitude.Sad;
                break;
            case 2:
                currentAttitude = Attitude.Smile;
                break;
        }
    }

    void CheckCustomerStatus()
    {
        ChangeRespond(currentAttitude);

        if (talkTimes == 2)
        {
            if (currentAttitude == Attitude.Love)
            {
                Gm.isBonusMultiplierOn = true;
                Gm.specialCustomerBonusMultiplier = positiveMultiplier;
            }
            if(currentAttitude == Attitude.Cry)
            {
                Gm.isBonusMultiplierOn = true;
                Gm.specialCustomerBonusMultiplier = negativeMultiplier;
            }
            talkTimes++;
        }
    }

    public void PositiveRespond()
    {
        if (talkTimes < 2)
        {
            talkTimes++;
        }

        ChangeStatus(positiveRespond);
    }

    public void NegativeRespond()
    {
        if(talkTimes < 2)
        {
            talkTimes++;
        }

        ChangeStatus(negativeRespond);
    }

    void ChangeStatus(Attitude a)
    {
        switch(a)
        {
            case Attitude.SuperSad:
                currentAttitude = Attitude.SuperSad;
                status.sprite = sprite[0];
                break;
            case Attitude.Sad:
                currentAttitude = Attitude.Sad;
                status.sprite = sprite[1];
                break;
            case Attitude.Cry:
                currentAttitude = Attitude.Cry;
                status.sprite = sprite[2];
                break;
            case Attitude.Smile:
                currentAttitude = Attitude.Smile;
                status.sprite = sprite[3];
                break;
            case Attitude.BigSmile:
                currentAttitude = Attitude.BigSmile;
                status.sprite = sprite[4];
                break;
            case Attitude.Love:
                currentAttitude = Attitude.Love;
                status.sprite = sprite[5];
                break;
            case Attitude.Angry:
                currentAttitude = Attitude.Angry;
                status.sprite = sprite[6];
                break;
            case Attitude.Surprised:
                currentAttitude = Attitude.Surprised;
                status.sprite = sprite[7];
                break;
            case Attitude.Confused:
                currentAttitude = Attitude.Confused;
                status.sprite = sprite[8];
                break;
        }
    }

    public void ChangeRespond(Attitude a)
    {
        foreach (SliderCanvas sC in playerCanvas)
        {
            switch (a)
            {
                case Attitude.SuperSad:
                    sC.negativeButton.image.sprite = sprite[6];
                    sC.positiveButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Angry;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Sad:
                    sC.negativeButton.image.sprite = sprite[7];
                    sC.positiveButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Smile:
                    sC.negativeButton.image.sprite = sprite[7];
                    sC.positiveButton.image.sprite = sprite[4];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.BigSmile;
                    break;
                case Attitude.Angry:
                    sC.negativeButton.image.sprite = sprite[2];
                    sC.positiveButton.image.sprite = sprite[7];
                    negativeRespond = Attitude.Cry;
                    positiveRespond = Attitude.Confused;
                    break;
                case Attitude.Confused:
                    sC.negativeButton.image.sprite = sprite[6];
                    sC.positiveButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Angry;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Surprised:
                    sC.negativeButton.image.sprite = sprite[7];
                    sC.positiveButton.image.sprite = sprite[4];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.BigSmile;
                    break;
                case Attitude.BigSmile:
                    sC.negativeButton.image.sprite = sprite[8];
                    sC.positiveButton.image.sprite = sprite[5];
                    negativeRespond = Attitude.Surprised;
                    positiveRespond = Attitude.Love;
                    break;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CustomerWaitArea")
        {
            gameManager.StartNewCustomer();
            isMoving = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CustomerWaitArea")
        {
            if (gameManager.satisfactionMeter.value == gameManager.satisfactionMeter.minValue && leaveWhenMeterReachZero)
                isMoving = true;
        }
    }

    void CheckSpherecast()
    {
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxRaycastDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxRaycastDistance;
            currentHitObject = null;
        }
    }

    void AutoMove()
    {
        curSpeed = speed * Time.deltaTime;

        targetPoint = customerPath.GetPoint(curPathIndex);

        //If reach the radius within the path then move to next point in the path
        if (Vector3.Distance(transform.position, targetPoint) < customerPath.Radius)
        {
            //Don't move the vehicle if path is finished 
            if (curPathIndex < pathLength - 1)
                curPathIndex++;
            else if (isLooping)
                curPathIndex = 0;
            else
                return;
        }

        //Move the vehicle until the end point is reached in the path
        if (curPathIndex >= pathLength)
            return;

        //Calculate the next Velocity towards the path
        if (curPathIndex == pathLength - 1)
            velocity += Steer(targetPoint, true);
        else
            velocity += Steer(targetPoint);

        transform.position += velocity; //Move the vehicle according to the velocity

        if(curPathIndex > 0)
            playerDirection = customerPath.pointA[curPathIndex] - customerPath.pointA[curPathIndex - 1];

        transform.rotation = Quaternion.LookRotation(playerDirection); //Rotate the vehicle towards the desired Velocity
    }

    //Steering algorithm to steer the vector towards the target
    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;

        //Normalise the desired Velocity
        desiredVelocity.Normalize();

        //Calculate the velocity according to the speed
        if (bFinalPoint && dist < 10.0f)
            desiredVelocity *= (curSpeed * (dist / 10.0f));
        else
            desiredVelocity *= curSpeed;

        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
