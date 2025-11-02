using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class MiniGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject annoucedText;
    [SerializeField] private GameObject cameraFollow;
    [SerializeField] private GameObject minigameCamera;
    [SerializeField] private GameObject openPot;
    [SerializeField] private GameObject miniGame;

    public GameObject player;
    private PlayerInput playerInput;
    private StarterAssetsInputs starterInput;
    public bool inMiniGame;
    public bool hasEntered = false;
    private bool uiShown = false;
    private void Awake()
    {
        //    player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
        starterInput = player.GetComponent<StarterAssetsInputs>();
    }
    private void Start()
    {
        annoucedText.SetActive(false);

        minigameCamera.SetActive(false);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!MiniGameManager.canPlay && !MiniGameManager.instance.isExisting)
            {

                annoucedText.SetActive(true);

            }
            else
            {
                player.GetComponent<raycheck>().enabled = false;
                if (!hasEntered)
                {
                    inMiniGame = true;
                    EnterMiniGame();
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        annoucedText.SetActive(false);
        if (hasEntered)
        {
            inMiniGame = false;
            ExitMiniGame();
        }
    }

    private void EnterMiniGame()
    {
        if (hasEntered) return;
        hasEntered = true;
        cameraFollow.SetActive(false);
        minigameCamera.SetActive(true);
        openPot.SetActive(false);
        // Tắt toàn bộ input nhân vật
        playerInput.enabled = false;

        // Không cho chuột điều khiển nhìn + NGĂN StarterAssets tự khoá chuột lại
        starterInput.cursorInputForLook = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(LoadUI());
    }
    IEnumerator LoadUI()
    {
        yield return new WaitForSeconds(2);
        if (!uiShown)
        {
            miniGame.SetActive(true);
            uiShown = true;
        }
    }

    // có sử dụng trong invoke kìa cha ơi quên quài





    public void ExitMiniGame()
    {
        if (!hasEntered) return;
        hasEntered = false;
        inMiniGame = false;
        minigameCamera.SetActive(false);
        cameraFollow.SetActive(true);
        openPot.SetActive(true);
        
        playerInput.enabled = true;

        starterInput.cursorInputForLook = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(DisableUI());
    }
    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(1);

        if (uiShown)
        {
            miniGame.SetActive(false);
            uiShown = false;
        }
    }
}
