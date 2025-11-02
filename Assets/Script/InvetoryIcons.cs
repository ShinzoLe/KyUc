using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InvetoryIcons : MonoBehaviour
{
    public Image[] emptyItems;

    public Sprite[] mainSlots;
    public Sprite[] miniSlots;
    public Sprite emptyIcon;

    public GameObject theCanvas;
    public GameObject inventoryObject;
    private int max;
    private int max2;
    public static bool iconUpdate;
    public static int newIcon = 0;
    public GameObject player;
    private MiniGameTrigger triggerMiniGame;
    void Start()
    {
        inventoryObject.SetActive(false);
        max = emptyItems.Length;
        triggerMiniGame=FindObjectOfType<MiniGameTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerMiniGame != null) Debug.Log("ko rong");
        if (inventoryObject.activeSelf) // mở visible con trỏ và ko cho xoay
        {
           // cam.SetActive(false);
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<StarterAssetsInputs>().cursorInputForLook = false;
            Cursor.visible = true;
            Cursor.lockState= CursorLockMode.None;
        }
        //else if(!MiniGameManager.canPlay || !inventoryObject.activeSelf)
        //{
        //   // cam.SetActive(true);
        //    player.GetComponent<PlayerInput>().enabled = true;
        //    player.GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        else if(triggerMiniGame.inMiniGame)
        {
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<StarterAssetsInputs>().cursorInputForLook = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (iconUpdate)
        {
            for (int i = 0; i < max; i++)
            {
                if (emptyItems[i].sprite == emptyIcon && ((newIcon==0)|| (newIcon == 1)||(newIcon == 2)|| (newIcon == 3))
                    && i <= 3)
                {
                    max = i;
                    emptyItems[i].sprite = mainSlots[newIcon];
                }

                if (emptyItems[i].sprite == emptyIcon && ((newIcon ==4) || (newIcon == 5) || (newIcon == 6) || (newIcon == 7))
                    && i >=4)
                {
                    max = i;
                    emptyItems[i].sprite = miniSlots[newIcon-4];
                    MiniGameManager.manualCount++;
                }
            }
            StartCoroutine(Reset());
        }





        if (Input.GetKeyUp(KeyCode.M))
        {

            if (inventoryObject.activeSelf)
            {
                inventoryObject.SetActive(false);
                Cursor.visible = false;
            }
            else
            {
                inventoryObject.SetActive(true);
                Cursor.visible = true;    
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(.1f);
        iconUpdate = false;
        max = emptyItems.Length;
    }



}
