using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerGoingUnder : MonoBehaviour
{
    private Transform player;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Animator animator;
    
    public PlayerInfo playerInfo;
    public AnimatorOverrideController duck;
    public AnimatorOverrideController flaviano;
    public Transform circle;
    public Transform outerCircle;
    public GameObject keyUI;
    public Camera cam;
    public GameObject objKey;

    [Header("LevelGoingUnder")]
    [SerializeField] GameObject goingUnderScreen;
    [SerializeField] GameObject goingUnderCover;

    private bool control = true;
    private bool control2 = false;

    [Header("Buttons")]
    [SerializeField] GameObject mirrorButton;
    [SerializeField] GameObject statistikoButton1;
    [SerializeField] GameObject readyButton;



    [Header("Dialogs")]
    [SerializeField] DialogManager mirrorDialog;
    [SerializeField] DialogManager statistikoDialog;


    
    


    

    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        mirrorButton.SetActive(false);
        statistikoButton1.SetActive(false);
        readyButton.SetActive(false);
        goingUnderScreen.SetActive(false);
       
        if (playerInfo.statisticFinished)
        {
            if (PlayerPrefs.GetInt("HasGoingUnder") == 0) goingUnderCover.SetActive(true);
            if (PlayerPrefs.GetInt("HasGoingUnderKey") == 0) objKey.SetActive(true);
        }

        
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("HasGoingUnder") == 1 && PlayerPrefs.GetInt("HasGoingUnderKey") == 1)
        {
            playerInfo.talkedToStatistiko1 = true;
            playerInfo.statisticFinished = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //ready button after talked to statistiko
        //Debug.Log(statistikoDialog.currentDialog+", "+statistikoDialog.currentLine);
        if (statistikoDialog.currentDialog == 0 && statistikoDialog.currentLine >= 1) control2 = true;
        //Debug.Log(control2);
        if (playerInfo.talkedToStatistiko1 && !playerInfo.statisticFinished && control == true && !playerInfo.isTalking && control2 == true)
        {
            readyButton.SetActive(true);
            control = false;
        }





            //se non parla si muove
            if (!playerInfo.isTalking)
        {
            if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
            {

                pointA = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
                circle.transform.position = pointA * 1;
                outerCircle.transform.position = pointA * 1;
                pointA = cam.transform.InverseTransformPoint(pointA);

                circle.GetComponent<SpriteRenderer>().enabled = true;
                outerCircle.GetComponent<SpriteRenderer>().enabled = true;

                //Debug.Log($"pointA.x: {pointA.x}, pointA.y: {pointA.y}");
            }
            if (Input.GetMouseButton(0))
            {

                pointB = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
                pointB = cam.transform.InverseTransformPoint(pointB);
                touchStart = true;

                //Debug.Log($"pointB.x: {pointB.x}, pointB.y: {pointB.y}");
            }
            else
            {
                touchStart = false;
            }
        }

        //se sta parlando
        if (playerInfo.isTalking)
        {
            mirrorButton.SetActive(false);
            statistikoButton1.SetActive(false);
            if (playerInfo.orator == "Mirror") mirrorDialog.HandleUpdate();
            if (playerInfo.orator == "Statistiko") statistikoDialog.HandleUpdate();

            
            
        }

        //button and key visibility
        if (PlayerPrefs.GetInt("HaveKey") == 1) keyUI.SetActive(true);
        else
        {
            keyUI.SetActive(false);
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
        if (!playerInfo.isTalking)
        {
            //se sta camminando
            if (touchStart && !(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
            {
                //activating walking animation
                animator.SetBool("isMoving", true);

                Vector2 offset = pointB - pointA;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

                moveCharacter(direction * 1);
                //MoveMap(direction * -1);

                circle.transform.position = new Vector2(outerCircle.position.x + direction.x, outerCircle.position.y + direction.y) * 1;




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

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cake
        if (collision.gameObject.CompareTag("Cake"))
        {
            PlayerPrefs.SetInt("IsDuck", 1);
            Destroy(collision.gameObject);
            objKey.SetActive(true);
        }

        //key
        if (collision.gameObject.CompareTag("Key"))
        {
            PlayerPrefs.SetInt("HaveKey", 1);
            PlayerPrefs.SetInt("HasGoingUnderKey", 1);
            Destroy(collision.gameObject);
        }

        //mirror
        if (collision.gameObject.CompareTag("MirrorInteractTrigger"))
        {
            mirrorButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("MirrorInteractTriggerOff"))
        {
            mirrorButton.SetActive(false);
        }

        //statistiko
        if (collision.gameObject.CompareTag("StatistikoInteractTrigger"))
        {
            if (!playerInfo.talkedToStatistiko1)
            {
                statistikoButton1.SetActive(true);
            }
            if (playerInfo.talkedToStatistiko1 && !playerInfo.statisticFinished)
            {
                readyButton.SetActive(true);
            }
            if (playerInfo.talkedToStatistiko1 && playerInfo.statisticFinished && PlayerPrefs.GetInt("HasGoingUnder") == 0)
            {
                statistikoDialog.currentDialog = 1;
                statistikoButton1.SetActive(true);
            }
            if (playerInfo.talkedToStatistiko1 && playerInfo.statisticFinished && PlayerPrefs.GetInt("HasGoingUnder") == 1)
            {
                statistikoDialog.currentDialog = 2;
                statistikoButton1.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("StatistikoInteractTriggerOff"))
        {
            statistikoButton1.SetActive(false);
            readyButton.SetActive(false);
        }

        //going under cover
        if (collision.gameObject.CompareTag("GoingUnderCover"))
        {
            PlayerPrefs.SetInt("HasGoingUnder", 1);
            Destroy(collision.gameObject);
            goingUnderScreen.SetActive(true);

        }

        

        


    }

    public void SetDialogStatistikoTo1()
    {
        statistikoDialog.currentDialog = 1;
    }

    public void ActivateHintTape()
    {
        playerInfo.HintTapeActive = true;
    }




    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * playerInfo.speed * Time.deltaTime);
    }

    
}
