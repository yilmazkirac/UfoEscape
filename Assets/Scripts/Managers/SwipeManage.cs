using UnityEngine;

public class SwipeManage : MonoBehaviour
{
  public static bool tab,swipeLeft,swipeRight,swipeUp,swipeDown;
    private bool isDraging=false;
    private Vector2 startTouch, swipeDelta;
    void Update()
    {
        tab = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        if (Input.GetMouseButtonDown(0))
        {
            tab = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        if (Input.touches.Length>0)
        {
            if (Input.touches[0].phase==TouchPhase.Began)
            {
                tab = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
           else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
       
        }
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length<0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }         
        }
        if (swipeDelta.magnitude>125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x)> Mathf.Abs(y))
            {
                if (x<0)
                {
                    swipeLeft = true;
                }
                else
                {
                       swipeRight = true;
                }
            }
            else {
                if (y<0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            Reset();
        }
    }
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging= false;
    }
}
