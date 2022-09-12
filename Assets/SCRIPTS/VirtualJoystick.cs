using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform stick;
    [SerializeField] Image background = null;

    public string player = "";
    public float limit = 250;

    public void OnPointerDown(PointerEventData eventData)
    {
        background.color = Color.red;
        stick.anchoredPosition = ConvertToLocal(eventData);
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = ConvertToLocal(eventData);

        if (pos.magnitude > limit)
        {
            pos = pos.normalized * limit;
        }
        stick.anchoredPosition = pos;

        float x = pos.x / limit;
        float y = pos.y / limit;

        SetHorizontal(x);
        SetVertical(y);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        background.color = Color.gray;
        stick.anchoredPosition = Vector2.zero;
        SetHorizontal(0);
        SetVertical(0);
    }

    void OnDisable()
    {
        SetHorizontal(0);
        SetVertical(0);
    }

    void SetHorizontal(float val)
    {
        InputManager.Instance.SetAxis("Horizontal" + player, val);
    }

    void SetVertical(float val)
    {
        InputManager.Instance.SetAxis("Vertical" + player, val);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
