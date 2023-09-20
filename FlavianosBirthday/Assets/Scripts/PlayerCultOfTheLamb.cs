using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class PlayerCultOfTheLamb : MonoBehaviour
{
    private Transform player;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Animator animator;
    private bool control = false;
    
    public PlayerInfo playerInfo;
    public AnimatorOverrideController duck;
    public AnimatorOverrideController flaviano;
    public Transform circle;
    public Transform outerCircle;
    public GameObject keyUI;
    public Camera cam;
    public GameObject objKey;

    [Header("LevelCultOfTheLamb")]
    [HideInInspector]
    public Vector2 startPos;
    [SerializeField] Animator transition;
    [SerializeField] GameObject cultOfTheLambCover;
    [SerializeField] GameObject cultOfTheLambScreen;
    [SerializeField] GameObject gunFlame;
    [SerializeField] GameObject hardDiskFlame;
    [SerializeField] GameObject branchFlame;
    [SerializeField] GameObject eyeballFlame;
    [SerializeField] GameObject platypusFlame;
    [SerializeField] GameObject blueFlame;

    [Header("Buttons")]
    [SerializeField] GameObject talkButton;
    [SerializeField] GameObject gunInteractButton;
    [SerializeField] GameObject hardDiskInteractButton;
    [SerializeField] GameObject branchInteractButton;
    [SerializeField] GameObject eyeballInteractButton;
    [SerializeField] GameObject platypusInteractButton;
    [SerializeField] GameObject gunPlaceButton;
    [SerializeField] GameObject hardDiskPlaceButton;
    [SerializeField] GameObject branchPlaceButton;
    [SerializeField] GameObject eyeballPlaceButton;
    [SerializeField] GameObject platypusPlaceButton;
    [SerializeField] GameObject sacrificeButton;
    [SerializeField] GameObject openDoorButton;

    [Header("Dialogs")]
    [SerializeField] DialogManager edmundDialogManager;








    public void SetHaveKey(bool value)
    {
        if (value) PlayerPrefs.SetInt("HaveKey", 1);
        else PlayerPrefs.SetInt("HaveKey", 0);
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();

        


        talkButton.SetActive(false);
        gunInteractButton.SetActive(false);
        hardDiskInteractButton.SetActive(false);
        branchInteractButton.SetActive(false);
        eyeballInteractButton.SetActive(false);
        platypusInteractButton.SetActive(false);

        gunPlaceButton.SetActive(false);
        hardDiskPlaceButton.SetActive(false);
        branchPlaceButton.SetActive(false);
        eyeballPlaceButton.SetActive(false);
        platypusPlaceButton.SetActive(false);

        openDoorButton.SetActive(false);

        gunFlame.SetActive(false);
        branchFlame.SetActive(false);
        hardDiskFlame.SetActive(false);
        eyeballFlame.SetActive(false);
        platypusFlame.SetActive(false);
        blueFlame.SetActive(false);

        if (PlayerPrefs.GetInt("HasCultOfTheLamb") == 1 && cultOfTheLambCover != null)
        {
            cultOfTheLambCover.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HasCultOfTheLambKey") == 1 && objKey != null)
        {
            objKey.SetActive(false);
        }

        if (playerInfo.sacrificedDone)
        {
            if (PlayerPrefs.GetInt("HasCultOfTheLamb") == 0) cultOfTheLambCover.SetActive(true);
        }

        
        
    }

    private void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

        //Edmund dialog
        if (edmundDialogManager.currentDialog == 0 && edmundDialogManager.currentLine >= 1) control = true;
        if (!playerInfo.isTalking && control)
        {
            control = false;
            edmundDialogManager.currentDialog = 1;
        }
        if (playerInfo.hasGun && playerInfo.hasHardDisk && playerInfo.hasBranch && playerInfo.hasEyeball && playerInfo.hasPlatypus && !playerInfo.sacrificedDone)
        {
            edmundDialogManager.currentDialog = 2;
        }
        if (playerInfo.placedGun && playerInfo.placedHardDisk && playerInfo.placedBranch && playerInfo.placedEyeball && playerInfo.placedPlatypus && !playerInfo.sacrificedDone)
        {
            edmundDialogManager.currentDialog = 3;
        }
        if (playerInfo.sacrificedDone && PlayerPrefs.GetInt("HasCultOfTheLambKey") == 0)
        {
            edmundDialogManager.currentDialog = 4;
        }
        if (PlayerPrefs.GetInt("HasCultOfTheLambKey") == 1)
        {
            edmundDialogManager.currentDialog = 5;
        }
        

        /*//werewolf dialog
        if (!playerInfo.mazeFinished)
        {
            werewolfDialogManager.currentDialog = 0;
        }
        if (playerInfo.mazeFinished)
        {
            werewolfDialogManager.currentDialog = 1;
        }*/

        //sacrifice button
        if (playerInfo.placedGun && playerInfo.placedHardDisk && playerInfo.placedBranch && playerInfo.placedEyeball && playerInfo.placedPlatypus && !playerInfo.sacrificedDone)
        {
            sacrificeButton.SetActive(true);
        }
        else
        {
            sacrificeButton.SetActive(false);
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
            talkButton.SetActive(false);
            if (playerInfo.orator == "Edmund") edmundDialogManager.HandleUpdate();

            /*talkButton.SetActive(false);
            if (playerInfo.orator == "Werewolf") werewolfDialogManager.HandleUpdate();
            */

            
            
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
            PlayerPrefs.SetInt("HasCultOfTheLambKey", 1);
            Destroy(collision.gameObject);
        }

        //Cult Of The Lamb cover
        if (collision.gameObject.CompareTag("CultOfTheLambCover"))
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("HasCultOfTheLamb", 1);
            cultOfTheLambScreen.SetActive(true);
        }

        //buttons
        //Edmund
        if (collision.gameObject.CompareTag("EdmundTalkTrigger"))
        {
            talkButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("EdmundTalkTriggerOff"))
        {
            talkButton.SetActive(false);
        }

        //gun
        if (collision.gameObject.CompareTag("GunInteractTrigger"))
        {
            gunInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("GunInteractTriggerOff"))
        {
            gunInteractButton.SetActive(false);
        }

        //gun drop button
        if (collision.gameObject.CompareTag("GunPlaceTrigger"))
        {
            gunPlaceButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("GunPlaceTriggerOff"))
        {
            gunPlaceButton.SetActive(false);
        }

        //Hard disk
        if (collision.gameObject.CompareTag("HardDiskInteractTrigger"))
        {
            hardDiskInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("HardDiskInteractTriggerOff"))
        {
            hardDiskInteractButton.SetActive(false);
        }

        //hard disk drop button
        if (collision.gameObject.CompareTag("HardDiskPlaceTrigger"))
        {
            hardDiskPlaceButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("HardDiskPlaceTriggerOff"))
        {
            hardDiskPlaceButton.SetActive(false);
        }

        //branch
        if (collision.gameObject.CompareTag("BranchInteractTrigger"))
        {
            branchInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("BranchInteractTriggerOff"))
        {
            branchInteractButton.SetActive(false);
        }

        //branch drop button
        if (collision.gameObject.CompareTag("BranchPlaceTrigger"))
        {
            branchPlaceButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("BranchPlaceTriggerOff"))
        {
            branchPlaceButton.SetActive(false);
        }

        //eyeball
        if (collision.gameObject.CompareTag("EyeballInteractTrigger"))
        {
            eyeballInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("EyeballInteractTriggerOff"))
        {
            eyeballInteractButton.SetActive(false);
        }

        //eyeball drop button
        if (collision.gameObject.CompareTag("EyeballPlaceTrigger"))
        {
            eyeballPlaceButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("EyeballPlaceTriggerOff"))
        {
            eyeballPlaceButton.SetActive(false);
        }

        //platypus
        if (collision.gameObject.CompareTag("PlatypusInteractTrigger"))
        {
            platypusInteractButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("PlatypusInteractTriggerOff"))
        {
            platypusInteractButton.SetActive(false);
        }

        //platypus drop button
        if (collision.gameObject.CompareTag("PlatypusPlaceTrigger"))
        {
            platypusPlaceButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("PlatypusPlaceTriggerOff"))
        {
            platypusPlaceButton.SetActive(false);
        }

        //door open button
        if (collision.gameObject.CompareTag("DoorTrigger"))
        {
            if (PlayerPrefs.GetInt("HasCultOfTheLamb") == 1 && PlayerPrefs.GetInt("HasCultOfTheLambKey") == 1) openDoorButton.SetActive(true);
        }
        if (collision.gameObject.CompareTag("DoorTriggerOff"))
        {
            openDoorButton.SetActive(false);
        }



    }


    public void RitualBegins()
    {
        StartCoroutine(Flames());
    }

    IEnumerator Flames()
    {
        gunFlame.SetActive(true);
        hardDiskFlame.SetActive(true);
        branchFlame.SetActive(true);
        eyeballFlame.SetActive(true);
        platypusFlame.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        blueFlame.SetActive(true);
    }

 


    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * playerInfo.speed * Time.deltaTime);
    }

    public void TalkToEdmund()
    {
        if (playerInfo.sacrificedDone && objKey != null)
        {
            objKey.SetActive(true);
        }
    }

    public void FinishTheGame()
    {
        PlayerPrefs.SetInt("GameFinished", 1);
        PlayerPrefs.SetInt("KeysFinished", 1);
    }

    
}
