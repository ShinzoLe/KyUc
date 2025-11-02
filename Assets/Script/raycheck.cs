using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycheck : MonoBehaviour
{
    public float rayDistance = 10f; // Khoảng cách tia ray sẽ kiểm tra
    public LayerMask layerMask; // Lớp va chạm của vật phẩm

    private GameObject curPickUp;
    void Update()
    {
        
            // Tạo tia raycast từ vị trí của camera hướng về phía trước
             Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);   
       

        RaycastHit hit; // Kết quả va chạm

        // Kiểm tra va chạm với vật thể trên lớp layerMask
        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            // Nếu va chạm, bạn có thể xử lý tại đây
           // Debug.Log("Va chạm với vật phẩm: " + hit.collider.name);
            if (hit.collider.CompareTag("Item"))
            {
                curPickUp = hit.collider.gameObject;
                if (MiniGameManager.curItem == null)
                {
                    MiniGameManager.curItem = curPickUp;
                }
                else if (curPickUp.GetComponent<Pickups>().number == MiniGameManager.curItem.GetComponent<Pickups>().number)
                {
                    MiniGameManager.curItem.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    MiniGameManager.curItem.GetComponent <Outline>().enabled = false;
                    MiniGameManager.curItem = curPickUp;
                }

            }
            else 
            {
                if (MiniGameManager.curItem != null)
                {
                    MiniGameManager.curItem.GetComponent<Outline>().enabled=false;
                }
            }

        }

        // Vẽ tia raycast trong Scene view để dễ điều chỉnh
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }
}
