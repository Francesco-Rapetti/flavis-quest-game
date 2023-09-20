using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerLevel2 : MonoBehaviour
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

    [Header("ToTheMoon")]
    public GameObject objKey;
    public GameObject objFindingParadiseCover;
    public GameObject map;
    public GameObject hintTape;
    public GameObject findingParadiseScreen;
    public GameObject enigma;
    public GameObject enigmaObstacle;

    [Header("Buttons")]
    public GameObject talk1Button;
    public GameObject talk2Button;
    public GameObject talk3Button;  
    public GameObject talk4Button;
    public GameObject interactButton;
    public GameObject tryButton;
    public GameObject playButton;

    [Header("Dialogs")]
    public DialogManager dialogWattsManager;
    public DialogManager dialogStaticBoyManager;
    public DialogManager dialogHintTapeManager;


    

    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        talk1Button.SetActive(false);
        talk2Button.SetActive(false);
        talk3Button.SetActive(false);
        talk4Button.SetActive(false);
        interactButton.SetActive(false);
        tryButton.SetActive(false);
        playButton.SetActive(false);

        if (playerInfo.enigmaSolved)
        {
            if (PlayerPrefs.GetInt("HasFindingParadise") == 0) objFindingParadiseCover.SetActive(true);
            if (PlayerPrefs.GetInt("HasFindingParadiseKey") == 0) objKey.SetActive(true);
        }

        enigma.SetActive(false);
        enigmaObstacle.SetActive(true);
    }

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        //hint tape visibility
        if (playerInfo.HintTapeActive) hintTape.SetActive(true);
        else hintTape.SetActive(false);


        if (playerInfo.talkedToWatts1)
        {
            enigma.SetActive(true);
            enigmaObstacle.SetActive(false);
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
            talk1Button.SetActive(false);
            talk2Button.SetActive(false);
            talk3Button.SetActive(false);
            talk4Button.SetActive(false);
            interactButton.SetActive(false);
            playButton.SetActive(false);

            if (playerInfo.orator == "Watts") dialogWattsManager.HandleUpdate();
            if (playerInfo.orator == "StaticBoy")
            {
                dialogStaticBoyManager.HandleUpdate();

                if (!playerInfo.enigmaSolved) tryButton.SetActive(true); 
                else tryButton.SetActive(false);
            }
            if (playerInfo.orator == "HintTape") dialogHintTapeManager.HandleUpdate();
            
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

        //set static boy dialog
        if (playerInfo.enigmaSolved && PlayerPrefs.GetInt("HasFindingParadise") == 0)
        {
            dialogStaticBoyManager.currentDialog = 1;
        }
        if (PlayerPrefs.GetInt("HasFindingParadise") == 1)
        {
            dialogStaticBoyManager.currentDialog = 2;
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
            PlayerPrefs.SetInt("HasFindingParadiseKey", 1);
            Destroy(collision.gameObject);
        }

        //buttons
        //DrWatts
        if (collision.gameObject.CompareTag("DrWattsTalkTrigger"))
        {
            if (!playerInfo.talkedToWatts1 && !playerInfo.isTalking)
            {
                talk1Button.SetActive(true);
                dialogWattsManager.currentDialog = 0;
            }
            else if (playerInfo.talkedToWatts1 && !playerInfo.enigmaSolved && !playerInfo.isTalking)
            {
                talk2Button.SetActive(true);
                dialogWattsManager.currentDialog = 1;
            }
            else if (playerInfo.talkedToWatts1 && playerInfo.enigmaSolved && !playerInfo.talkedToWatts3 && !playerInfo.isTalking)
            {
                talk3Button.SetActive(true);
                dialogWattsManager.currentDialog = 2;
            }
            else if (playerInfo.talkedToWatts1 && playerInfo.talkedToWatts3 && playerInfo.enigmaSolved && !playerInfo.isTalking)
            {
                talk4Button.SetActive(true);
                dialogWattsManager.currentDialog = 3;
            }
        }
        if (collision.gameObject.CompareTag("DrWattsTalkTriggerOff"))
        {
            talk1Button.SetActive(false);
            talk2Button.SetActive(false);
            talk3Button.SetActive(false);
            talk4Button.SetActive(false);
        }

        //static boy
        if (collision.gameObject.CompareTag("StaticBoyTalkTrigger"))
        {
            interactButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("StaticBoyTalkTriggerOff"))
        {
            interactButton.SetActive(false);
            tryButton.SetActive(false);
        }

        //Finding Paradise cover
        if (collision.gameObject.CompareTag("FindingParadiseCover"))
        {
            dialogStaticBoyManager.currentDialog = 3;
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("HasFindingParadise", 1);
            findingParadiseScreen.SetActive(true);
        }

        //Hint tape
        if (collision.gameObject.CompareTag("HintTapeTrigger"))
        {
            playButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("HintTapeTriggerOff"))
        {
            playButton.SetActive(false);
        }


    }

    public void ActivateHintTape()
    {
        playerInfo.HintTapeActive = true;
    }




    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * playerInfo.speed * Time.deltaTime);
    }

    void MoveMap(Vector2 direction)
    {
        map.GetComponent<Transform>().Translate(direction * playerInfo.speed * Time.deltaTime);
    }
}
