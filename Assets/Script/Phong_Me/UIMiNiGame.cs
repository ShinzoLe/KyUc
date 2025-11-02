using System.Collections.Generic;
using UnityEngine;

public class UIMiNiGame : MonoBehaviour
{
    public GameObject[] UIRecipe;
    public GameObject[] UISlotDisplays;
    public GameObject UIPanel;
    private UIMiniGameSlot[] Slots;
    private Dictionary<int, int> recipe;
    private int cur=-1;

    private int lastCurrRecipe = -1;
    private bool lastCanPlay = false;

    
    private void Start()
    {
        //MiniGame.SetActive(false);
        ResetRecipe();
        ResetUI();
    }
    void Update()
    {
        var mgr = MiniGameManager.instance;
        if (mgr == null) return;

        recipe = mgr.recipe2;

        if ( UIPanel!=null &&!UIPanel.activeInHierarchy)
            return;
        // Chỉ bật/tắt UI khi currRecipe hoặc canPlay thay đổi tránh chớp chớp UI
        if (lastCurrRecipe != mgr.currRecipe || lastCanPlay != MiniGameManager.canPlay)
        {

            lastCurrRecipe = mgr.currRecipe;           
            lastCanPlay = MiniGameManager.canPlay;

            if (MiniGameManager.canPlay)
            {
                ResetRecipe();                         
                ResetUI();

                if (mgr.currRecipe >= 0 && mgr.currRecipe < UIRecipe.Length)
                {
                    UIRecipe[mgr.currRecipe].SetActive(true);   
                    
                }
                if (mgr.currRecipe >= 0 && mgr.currRecipe < UISlotDisplays.Length)
                {
                    UISlotDisplays[mgr.currRecipe].SetActive(true);  
                    cur = mgr.currRecipe;                              
                }
                
            }
            else
            {
                // Khi thoát mini game, đảm bảo tắt sạch UI và reset con trỏ
                ResetRecipe();
                ResetUI();
                cur = -1;                                       
            }
            
        }
        
        UpdateTextIngre(); 

    }



    private void UpdateTextIngre()
    {
        if (cur >= 0 && cur < UISlotDisplays.Length)
        {
            if (Slots == null || Slots.Length == 0)
                Slots = UISlotDisplays[cur].GetComponentsInChildren<UIMiniGameSlot>(true);
            if (Slots != null && recipe!=null)
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    // duyệt qua dictionary
                    foreach (KeyValuePair<int, int> j in recipe)
                    {
                        int id = Slots[i].idIngre;

                        // kiểm tra id xem có tồn tại ko
                        if (recipe.TryGetValue(id, out int amt))         
                        {
                            Slots[i].textIngre.text = (amt >= 0 ? amt : 0).ToString(); // gán lại số lượng nguyên liệu
                        }
                    }
                }
            }
        }
        
    }


    private void ResetRecipe()
    {
        for (int i = 0; i < UIRecipe.Length; i++)
        {
            UIRecipe[i].SetActive(false);
        }

    }
    private void ResetUI()
    {
        for (int i = 0; i < UISlotDisplays.Length; i++)
        {
            UISlotDisplays[i].SetActive(false);
        }
        Slots = null;// gán lại để tránh còn tham chiếu cũ
    }
}
