using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickArea;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = joystick.transform.position;  // Keep track of joystick's original position.
        joystickRadius = joystickArea.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    public void PointerDown()
    {
        // Don't move the joystick when the area is touched. Keep its position fixed.
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        // Update the vector without changing the joystick's position.
        if (joystickDist < joystickRadius)
        {
            joystickVec = (dragPos - joystickTouchPos).normalized;
        }
        else
        {
            joystickVec = (dragPos - joystickTouchPos).normalized * joystickRadius;
        }
    }

    public void PointerUp()
    {
        // Reset the joystick vector to zero when touch is released.
        joystickVec = Vector2.zero;
    }
}
