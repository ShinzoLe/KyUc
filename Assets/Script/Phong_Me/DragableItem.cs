using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image imageUI;                 // icon trên UI
  
    Transform startParent;
    Vector2 startAnchoredPos;

    [Header("3D")]
    public GameObject item3dPrefab;       // gán prefab 3D tương ứng
    public LayerMask dragSurface = ~0;    // layer bề mặt được phép đặt
    public float surfaceOffset = 0.01f;   // nhấc nhẹ khỏi mặt bàn
    public float fallbackDistance = 2.0f; // khoảng cách trước camera khi chưa raycast trúng
    public float dropDistance = -0.5f;
    GameObject dragging3D;

    [Header("Items")]
    public bool pumpKin;
    public bool meat;
    public bool tomato;
    public bool carrot;
    public bool eggplant;
    public bool fish;
    public bool garlic;
    public bool mushroom;
    public bool sausages;
    public bool pepper;


    public void OnBeginDrag(PointerEventData eventData)
    {

        // Spawn ghost 3D
        if (eggplant || pumpKin || meat || pepper ||sausages)
        {

            dragging3D = Instantiate(item3dPrefab);
        }
        else
        {
             dragging3D = Instantiate(item3dPrefab,transform.position, Quaternion.Euler(90f, 0f, 0f));
        }
        MoveGhostToMouse();   // đặt vị trí ban đầu
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Không di chuyển UI nữa, chỉ di chuyển object 3D
        MoveGhostToMouse();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Kiểm tra điểm thả
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000f, dragSurface))
        {
            dragging3D.transform.position = hit.point + hit.normal * dropDistance;
            var droppedItem = hit.collider.gameObject.GetComponent<DropTarget>();
            if (droppedItem != null)
            {
                droppedItem.OnDropped(dragging3D);
            }
            //  Destroy(dragging3D);
        }
        else
        {
            //  hủy 3D và trả UI về chỗ cũ
            if (dragging3D) Destroy(dragging3D);
            
        }

      
     
    }

    void MoveGhostToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000f, dragSurface))
        {
            dragging3D.transform.position = hit.point + hit.normal * surfaceOffset;
           
        }
        else
        {
            // Không trúng gì: đặt tạm trước camera
            dragging3D.transform.position = ray.GetPoint(fallbackDistance);
        }
    }

    
}
