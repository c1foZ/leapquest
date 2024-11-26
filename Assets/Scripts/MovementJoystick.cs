using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickArea;
    public Vector2 joystickVec; 
    private Vector2 joystickTouchPos; 
    private float joystickRadius; 
    private readonly Vector2 exactPosition = new Vector2(100, 130); 

    void Start()
    {
        joystickRadius = joystickArea.GetComponent<RectTransform>().sizeDelta.y / 2;

        ResetToExactPosition();
    }

    public void PointerDown()
    {
        Vector2 mousePos = Input.mousePosition;
        joystick.GetComponent<RectTransform>().position = mousePos;
        joystickArea.GetComponent<RectTransform>().position = mousePos;
        joystickTouchPos = mousePos;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        joystickVec = (dragPos - joystickTouchPos).normalized;
        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.GetComponent<RectTransform>().position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.GetComponent<RectTransform>().position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        ResetToExactPosition();
    }

    private void ResetToExactPosition()
    {
        joystick.GetComponent<RectTransform>().anchoredPosition = exactPosition;
        joystickArea.GetComponent<RectTransform>().anchoredPosition = exactPosition;
    }
}
