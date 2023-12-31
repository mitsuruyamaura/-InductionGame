using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    int x;

    public int calcValue;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">ë´Ç∑êî</param>
    /// <param name="b">ë´Ç∑êî</param>
    private void Calculate(int a, int b) {  // 2-2

        x = a + b;  // 3
        Debug.Log("x ÇÃíl : " + x);  // 4
    }

    void Start() {

        CalculateMoney(calcValue);   /// 168798

        StartHoge();

        x = 10;  // 1

        int y = Hikizan(11, 6);  // value
        // y = Hkizan value // 11- 6 = 5

        // int = int;

        int random = UnityEngine.Random.Range(0, 100);
        //int random = 59;

        Rigidbody rb = GetComponent<Rigidbody>();

        //GameObject objPrefab = null;
        //GameObject obj = Instantiate(objPrefab);


        // GameObject = GameObject

        // Rigidbody = Rigidbody;

        // Rigidbody = Image
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CalculateMoney(calcValue);
        }    
    }


    private int Hikizan(int c, int d) {

        //int value = c - d;

        //return value;

        return c - d;
    }



    private bool CheckEnemy(int no) {  // 4  8

        // 4 > 5
        // 8 > 5
        return no > 5;  // false  true
    }


    private void Hoge() {  // 2

        // CheckEnemy(Random.Range(0,10) -> CheckEnemy(4)
        // CheckEnemy(Random.Range(0,10) -> CheckEnemy(8)

        // bool false == true   true == true

        int value = UnityEngine.Random.Range(0, 10);
        Debug.Log("Random Ç»íl :" + value);

        if (CheckEnemy(value) == true) {
            Debug.Log("Enemy");
        } else {
            Debug.Log("Not Enemy");
        }
    }

    private void StartHoge() {
        //Hoge();  // 1


        // ElementType = ElementType;

        int randomValue = UnityEngine.Random.Range(0, 4);
        Debug.Log("Random Ç»íl" + randomValue);
        ElementType elementType = GetElementTypeFromNo(randomValue);
        Debug.Log("ElementType : " + elementType);
        //elementType = ElementType.Fire;
    }


    public enum ElementType {
        Fire,
        Ice,
        Wind,
        Earth
    }


    private ElementType GetElementTypeFromNo(int no) {  // no = 1

        switch (no) {
            case 0:
                return ElementType.Ice;

            case 1:
                return ElementType.Fire;

            case 2:
                return ElementType.Earth;

            case 3:
                return ElementType.Wind;

            default:
                return ElementType.Fire;
        }
    }



    //public enum MoneyType {
    //    TenThousand,
    //    FiveThousand,
    //    TwoThousand,
    //    OneThousand,

    //    FiveHundred,
    //    OneHundred,

    //    Fifty,
    //    Ten,

    //    Five,
    //    One,
    //}

    // ã‡éÌÇÃìoò^
    int[] moneys = new int[10] { 10000, 5000, 2000, 1000, 500, 100, 50, 10, 5, 1 };

    private void CalculateMoney(int amount) {

        // ã‡éÌÇÃêîÇæÇØåJÇËï‘Ç∑
        for (int i = 0; i < moneys.Length; i++) {
            // ã‡éÌÇ≤Ç∆Ç…êîÇåvéZÇµÅAamount Ç©ÇÁå∏éZÇ∑ÇÈ
            int count = amount / moneys[i];
            amount -= count * moneys[i];

            if (i < 4) {
                //Console.WriteLine(moneys[i] + " â~éDÇ™ " + count + " ñá");
                Debug.Log(moneys[i] + " â~éDÇ™ " + count + " ñá");
            } else {
                Debug.Log(moneys[i] + " â~ã Ç™ " + count + " ñá");
            }
        }
        Debug.Log("ïKóvÇ≈Ç∑");

        //int tenThousand = amount / 10000;
        //Debug.Log("10000â~éD : " + tenThousand);
        //amount -= tenThousand * 10000;

        //int fiveThousand = amount / 5000;
        //Debug.Log("5000â~éD : " + fiveThousand);
        //amount -= fiveThousand * 5000;

        //int twoThousand = amount / 2000;
        //Debug.Log("2000â~éD : " + twoThousand);
        //amount -= twoThousand * 2000;

        //int oneThousand = amount / 1000;
        //Debug.Log("1000â~éD : " + oneThousand);
        //amount -= oneThousand * 1000;

        ////Debug.Log(amount);

        //int fiveHundred = amount / 500;
        //Debug.Log("500â~ã  : " + fiveHundred);
        //amount -= fiveHundred * 500;

        //int oneHundred = amount / 100;
        //Debug.Log("100â~ã  : " + oneHundred);
        //amount -= oneHundred * 100;

        //int fifty = amount / 50;
        //Debug.Log("50â~ã  : " + fifty);
        //amount -= fifty * 50;

        //int ten = amount / 10;
        //Debug.Log("10â~ã  : " + ten);
        //amount -= ten * 10;

        //int five = amount / 5;
        //Debug.Log("5â~ã  : " + five);
        //amount -= five * 5;

        //int one = amount;
        //Debug.Log("1â~ã  : " + one);

        //Debug.Log("ïKóvÇ≈Ç∑");
    }
}