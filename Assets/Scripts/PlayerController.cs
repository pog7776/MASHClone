using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10;    //horizontal speed
    public float thrustSpeed = 15;  //vertical speed
    public int carryCapacity = 3;
    public GameObject sceneController;  //current scene controller

    [SerializeField] private int carrying;
    [SerializeField] private int point;

    private Rigidbody2D helicopter; //player

	// Use this for initialization
	void Start () {
       helicopter = GetComponent<Rigidbody2D>();    //indentify helicopter rigidBody (player)
        carrying = 0;
        point = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            helicopter.AddForce(transform.right * speed);   //if d or right is pressed add +x force to helicopter
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            helicopter.AddForce(transform.right * -speed);  //if a or left is pressed add -x force to helicopter
        }

        if (Input.GetButton("Jump") || Input.GetButton("Vertical") && Input.GetAxisRaw("Horizontal") > 0)
        {
            helicopter.AddForce(transform.up * thrustSpeed);    //if space or w is pressed add vertical force
        }
        /*else if(Input.GetButton("Vertical") && Input.GetAxisRaw("Horizontal") < 0)
        {
            helicopter.AddForce(transform.up * -thrustSpeed/2);
        }
        */      //idk why this doesn't work
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Soldier" && carrying < carryCapacity)    //collide with soldier
        {
            Destroy(col.gameObject);    //remove soldier from game
            //Debug.Log("pickup soldier"); //debug

            carrying++; //count how many soldiers are carried

            point = point + col.gameObject.GetComponent<Soldier>().pointValue;  //get point value from soldier
        }

        if (col.gameObject.tag == "Hospital" && carryCapacity > 0)  //collide with hospital
        {
            //Debug.Log("drop off soldier"); //debug
            sceneController.GetComponent<SceneController>().UpdateScore(point); //send to scene controller to add to score

            carrying = 0;
            point = 0;
        }
    }
}
