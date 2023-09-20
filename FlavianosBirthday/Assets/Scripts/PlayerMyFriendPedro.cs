using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class PlayerMyFriendPedro : MonoBehaviour
{
    private Transform player;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Animator animator;
    private bool playerHit = false;
    
    public PlayerInfo playerInfo;
    public AnimatorOverrideController duck;
    public AnimatorOverrideController flaviano;
    public Transform circle;
    public Transform outerCircle;
    public GameObject keyUI;
    public Camera cam;
    public GameObject objKey;

    [Header("LevelMyFriendPedro")]
    Vector2 startPos;
    [SerializeField] GameObject myFriendPedroCover;
    [SerializeField] GameObject myFriendPedroScreen;
    [SerializeField] Animator transition;

    [Header("Buttons")]
    [SerializeField] GameObject leverButton;
    [SerializeField] GameObject pedroTalkButton;
    [SerializeField] GameObject pascalTalkButton;
    [SerializeField] GameObject easterEggButton;

    [Header("Dialogs")]
    [SerializeField] DialogManager pedroDialogManager;
    [SerializeField] DialogManager pascalDialogManager;
    [SerializeField] DialogManager easterEggDialogManager;

    
    
    


    

    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();

        leverButton.SetActive(false);
        pedroTalkButton.SetActive(false);
        pascalTalkButton.SetActive(false);
        easterEggButton.SetActive(false);

     
        if (playerInfo.leverPulled)
        {
            if (PlayerPrefs.GetInt("HasMyFriendPedro") == 0) myFriendPedroCover.SetActive(true);
            if (PlayerPrefs.GetInt("HasMyFriendPedroKey") == 0) objKey.SetActive(true);
        }
        
    }

    private void Start()
    {
        startPos = new Vector3(-7.723f, 5.764f, player.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        //Pedro dialog
        if (!playerInfo.leverPulled)
        {
            pedroDialogManager.currentDialog = 0;
        }
        if (playerInfo.leverPulled)
        {
            pedroDialogManager.currentDialog = 1;
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
            pedroTalkButton.SetActive(false);
            pascalTalkButton.SetActive(false);
            easterEggButton.SetActive(false);
            if (playerInfo.orator == "Pedro") pedroDialogManager.HandleUpdate();
            if (playerInfo.orator == "Pascal") pascalDialogManager.HandleUpdate();
            if (playerInfo.orator == "EasterEgg") easterEggDialogManager.HandleUpdate();

            /*mirrorButton.SetActive(false);
            statistikoButton1.SetActive(false);
            if (playerInfo.orator == "Mirror") mirrorDialog.HandleUpdate();
            if (playerInfo.orator == "Statistiko") statistikoDialog.HandleUpdate();*/


            

            
            
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
            PlayerPrefs.SetInt("HasMyFriendPedroKey", 1);
            Destroy(collision.gameObject);
        }

        //bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!playerHit)
            {
                playerHit = true;
                StartCoroutine(PlayerHit());
            }
        }

        //lever
        if (collision.gameObject.CompareTag("LeverPullTrigger"))
        {
            leverButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("LeverPullTriggerOff"))
        {
            leverButton.SetActive(false);
        }

        //Pedro
        if (collision.gameObject.CompareTag("PedroTalkTrigger"))
        {
            pedroTalkButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("PedroTalkTriggerOff"))
        {
            pedroTalkButton.SetActive(false);
        }

        //Pascal
        if (collision.gameObject.CompareTag("PascalTalkTrigger"))
        {
            if (!playerInfo.leverPulled)
            {
                pascalDialogManager.currentDialog = 0;
                pascalTalkButton.SetActive(true);
            }

            if (playerInfo.leverPulled && PlayerPrefs.GetInt("HasMyFriendPedro") == 0)
            {
                pascalDialogManager.currentDialog = 1;
                pascalTalkButton.SetActive(true);
            }

            if (playerInfo.leverPulled && PlayerPrefs.GetInt("HasMyFriendPedro") == 1)
            {
                pascalDialogManager.currentDialog = 2;
                pascalTalkButton.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("PascalTalkTriggerOff"))
        {
            pascalTalkButton.SetActive(false);
        }

        //My Friend Pedro cover
        if (collision.gameObject.CompareTag("MyFriendPedroCover"))
        {
            PlayerPrefs.SetInt("HasMyFriendPedro", 1);
            Destroy(collision.gameObject);
            myFriendPedroScreen.SetActive(true);

        }

        //easter egg
        if (collision.gameObject.CompareTag("EasterEggTrigger"))
        {
            easterEggButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("EasterEggTriggerOff"))
        {
            easterEggButton.SetActive(false);
        }


    }

    IEnumerator PlayerHit()
    {
        //play animation
        transition.SetTrigger("Start");
        //Debug.Log("Start");

        //wait
        yield return new WaitForSeconds(1);

        //load scene
        gameObject.transform.position = startPos;
        playerHit = false;

        //end animation
        transition.SetTrigger("End");
        //Debug.Log("End");

        //wait
        yield return new WaitForSeconds(1);


        //stand animation
        transition.SetTrigger("Stand");
        //Debug.Log("Stand");
    }




    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * playerInfo.speed * Time.deltaTime);
    }

    
}
