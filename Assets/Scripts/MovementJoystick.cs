using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickArea;
    public Vector2 joystickVec; // Movement direction vector
    private Vector2 joystickTouchPos; // Position where touch started
    private float joystickRadius; // Maximum drag distance
    private readonly Vector2 exactPosition = new Vector2(100, 130); // Reset position

    void Start()
    {
        // Calculate joystick radius based on the size of the joystick area
        joystickRadius = joystickArea.GetComponent<RectTransform>().sizeDelta.y / 2;

        // Set both joystick and joystick area to the exact starting position
        ResetToExactPosition();
    }

    public void PointerDown()
    {
        // Set the joystick and joystick area to the current pointer position
        Vector2 mousePos = Input.mousePosition;
        joystick.GetComponent<RectTransform>().position = mousePos;
        joystickArea.GetComponent<RectTransform>().position = mousePos;
        joystickTouchPos = mousePos;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        // Calculate drag direction and distance
        joystickVec = (dragPos - joystickTouchPos).normalized;
        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        // Move joystick within the radius limit
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
        // Reset joystick vector and positions to the exact position
        joystickVec = Vector2.zero;
        ResetToExactPosition();
    }

    private void ResetToExactPosition()
    {
        // Use anchoredPosition to reset to the exact UI space position
        joystick.GetComponent<RectTransform>().anchoredPosition = exactPosition;
        joystickArea.GetComponent<RectTransform>().anchoredPosition = exactPosition;
    }
}
