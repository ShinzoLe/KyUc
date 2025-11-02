
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;
    public static int manualCount = 0;
    public static bool canPlay; // nhớ set lại khi xong nha cha
    public static GameObject curItem;

    public int currRecipe;
    public RecipeScript[] recipe;
    public Text textUI;


    private int max;
    private int key;
    private int value;
    private bool doneRecipe;
    public Dictionary<int, int> recipe2;

    private List<int> currIDIngre;
    [SerializeField] private MiniGameTrigger miniGameTrigger;

   [HideInInspector] public bool isExisting = false; // ngăn cho UI tắt mà ko bị chớp 
    //Sử dụng design pattern singleton
    private void Awake()
    {
        // Nếu đã có 1 instance khác tồn tại thì xóa cái mới
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Gán instance hiện tại
        instance = this;

        // Giữ lại khi chuyển scene
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // lấy length nguyên liệu trong scriptable object
        max = recipe[0].ingredients.Length;
        BuildRecipe();
    }

    void BuildRecipe()
    {
        doneRecipe = false;
        recipe2 = new Dictionary<int, int>();
        currIDIngre = new List<int>();
        if (currRecipe >= 0 && currRecipe < recipe.Length)
        {
            for (int j = 0; j < max; j++)
            {
                int key = recipe[currRecipe].ingredients[j].idIngre;
                int value = recipe[currRecipe].ingredients[j].ingerAmt;
                recipe2[key] = value;
            }
        }
      
    }

    void Update()
    {
        if (manualCount >= 4)
        {
           AllowPlayOnce(); // tránh lặp lại liên tục mỗi fame
        }
        if (currRecipe >= 4 && !isExisting)
        {
            isExisting = true;
            doneRecipe = false;
            StartCoroutine(DelayText("Hoàn thành 100%"));
            StartCoroutine(ExitMiniGame());
        }
    }

    public void AllowPlayOnce()
    {
        if (!canPlay && !isExisting)
            canPlay = true;
    }
    IEnumerator DelayText(string mess)
    {
        textUI.transform.gameObject.SetActive(true);
        textUI.text = mess;
        yield return new WaitForSeconds(1.5f);
        textUI.transform.gameObject.SetActive(false);
    }

    IEnumerator ExitMiniGame()
    {
        yield return new WaitForSeconds(1.5f);
        manualCount = 0;
        canPlay = false;
        miniGameTrigger.ExitMiniGame();
    }

    public void CheckIngre(int item)
    {
        if (recipe2.ContainsKey(item))
        {
            int amtIngre = recipe2[item] - 1;
            recipe2[item] = amtIngre;

        }
        else
        {
            StartCoroutine(DelayText("không có vật phẩm trong công thức"));
        }
        CheckComplete();
    }

    private void CheckComplete()
    {
        foreach (var i in recipe2)
        {
            if (i.Value == 0)
            {
                doneRecipe = true;

                // tránh trùng thêm id nguyên liệu đã về 0 vào list
                if (!currIDIngre.Contains(i.Key))
                {
                    currIDIngre.Add(i.Key);

                }
                Debug.Log("count " + currIDIngre.Count);
            }
            else if (i.Value < 0)
            {
                doneRecipe = false;
                StartCoroutine(DelayText("Sai rồi. Hãy thử lại"));
                currRecipe = 0;
                BuildRecipe();
                return;
            }
            else
            {
                doneRecipe = false;
                //Debug.Log("chưa đủ");
            }
        }
        CheckAmtIngre();

    }

    private void CheckAmtIngre()
    {

        if (doneRecipe && currIDIngre.Count >= 4)
        {
            StartCoroutine(BuildDelay(0.2f));
            currRecipe++;

            StartCoroutine(DelayText(" Đã hoàn thành xong công thức " + currRecipe));
            //Debug.Log(" da doan thanh xong cong thuc " + currRecipe);
            if (currRecipe < recipe.Length)
                BuildRecipe();
        }


    }
    IEnumerator BuildDelay(float time)
    {
        yield return new WaitForSeconds(time);

    }
}
