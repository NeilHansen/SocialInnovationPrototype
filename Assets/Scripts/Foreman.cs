using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foreman : MonoBehaviour
{

    public CustomerMovePath foremanPath;
    public GameManagerConstruction gameManager;
    public bool leaveWhenMeterReachZero = false;

    public enum ToolList
    {
        Nails,
        SmallPipe,
        SmallWood,
        ComboPipe,
        ComboWood,
        PipeConnector
    }
    public class itemInfo
    {
        public ToolList tool;
        public int amount;
        public itemInfo(ToolList t, int a)
        {
            tool = t;
            amount = a;
        }
    }
    public List<ToolList> defaultItemList;
    public List<itemInfo> finalItemList;
    public Text item1Name, item2Name, item3Name;
    public Text item1Quantity, item2Quantity, item3Quantity;
    private bool item1Received, item2Received, item3Received;
    private bool isCombo;
    //private int item1Needed, item2Needed, item3Needed;

    public bool isMoving;
    private float mass = 1.0f;
    private float speed = 3.0f;
    private bool isLooping = false;
    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;
    private Vector3 playerDirection;
    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        foremanPath = FindObjectOfType<CustomerMovePath>();
        gameManager = FindObjectOfType<GameManagerConstruction>();
        pathLength = foremanPath.Length;
        curPathIndex = 0;
        velocity = transform.forward;
        isCombo = IsCombo();
        GenerateItems();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    ReceiveItem(ToolList.Nails);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    ReceiveItem(ToolList.SmallPipe);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ReceiveItem(ToolList.SmallWood);
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    ReceiveItem(ToolList.ComboPipe);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ReceiveItem(ToolList.ComboWood);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    ReceiveItem(ToolList.PipeConnector);
        //}

        item1Quantity.text = "x " + finalItemList[0].amount.ToString();
        if (finalItemList.Count > 1)
            item2Quantity.text = "x " + finalItemList[1].amount.ToString();
        if (finalItemList.Count > 2)
            item3Quantity.text = "x " + finalItemList[2].amount.ToString();

        if (item1Received && item2Received && item3Received)
        {
            isMoving = true;
            //Destroy(gameObject);
            gameManager.AddScore();
            Destroy(gameObject.transform.parent.gameObject);
        }


        if (isMoving)
            AutoMove();


        //DONT DO THIS!!!!!
        if (transform.position.z <= -14.0f)
        {
            if (gameObject.transform.parent == null)
                Destroy(gameObject);
           
            else
                Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void GenerateItems()
    {
        finalItemList = new List<itemInfo>();
        //Spawn combo
        if (isCombo)
        {
            item3Received = true;
            item3Name.enabled = false;
            item3Quantity.enabled = false;
            switch (Random.Range(0, 2))
            {
                case 0:
                    item2Received = true;
                    item2Name.enabled = false;
                    item2Quantity.enabled = false;

                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            finalItemList.Add(new itemInfo(ToolList.ComboPipe, Random.Range(1, 2)));
                            break;
                        case 1:
                            finalItemList.Add(new itemInfo(ToolList.ComboWood, Random.Range(1, 1)));
                            break;
                        case 2:
                            finalItemList.Add(new itemInfo(ToolList.Nails, Random.Range(1, 2)));
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                    item1Name.text = finalItemList[0].tool.ToString();
                    break;
                case 1:
                    finalItemList.Add(new itemInfo(ToolList.ComboWood, Random.Range(1, 1)));
                    finalItemList.Add(new itemInfo(ToolList.Nails, Random.Range(1, 2)));
                    item1Name.text = finalItemList[0].tool.ToString();
                    item2Name.text = finalItemList[1].tool.ToString();
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        //Spawn random single items
        else
        {
            List<ToolList> shuffledList = ShuffleList();

            finalItemList.Add(new itemInfo(shuffledList[0], Random.Range(1, 3)));
            finalItemList.Add(new itemInfo(shuffledList[1], Random.Range(1, 3)));
            finalItemList.Add(new itemInfo(shuffledList[2], Random.Range(1, 3)));

            item1Name.text = finalItemList[0].tool.ToString();
            item2Name.text = finalItemList[1].tool.ToString();
            item3Name.text = finalItemList[2].tool.ToString();
        }
    }

    public void ReceiveItem(ToolList t)
    {
        foreach (itemInfo iI in finalItemList)
        {
            if (t == iI.tool)
            {
                if (iI.amount > 0)
                    iI.amount -= 1;
                if (iI.amount == 0)
                {
                    int index = finalItemList.IndexOf(iI);
                    switch (index)
                    {
                        case 0:
                            item1Received = true;
                            break;
                        case 1:
                            item2Received = true;
                            break;
                        case 2:
                            item3Received = true;
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
            }
        }
    }

    private List<ToolList> ShuffleList()
    {
        defaultItemList = new List<ToolList>();
        defaultItemList.Add(ToolList.Nails);
        defaultItemList.Add(ToolList.SmallPipe);
        defaultItemList.Add(ToolList.SmallWood);
        //Debug.Log(defaultItemList[0] + " " + defaultItemList[1] + " " + defaultItemList[2]);
        List<ToolList> shuffledList = new List<ToolList>();
        //Debug.Log(defaultItemList.Count);
        for (int i = 0; i < defaultItemList.Count; i++)
        {
            Swap(defaultItemList, i, Random.Range(0, defaultItemList.Count));
        }
        return defaultItemList;
    }

    private void Swap<T>(List<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    private bool IsCombo()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                return true;
            case 1:
            case 2:
            case 3:
            case 4:
                return false;
            default:
                return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CustomerWaitArea")
        {
            //gameManager.StartNewCustomer();
            //isMoving = false;
            //Debug.Log("Stop");
        }
    }

    void AutoMove()
    {
        curSpeed = speed * Time.deltaTime;

        targetPoint = foremanPath.GetPoint(curPathIndex);

        //If reach the radius within the path then move to next point in the path
        if (Vector3.Distance(transform.position, targetPoint) < foremanPath.Radius)
        {
            //Don't move the vehicle if path is finished 
            if (curPathIndex < pathLength - 1)
            {
                curPathIndex++;
                if (curPathIndex == 2)
                {
                    isMoving = false;
                    return;
                }
            }
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

        if (curPathIndex > 0)
            playerDirection = foremanPath.pointA[curPathIndex] - foremanPath.pointA[curPathIndex - 1];

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
        //if (bFinalPoint && dist < 10.0f)
        //    desiredVelocity *= (curSpeed * (dist / 10.0f));
        //else
        desiredVelocity *= curSpeed;

        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }
}