using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {

    public CustomerMovePath customerPath;
    public GameManager gameManager;

    public Sprite[] sprite;
    public Image status;

    private bool isMoving = true;
    private float mass = 1.0f;
    private float speed = 3.0f;
    private bool isLooping = false;
    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;
    private Vector3 playerDirection;
    private Vector3 velocity;

    public GameObject currentHitObject;
    private float currentHitDistance;

    public float maxRaycastDistance;
    public float sphereRadius;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    // Use this for initialization
    void Start () {
		customerPath = FindObjectOfType<CustomerMovePath>();
        gameManager = FindObjectOfType<GameManager>();
        status = FindObjectOfType<Image>();
        pathLength = customerPath.Length;
        curPathIndex = 0;
        velocity = transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        
        CheckSpherecast();

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

    public void PositiveRespond()
    {
        status.sprite = sprite[0];
    }

    public void NegativeRespond()
    {
        status.sprite = sprite[1];
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
            if (gameManager.satisfactionMeter.value == gameManager.satisfactionMeter.minValue)
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
