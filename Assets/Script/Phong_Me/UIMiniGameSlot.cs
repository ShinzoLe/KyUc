using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniGameSlot : MonoBehaviour
{
    public int idRecipe;
    public int idIngre;
    public Text textIngre;

    void Start()
    {
        textIngre =gameObject.GetComponentInChildren<Text>();
    }

}
