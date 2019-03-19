using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 0;    //horizontal speed
    public float thrustSpeed = 15;  //vertical speed
    public int carryCapacity = 3;
    public GameObject sceneController;  //current scene controller
    public float uprightForce = 5f;
    public float accelerateRotateForce = 1f;

    public Text passengersTxt;

    [SerializeField] private int carrying;
    [SerializeField] private int point;

    private Rigidbody2D helicopter; //player
    private Vector3 newScale;

    // Use this for initialization
    void Start () {
       helicopter = GetComponent<Rigidbody2D>();    //indentify helicopter rigidBody (player)
        newScale = helicopter.transform.localScale;
        carrying = 0;
        point = 0;
        if (passengersTxt) { passengersTxt.text = "Passengers: " + carrying.ToString(); }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            if (newScale.x != -1)
            {
                newScale.x = -1;   //change direction of heli
            }
            helicopter.AddForce(transform.right * speed);   //if d or right is pressed add +x force to helicopter
            helicopter.AddTorque(-accelerateRotateForce);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (newScale.x != 1)
            {
                newScale.x = 1;   //change direction of heli
            }
            helicopter.AddForce(transform.right * -speed);  //if a or left is pressed add -x force to helicopter
            helicopter.AddTorque(accelerateRotateForce);
        }
        helicopter.transform.localScale = newScale; //rotate heli

        if (Input.GetButton("Jump") || Input.GetButton("Vertical") && Input.GetAxisRaw("Horizontal") > 0)
        {
            helicopter.AddForce(transform.up * thrustSpeed);    //if space or w is pressed add vertical force

        }
        /*else if(Input.GetButton("Vertical") && Input.GetAxisRaw("Horizontal") < 0)
        {
            helicopter.AddForce(transform.up * -thrustSpeed/2);
        }
        */      //idk why this doesn't work


        if (Input.GetButtonDown("Restart")){    //reset scene when restart button pressed
            sceneController.GetComponent<SceneController>().ResetScene();
        }

         if(helicopter.transform.rotation.z > 0)
        {
            helicopter.AddTorque(-uprightForce * helicopter.transform.rotation.z);
        }
        else if(helicopter.transform.rotation.z < 0)
        {
            helicopter.AddTorque(uprightForce * -helicopter.transform.rotation.z);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Soldier" && carrying < carryCapacity)    //collide with soldier
        {
            Destroy(col.gameObject);    //remove soldier from game
            //Debug.Log("pickup soldier"); //debug

            carrying++; //count how many soldiers are carried

            passengersTxt.text = "Passengers: " + carrying.ToString();

            point = point + col.gameObject.GetComponent<Soldier>().pointValue;  //get point value from soldier
        }

        if (col.gameObject.tag == "Hospital" && carryCapacity > 0)  //collide with hospital
        {
            //Debug.Log("drop off soldier"); //debug
            sceneController.GetComponent<SceneController>().UpdateScore(point); //send to scene controller to add to score

            carrying = 0;
            point = 0;
            passengersTxt.text = "Passengers: " + carrying.ToString();
        }

        if(col.gameObject.tag == "Tree")    //if collide with tree
        {
            SceneManager.LoadScene("gameOver");
            //sceneController.GetComponent<SceneController>().ResetScene();   //come back and add game over screen
        }
    }
}
