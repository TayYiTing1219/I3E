using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //variables should be here!!
    int health = 10; 

    int maxHealth = 20;
    float speed = 5.0f; 

    string demoString = "Ayam Penyet from Food Club is the best.";

    bool isAlive = true;

    string number = "";

    int smallerInt = 3;

    int largerInt = 10;

    int times =  0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 1; i < 11; i+=1)
        {
            number += i + " ";
        }
        Debug.Log(number);

        while(smallerInt < largerInt)
        {
            smallerInt += 1;
            times += 1;
        }
        
        Debug.Log("It took " + times + " increments to make the numbers equal");
        Debug.Log("smallerInt is now " + smallerInt + " and largerInt is " + largerInt);
        // // health is 10
        // Debug.Log(health--);
        // // the -- will do it after the statement 
        // // result = 10
        // Debug.Log(--health);
        // // it will - the health exactly at the line not after


        // // Debug.Log("Are you sure " + demoString);
        // // Debug.Log(isAlive);

        // // for(int i= 0; i <= 10; ++i)
        // // {
        // //     --health;
        // // }

    //     while(isAlive)
    //     {
    //         --health;
    //         // 
    //         if(health <= 0)
    //          isAlive = false;
    //          Debug.Log("Player is dead");
    //     }

    //     if(health >= maxHealth)
    //     {
    //         Debug.Log("Player is at full health");
    //     }
    //     else if(health < maxHealth && health > 0)
    //     {
    //         Debug.Log("Player is not at full health");
    //     }
    //     else if(health == 0)
    //     {
    //         Debug.Log("Player is dead");
    //     }
    //     if(health%2 == 0)
    //     {
    //         Debug.Log("health is even");
    //     }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("This is a demo component");
    }
}
