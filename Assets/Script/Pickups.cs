
using System;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private Outline outline;
    
   [SerializeField] private GameObject tip;
    public int number;

    private GameObject invertoryObject;
    private bool canPick;
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void Start()
    {
        tip = FindFirstObjectByType<Tip>(FindObjectsInactive.Include)?.gameObject;
        invertoryObject = GameObject.Find("InventoryCanvas");
        outline.enabled = false;
        tip.SetActive(false);
    }

    private void Update()
    {
        if (canPick)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {

                Display();
                Destroy(gameObject);
                tip.SetActive(false);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPick = true;
            tip.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           canPick = false;
           tip.SetActive(false);
        }
    }

    private void Display()
    {
        InvetoryIcons.newIcon = number;
        InvetoryIcons.iconUpdate = true;
    }

}
