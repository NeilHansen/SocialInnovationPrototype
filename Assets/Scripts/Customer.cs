using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{

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

	int previousRandoNum;

	public enum Attitude
	{
		Angry,
        BigSmile,
        Confused,
        Cry,
        Frown,
        Smile,
        SuperSad,
        Surprised,
        Wink,
        None
	}

	public List<Attitude> attitudeList;

	public class Responds
	{
		public Attitude customerAttitude;
		public Attitude correctAnswer;
		public Attitude wrongAnswer;
	}

	public List<Responds> correctRespond;

	Responds currentAttitude;
	Attitude positiveRespond;
	Attitude negativeRespond;

	public int correctTimes = 0;

	public float positiveMultiplier = 2.0f;
	public float negativeMultiplier = 0.5f;

	public Text happyPointText;

	// Use this for initialization
	void Start()
	{
		
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
			GenerateAttitudeList();
            GenerateCorrectRespondList();

			currentAttitude = GenerateAttitude(Attitude.None);
			foreach (SliderCanvas sC in playerCanvas)
			{
				AssignButtonFunction(currentAttitude, sC);
			}
		}
		int rando = Random.Range(0, 2);
		if (rando != 1)
		{
			currentSituation = newSituationSprites;
		}
		else
		{
			currentSituation = sprite;
		}
	}

	// Update is called once per frame
	void Update()
	{

		CheckSpherecast();


		if (gameObject.tag == "SpecialCustomer")
		{
			TriggerDialogue();
			ChangeStatus(currentAttitude.customerAttitude);
			happyPointText.text = correctTimes.ToString();
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

		if (isMoving)
			AutoMove();

		if (transform.position.z <= -12.0f)
		{
			if (gameObject.transform.parent == null)
				Destroy(gameObject);
			else
				Destroy(gameObject.transform.parent.gameObject);
		}
	}

    void GenerateAttitudeList()
	{
		attitudeList = new List<Attitude>();
		attitudeList.Add(Attitude.Angry);
		attitudeList.Add(Attitude.BigSmile);
		attitudeList.Add(Attitude.Confused);
		attitudeList.Add(Attitude.Cry);
		attitudeList.Add(Attitude.Frown);
		attitudeList.Add(Attitude.None);
		attitudeList.Add(Attitude.Smile);
		attitudeList.Add(Attitude.SuperSad);
		attitudeList.Add(Attitude.Surprised);
		attitudeList.Add(Attitude.Wink);
	}

	void GenerateCorrectRespondList()
	{
		correctRespond = new List<Responds>();
		Responds temp1 = new Responds();
		temp1.customerAttitude = Attitude.Angry;
		temp1.correctAnswer = Attitude.Wink;
		temp1.wrongAnswer = Attitude.None;
		correctRespond.Add(temp1);
		Responds temp2 = new Responds();
		temp2.customerAttitude = Attitude.Cry;
		temp2.correctAnswer = Attitude.BigSmile;
		temp2.wrongAnswer = Attitude.None;
		correctRespond.Add(temp2);
		Responds temp3 = new Responds();
		temp3.customerAttitude = Attitude.Confused;
		temp3.correctAnswer = Attitude.Surprised;
		temp3.wrongAnswer = Attitude.None;
		correctRespond.Add(temp3);
		Responds temp4 = new Responds();
		temp4.customerAttitude = Attitude.Frown;
		temp4.correctAnswer = Attitude.Smile;
		temp4.wrongAnswer = Attitude.None;
		correctRespond.Add(temp4);
	}

	Responds GenerateAttitude(Attitude cAttitude)
	{
		List<Responds> availableAttitudes = new List<Responds>();
		//availableAttitudes.RemoveRange(0, availableAttitudes.Count);
		Responds retVal = new Responds();

		foreach (Responds r in correctRespond)
		{
			if (r.customerAttitude != cAttitude || r.customerAttitude != Attitude.None)
			{
				availableAttitudes.Add(r);
			}
		}

		int tempRandom = Random.Range(0, 3);
		if (tempRandom != previousRandoNum)
		{
			previousRandoNum = tempRandom;
			switch (tempRandom)
			{
				case 0:
					retVal = availableAttitudes[0];
					break;
				case 1:
					retVal = availableAttitudes[1];
					break;
				case 2:
					retVal = availableAttitudes[2];
					break;
				case 3:
					retVal = availableAttitudes[3];
					break;
				default:
					Debug.Log("Error generating attitude");
					break;
			}
		}

		retVal.wrongAnswer = GenerateWrongRespond(retVal.correctAnswer);
		Debug.Log(retVal.customerAttitude + " " + retVal.correctAnswer + " " + retVal.wrongAnswer);

		return retVal;
	}

    Attitude GenerateWrongRespond(Attitude a)
	{
		Attitude retValue = Attitude.None;
		List<Attitude> avaiAttitudes = new List<Attitude>();

		foreach(Attitude at in attitudeList)
		{
			if(at != a && at!= Attitude.None)
			{
				avaiAttitudes.Add(at);
			}
		}

		switch(Random.Range(0,8))
		{
			case 0:
				retValue = avaiAttitudes[0];
				break;
			case 1:
				retValue = avaiAttitudes[1];
				break;
			case 2:
				retValue = avaiAttitudes[2];
				break;
			case 3:
				retValue = avaiAttitudes[3];
                break;
			case 4:
				retValue = avaiAttitudes[4];
                break;
			case 5:
				retValue = avaiAttitudes[5];
                break;
			case 6:
				retValue = avaiAttitudes[6];
                break;
			case 7:
				retValue = avaiAttitudes[7];
                break;
			case 8:
				retValue = avaiAttitudes[8];
                break;
			default:
				Debug.Log("Error generating wrong respond");
				break;
		}

		return retValue;
	}

	void AssignButtonFunction(Responds cAttitude, SliderCanvas slider)
	{
		slider.leftButton.onClick.RemoveAllListeners();
        slider.rightButton.onClick.RemoveAllListeners();
		Attitude correct = cAttitude.correctAnswer;
		Attitude wrong = cAttitude.wrongAnswer;
		Debug.Log(correctTimes);
        switch (Random.Range(0,2))
		{
			case 0:
				AssignButtonImage(slider.leftButton, correct);
				AssignButtonImage(slider.rightButton, wrong);
				slider.leftButton.onClick.AddListener(CorrectRespond);
				slider.rightButton.onClick.AddListener(ResetRespond);
				break;
			case 1:
				AssignButtonImage(slider.leftButton, wrong);
                AssignButtonImage(slider.rightButton, correct);
				slider.leftButton.onClick.AddListener(ResetRespond);
				slider.rightButton.onClick.AddListener(CorrectRespond);
				break;

			default:
				Debug.Log("Error assigning button function");
				break;
		}
	}

    void CorrectRespond()
	{
		correctTimes += 1;
        if(correctTimes >= 2)
		{
			Gm.isBonusMultiplierOn = true;
            Gm.specialCustomerBonusMultiplier = positiveMultiplier;
		}
		ResetRespond();
	}

    void ResetRespond()
	{
		currentAttitude = GenerateAttitude(currentAttitude.customerAttitude);
        foreach (SliderCanvas sC in playerCanvas)
        {
            AssignButtonFunction(currentAttitude, sC);
        }
	}

	void AssignButtonImage(Button button, Attitude a)
	{
		switch (a)
		{
			case Attitude.Angry:
				button.image.sprite = sprite[0];
				break;
			case Attitude.BigSmile:
				button.image.sprite = sprite[1];
				break;
			case Attitude.Confused:
				button.image.sprite = sprite[2];
				break;
			case Attitude.Cry:
				button.image.sprite = sprite[3];
				break;
			case Attitude.Frown:
				button.image.sprite = sprite[4];
				break;
			case Attitude.Smile:
				button.image.sprite = sprite[5];
				break;
			case Attitude.SuperSad:
				button.image.sprite = sprite[6];
				break;
			case Attitude.Surprised:
				button.image.sprite = sprite[7];
				break;
			case Attitude.Wink:
				button.image.sprite = sprite[8];
				break;
		}
	}

    void TriggerDialogue()
    {      
		foreach (SliderCanvas sC in playerCanvas)
		{
			if (Vector3.Distance(sC.gameObject.transform.position, transform.position) < 5.0f && correctTimes < 2)
			{
				sC.leftButton.gameObject.SetActive(true);
				sC.rightButton.gameObject.SetActive(true);
			}
			else
			{
				sC.leftButton.gameObject.SetActive(false);
				sC.rightButton.gameObject.SetActive(false);
			}
		}
    }
    

    /*void CheckCustomerStatus()
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
    }*/

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
			case Attitude.Angry:
                status.sprite = sprite[0];
                break;
            case Attitude.BigSmile:
                //currentAttitude = Attitude.Sad;
                status.sprite = sprite[1];
                break;
			case Attitude.Confused:
                //currentAttitude = Attitude.Cry;
                status.sprite = sprite[2];
                break;
            case Attitude.Cry:
                //currentAttitude = Attitude.Smile;
                status.sprite = sprite[3];
                break;
            case Attitude.Frown:
                //currentAttitude = Attitude.BigSmile;
                status.sprite = sprite[4];
                break;
            case Attitude.Smile:
                //currentAttitude = Attitude.Love;
                status.sprite = sprite[5];
                break;
			case Attitude.SuperSad:
                //currentAttitude = Attitude.Angry;
                status.sprite = sprite[6];
                break;
            case Attitude.Surprised:
                //currentAttitude = Attitude.Surprised;
                status.sprite = sprite[7];
                break;
            case Attitude.Wink:
                //currentAttitude = Attitude.Confused;
                status.sprite = sprite[8];
                break;
        }
    }

    /*public void ChangeRespond(Attitude a)
    {
        foreach (SliderCanvas sC in playerCanvas)
        {
            switch (a)
            {
                case Attitude.SuperSad:
					sC.leftButton.image.sprite = sprite[6];
					sC.rightButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Angry;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Sad:
					sC.leftButton.image.sprite = sprite[7];
					sC.rightButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Smile:
					sC.leftButton.image.sprite = sprite[7];
					sC.rightButton.image.sprite = sprite[4];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.BigSmile;
                    break;
                case Attitude.Angry:
					sC.leftButton.image.sprite = sprite[2];
					sC.rightButton.image.sprite = sprite[7];
                    negativeRespond = Attitude.Cry;
                    positiveRespond = Attitude.Confused;
                    break;
                case Attitude.Confused:
					sC.leftButton.image.sprite = sprite[6];
					sC.rightButton.image.sprite = sprite[8];
                    negativeRespond = Attitude.Angry;
                    positiveRespond = Attitude.Surprised;
                    break;
                case Attitude.Surprised:
					sC.leftButton.image.sprite = sprite[7];
					sC.rightButton.image.sprite = sprite[4];
                    negativeRespond = Attitude.Confused;
                    positiveRespond = Attitude.BigSmile;
                    break;
                case Attitude.BigSmile:
					sC.leftButton.image.sprite = sprite[8];
					sC.rightButton.image.sprite = sprite[5];
                    negativeRespond = Attitude.Surprised;
                    positiveRespond = Attitude.Love;
                    break;
            }
        }
    }*/
    
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
