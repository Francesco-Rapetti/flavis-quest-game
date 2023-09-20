using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerOri : MonoBehaviour
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

    [Header("LevelOri")]
    [SerializeField] GameObject oriScreen;
    [SerializeField] GameObject oriCover;

    [Header("Buttons")]
    [SerializeField] GameObject naruTalkButton1;
    [SerializeField] GameObject naruTalkButton2;
    [SerializeField] GameObject naruTalkButton3;
    [SerializeField] GameObject naruTalkButton4;
    [SerializeField] GameObject kuTalkButton1;
    [SerializeField] GameObject kuTalkButton2;
    [SerializeField] GameObject chocoboInteractButton;
    [SerializeField] GameObject isabelInteractButton;
    [SerializeField] GameObject strayInteractButton;
    [SerializeField] GameObject dogInteractButton;


    [Header("Dialogs")]
    [SerializeField] DialogManager naruDialogManager;
    [SerializeField] DialogManager kuDialogManager;
    


    

    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        naruTalkButton1.SetActive(false);
        naruTalkButton2.SetActive(false);
        naruTalkButton3.SetActive(false);
        naruTalkButton4.SetActive(false);
        kuTalkButton1.SetActive(false);
        kuTalkButton2.SetActive(false);
        chocoboInteractButton.SetActive(false);
        isabelInteractButton.SetActive(false);
        strayInteractButton.SetActive(false);
        dogInteractButton.SetActive(false);

        if (playerInfo.talkedToNaru2)
        {
            if (PlayerPrefs.GetInt("HasOri") == 0) oriCover.SetActive(true);
            if (PlayerPrefs.GetInt("HasOriKey") == 0) objKey.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //hint tape visibility
        /*if (playerInfo.HintTapeActive) hintTape.SetActive(true);
        else hintTape.SetActive(false);*/

        

        



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
            naruTalkButton1.SetActive(false);
            naruTalkButton2.SetActive(false);
            naruTalkButton3.SetActive(false);
            naruTalkButton4.SetActive(false);
            kuTalkButton1.SetActive(false);
            kuTalkButton2.SetActive(false);
            if (playerInfo.orator == "Naru") naruDialogManager.HandleUpdate();
            if (playerInfo.orator == "Ku") kuDialogManager.HandleUpdate();

            /*talk1Button.SetActive(false);
            talk2Button.SetActive(false);
            talk3Button.SetActive(false);
            talk4Button.SetActive(false);
            interactButton.SetActive(false);
            playButton.SetActive(false);*/

            /*if (playerInfo.orator == "Watts") dialogWattsManager.HandleUpdate();
            if (playerInfo.orator == "StaticBoy")
            {
                dialogStaticBoyManager.HandleUpdate();

                if (!playerInfo.enigmaSolved) tryButton.SetActive(true); 
                else tryButton.SetActive(false);
            }
            if (playerInfo.orator == "HintTape") dialogHintTapeManager.HandleUpdate();*/
            
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

        /*//set static boy dialog
        if (playerInfo.enigmaSolved && !playerInfo.hasFindingParadise)
        {
            dialogStaticBoyManager.currentDialog = 1;
        }
        if (playerInfo.hasFindingParadise)
        {
            dialogStaticBoyManager.currentDialog = 2;
        }*/
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
            PlayerPrefs.SetInt("HasOriKey", 1);
            Destroy(collision.gameObject);
        }

        //buttons
        //Naru
        if (collision.gameObject.CompareTag("NaruTalkTrigger"))
        {
            if (!(playerInfo.dogFound && playerInfo.chocoboFound && playerInfo.isabelFound && playerInfo.strayFound))
            {
                if (!playerInfo.talkedToNaru1)
                {
                    naruTalkButton1.SetActive(true);
                    naruDialogManager.currentDialog = 0;
                }
                if (playerInfo.talkedToNaru1)
                {
                    naruTalkButton2.SetActive(true);
                    naruDialogManager.currentDialog = 1;
                }
            }
            else
            {
                if (!playerInfo.talkedToNaru2)
                {
                    naruTalkButton3.SetActive(true);
                    naruDialogManager.currentDialog = 2;
                }
                if (playerInfo.talkedToNaru2)
                {
                    naruTalkButton4.SetActive(true);
                    naruDialogManager.currentDialog = 3;
                }
            }
        }
        if (collision.gameObject.CompareTag("NaruTalkTriggerOff"))
        {
            naruTalkButton1.SetActive(false);
            naruTalkButton2.SetActive(false);
            naruTalkButton3.SetActive(false);   
            naruTalkButton4.SetActive(false);
        }

        //Ku
        if (collision.gameObject.CompareTag("KuTalkTrigger"))
        {
            if (!(playerInfo.dogFound && playerInfo.chocoboFound && playerInfo.isabelFound && playerInfo.strayFound))
            {
                kuTalkButton1.SetActive(true);
                kuDialogManager.currentDialog = 0;
            }
            else
            {
                kuTalkButton2.SetActive(true);
                kuDialogManager.currentDialog = 1;
            }
        }
        if (collision.gameObject.CompareTag("KuTalkTriggerOff"))
        {
            kuTalkButton1.SetActive(false);
            kuTalkButton2.SetActive(false);
        }

        //chocobo
        if (collision.gameObject.CompareTag("ChocoboInteractTrigger"))
        {
            chocoboInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("ChocoboInteractTriggerOff"))
        {
            chocoboInteractButton.SetActive(false);
        }

        //Isabel
        if (collision.gameObject.CompareTag("IsabelInteractTrigger"))
        {
            isabelInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("IsabelInteractTriggerOff"))
        {
            isabelInteractButton.SetActive(false);
        }

        //stray
        if (collision.gameObject.CompareTag("StrayInteractTrigger"))
        {
            strayInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("StrayInteractTriggerOff"))
        {
            strayInteractButton.SetActive(false);
        }

        //dog
        if (collision.gameObject.CompareTag("DogInteractTrigger"))
        {
            dogInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("DogInteractTriggerOff"))
        {
            dogInteractButton.SetActive(false);
        }

        //Ori cover
        if (collision.gameObject.CompareTag("OriCover"))
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("HasOri", 1);
            oriScreen.SetActive(true);
        }

        /*//DrWatts
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
        }*/
        /*if (collision.gameObject.CompareTag("DrWattsTalkTriggerOff"))
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
            playerInfo.hasFindingParadise = true;
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
        }*/


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
