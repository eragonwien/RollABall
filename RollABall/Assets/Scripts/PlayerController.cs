using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rbody;
    private int count;
    public Text countText, winText, bonusText;
    private Boolean gameover;
    private float timeCounter;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        count = 0;
        RefreshCountText();
        winText.text = "";
        bonusText.text = "";
        gameover = false;
        timeCounter = 3;
    }

    // is before rendering = game code here
    private void Update()
    {
        if (gameover)
        {
            timeCounter -= Time.deltaTime;
            bonusText.text = ((int)timeCounter).ToString();
            if (timeCounter < 0)
            {
                Application.Quit();
            }
        }
    }

    // before physic calc = physic code
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rbody.AddForce(movement * speed); 
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            RefreshCountText();
            CheckWinning();
        }
    }

    

    private void RefreshCountText()
    {
        countText.text = "Score : " + count.ToString() + "/5";
    }

    private void CheckWinning()
    {
        if (count == 5)
        {
            winText.text = "You Win !";
            rbody.constraints = RigidbodyConstraints.FreezePosition;
            gameover = true;
        }
    }
}
