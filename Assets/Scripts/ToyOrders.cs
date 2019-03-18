using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyOrders : MonoBehaviour {

    public enum ToyList
    {
        Ball,
        Doll,
        BaseballBat,
        Robot
    }

    public class ItemInfo
    {
        public ToyList toy;
        public int amount;
        public ItemInfo(ToyList t, int a)
        {
            toy = t;
            amount = a;
        }
    }

    private List<ToyList> defaultItemList;
    private List<ItemInfo> finalItemList;
    public Text item1Name, item2Name, item3Name;
    public Text item1Quantity, item2Quantity, item3Quantity;
    private bool item1Received, item2Received, item3Received;
    public bool canPickUp = false;

    // Use this for initialization
    void Start () {
        GenerateItems();
	}
	
	// Update is called once per frame
	void Update () {
        item1Quantity.text = "x " + finalItemList[0].amount.ToString();
        item2Quantity.text = "x " + finalItemList[1].amount.ToString();
        item3Quantity.text = "x " + finalItemList[2].amount.ToString();

        if (item1Received && item2Received && item3Received)
        {
           // Debug.Log("All items ready");
           // transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            canPickUp = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            ReceiveItem(ToyList.Ball);
        if (Input.GetKeyDown(KeyCode.W))
            ReceiveItem(ToyList.Doll);
        if (Input.GetKeyDown(KeyCode.E))
            ReceiveItem(ToyList.BaseballBat);
        if (Input.GetKeyDown(KeyCode.R))
            ReceiveItem(ToyList.Robot);
    }

    void GenerateItems()
    {
        finalItemList = new List<ItemInfo>();
        
        List<ToyList> shuffledList = ShuffleList();

        finalItemList.Add(new ItemInfo(shuffledList[0], Random.Range(1, 3)));
        finalItemList.Add(new ItemInfo(shuffledList[1], Random.Range(1, 3)));
        finalItemList.Add(new ItemInfo(shuffledList[2], Random.Range(1, 3)));

        item1Name.text = finalItemList[0].toy.ToString();
        item2Name.text = finalItemList[1].toy.ToString();
        item3Name.text = finalItemList[2].toy.ToString();
    }

    public void ReceiveItem(ToyList t)
    {
        foreach (ItemInfo iI in finalItemList)
        {
            if (t == iI.toy)
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

    private List<ToyList> ShuffleList()
    {
        defaultItemList = new List<ToyList>();
        defaultItemList.Add(ToyList.Ball);
        defaultItemList.Add(ToyList.Doll);
        defaultItemList.Add(ToyList.BaseballBat);
        defaultItemList.Add(ToyList.Robot);

        //List<ToyList> shuffledList = new List<ToyList>();

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
}
