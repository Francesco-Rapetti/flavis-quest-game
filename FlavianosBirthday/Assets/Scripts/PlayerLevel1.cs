using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLevel1 : MonoBehaviour
{
    private Transform player;
    private Vector2 previousPointB = new Vector2(0, 0);
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Animator animator;

    [Header("Level1")]
    public PlayerInfo playerInfo;
    public AnimatorOverrideController duck;
    public AnimatorOverrideController flaviano;
    public Transform circle;
    public Transform outerCircle;
    public GameObject keyUI;
    public GameObject objKey;
    public GameObject openDoorToTheMoonButton;
    public GameObject openDoorGoingUnderButton;
    public GameObject openDoorOriButton;
    public GameObject openDoorBeforeYourEyesButton;
    public GameObject openDoorMyFriendPedroButton;
    public GameObject openDoorCultOfTheLambButton;
    public GameObject chainsDoor;
    public GameObject cake;

    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        player.transform.position = playerInfo.playerPosition;

    }

    private void Start()
    {
        //continue settings
        if (PlayerPrefs.GetInt("CakeEat") == 1 && playerInfo.AllDoorsClosed() && PlayerPrefs.GetInt("HaveKey") == 0 && objKey != null) objKey.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        



        if (PlayerPrefs.GetInt("CakeEat") == 1)
        {
            cake.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            circle.transform.position = pointA * 1;
            outerCircle.transform.position = pointA * 1;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

        //door chains
        if (playerInfo.doorsOpened >= 5)
        {
            Destroy(chainsDoor);
        }

        //button and key visibility
        if (PlayerPrefs.GetInt("HaveKey") == 1) keyUI.SetActive(true);
        else
        {
            keyUI.SetActive(false);
            openDoorToTheMoonButton.SetActive(false);
            openDoorGoingUnderButton.SetActive(false);
            openDoorOriButton.SetActive(false);
            openDoorBeforeYourEyesButton.SetActive(false);
            openDoorMyFriendPedroButton.SetActive(false);
            openDoorCultOfTheLambButton.SetActive(false);
        }

        //Player transofmation
        if (PlayerPrefs.GetInt("IsDuck") == 1)
        {
            animator.runtimeAnimatorController = duck as RuntimeAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = flaviano as RuntimeAnimatorController;
        }
    }
    private void FixedUpdate()
    {

        if (touchStart && !(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            //activating walking animation
            animator.SetBool("isMoving", true);

            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

            /*if (IsWalkable(direction)) */moveCharacter(direction * 1);

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y) * 1;

            
            

            if (offset.x < 0 && offset.y > 0)           //caso nord-ovest
            {
                if (Math.Abs(offset.x) - Math.Abs(offset.y) > 0)
                {
                    //ovest
                    animator.SetFloat("moveX", -1);
                    animator.SetFloat("moveY", 0);
                }
                else
                {
                    //nord
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", 1);
                }
            }
            else if (offset.x > 0 && offset.y > 0)      //caso nord-est
            {
                if (Math.Abs(offset.x) - Math.Abs(offset.y) < 0)
                {
                    //nord
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", 1);
                }
                else
                {
                    //est
                    animator.SetFloat("moveX", 1);
                    animator.SetFloat("moveY", 0);
                }
            }
            else if (offset.x > 0 && offset.y < 0)      //caso sud-est
            {
                if (Math.Abs(offset.x) - Math.Abs(offset.y) < 0)
                {
                    //sud
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", -1);
                }
                else
                {
                    //est
                    animator.SetFloat("moveX", 1);
                    animator.SetFloat("moveY", 0);
                }
            }
            else if (offset.x < 0 && offset.y < 0)      //caso sud-ovest
            {
                if (Math.Abs(offset.x) - Math.Abs(offset.y) > 0)
                {
                    //ovest
                    animator.SetFloat("moveX", -1);
                    animator.SetFloat("moveY", 0);
                }
                else
                {
                    //sud
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", -1);
                }
            }


            
            //Debug.Log($"x: {offset.x}, y: {offset.y}");
            
        }
        else
        {
            //deactivating walking animation
            animator.SetBool("isMoving", false);

            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cake
        if (collision.gameObject.CompareTag("Cake"))
        {
            PlayerPrefs.SetInt("IsDuck", 1);
            objKey.SetActive(true);
            PlayerPrefs.SetInt("CakeEat", 1);
        }

        //key
        if (collision.gameObject.CompareTag("Key"))
        {
            PlayerPrefs.SetInt("HaveKey", 1);
            Destroy(collision.gameObject);
        }

        //To The Moon door
        if (collision.gameObject.CompareTag("DoorToTheMoonTrigger") && PlayerPrefs.GetInt("DoorTTMOpen") == 0) openDoorToTheMoonButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorToTheMoonTriggerOff")) openDoorToTheMoonButton.SetActive(false);

        //Going Under door
        if (collision.gameObject.CompareTag("DoorGoingUnderTrigger") && PlayerPrefs.GetInt("DoorGUOpen") == 0) openDoorGoingUnderButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorGoingUnderTriggerOff")) openDoorGoingUnderButton.SetActive(false);

        //Ori door
        if (collision.gameObject.CompareTag("DoorOriTrigger") && PlayerPrefs.GetInt("DoorOOpen") == 0) openDoorOriButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorOriTriggerOff")) openDoorOriButton.SetActive(false);

        //Before Your Eyes door
        if (collision.gameObject.CompareTag("DoorBeforeYourEyesTrigger") && PlayerPrefs.GetInt("DoorBYEOpen") == 0) openDoorBeforeYourEyesButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorBeforeYourEyesTriggerOff")) openDoorBeforeYourEyesButton.SetActive(false);

        //My Friend Pedro door
        if (collision.gameObject.CompareTag("DoorMyFriendPedroTrigger") && PlayerPrefs.GetInt("DoorMFPOpen") == 0) openDoorMyFriendPedroButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorMyFriendPedroTriggerOff")) openDoorMyFriendPedroButton.SetActive(false);

        //Cult Of The Lamb door
        if (collision.gameObject.CompareTag("DoorCultOfTheLambTrigger") && playerInfo.doorsOpened >= 5 && PlayerPrefs.GetInt("DoorCOTLOpen") == 0) openDoorCultOfTheLambButton.SetActive(true);
        if (collision.gameObject.CompareTag("DoorCultOfTheLambTriggerOff")) openDoorCultOfTheLambButton.SetActive(false);
    }



    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * playerInfo.speed * Time.deltaTime);
    }
}
