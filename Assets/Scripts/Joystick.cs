using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[Serializable]
public class Joystick : MonoBehaviour
{
    public Joystick()
    {
        this.deadZone = Vector2.zero;
        this.lastFingerId = -1;
        this.firstDeltaTime = 0.5f;
        this.guiBoundary = new Boundary();
    }

    public virtual void Start()
    {
        this.gui = (Image)this.GetComponent(typeof(Image));
        RectTransform rectTransform = gui.GetComponent<RectTransform>(); // Lấy RectTransform của Image
        this.defaultRect = rectTransform.rect; // Lưu trữ Rect ban đầu

        // Điều chỉnh vị trí dựa trên transform của joystick
        rectTransform.anchoredPosition = new Vector2(this.transform.position.x * Screen.width, this.transform.position.y * Screen.height);

        // Khởi tạo các biến liên quan đến touchPad nếu có
        if (this.touchPad)
        {
            if (this.gui.sprite) // Kiểm tra sprite của Image thay vì texture cũ
            {
                this.touchZone = this.defaultRect;
            }
        }
        else
        {
            this.guiTouchOffset.x = this.defaultRect.width * 0.5f;
            this.guiTouchOffset.y = this.defaultRect.height * 0.5f;
            this.guiCenter.x = this.defaultRect.x + this.guiTouchOffset.x;
            this.guiCenter.y = this.defaultRect.y + this.guiTouchOffset.y;
            this.guiBoundary.min.x = this.defaultRect.x - this.guiTouchOffset.x;
            this.guiBoundary.max.x = this.defaultRect.x + this.guiTouchOffset.x;
            this.guiBoundary.min.y = this.defaultRect.y - this.guiTouchOffset.y;
            this.guiBoundary.max.y = this.defaultRect.y + this.guiTouchOffset.y;
        }
    }

    public virtual void Disable()
    {
        this.gameObject.SetActive(false);
        Joystick.enumeratedJoysticks = false;
    }

    public virtual void ResetJoystick()
    {
        // Thay đổi pixelInset thành RectTransform
        RectTransform rectTransform = this.gui.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(this.defaultRect.x, this.defaultRect.y); // Reset vị trí

        this.lastFingerId = -1;
        this.position = Vector2.zero;
        this.fingerDownPos = Vector2.zero;
        if (this.touchPad)
        {
            float a = 0.025f;
            Color color = this.gui.color;
            color.a = a;
            this.gui.color = color;
        }
    }

    public virtual bool IsFingerDown()
    {
        return this.lastFingerId != -1;
    }

    public virtual void LatchedFinger(int fingerId)
    {
        if (this.lastFingerId == fingerId)
        {
            this.ResetJoystick();
        }
    }

    public virtual void Update()
    {
        if (!Joystick.enumeratedJoysticks)
        {
            Joystick.joysticks = (((Joystick[])UnityEngine.Object.FindObjectsOfType(typeof(Joystick))) as Joystick[]);
            Joystick.enumeratedJoysticks = true;
        }

        int touchCount = UnityEngine.Input.touchCount;
        if (this.tapTimeWindow > 0)
        {
            this.tapTimeWindow -= Time.deltaTime;
        }
        else
        {
            this.tapCount = 0;
        }

        if (touchCount == 0)
        {
            this.ResetJoystick();
        }
        else
        {
            for (int i = 0; i < touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);
                Vector2 vector = touch.position - this.guiTouchOffset;
                bool flag = false;

                if (this.touchPad)
                {
                    if (this.touchZone.Contains(touch.position))
                    {
                        flag = true;
                    }
                }
                else
                {
                    // Lấy RectTransform của Image
                    RectTransform rectTransform = this.gui.GetComponent<RectTransform>();

                    // Chuyển đổi vị trí màn hình của touch.position thành không gian địa phương của RectTransform
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, null, out localPoint);

                    // Kiểm tra xem điểm localPoint có nằm trong rect của RectTransform không
                    if (rectTransform.rect.Contains(localPoint))
                    {
                        flag = true;
                    }
                }


                if (flag && (this.lastFingerId == -1 || this.lastFingerId != touch.fingerId))
                {
                    if (this.touchPad)
                    {
                        float a = 0.15f;
                        Color color = this.gui.color;
                        color.a = a;
                        this.gui.color = color;
                        this.lastFingerId = touch.fingerId;
                        this.fingerDownPos = touch.position;
                        this.fingerDownTime = Time.time;
                    }
                    this.lastFingerId = touch.fingerId;
                    if (this.tapTimeWindow > 0)
                    {
                        this.tapCount++;
                    }
                    else
                    {
                        this.tapCount = 1;
                        this.tapTimeWindow = Joystick.tapTimeDelta;
                    }
                    int j = 0;
                    Joystick[] array = Joystick.joysticks;
                    int length = array.Length;
                    while (j < length)
                    {
                        if (array[j] != this)
                        {
                            array[j].LatchedFinger(touch.fingerId);
                        }
                        j++;
                    }
                }

                if (this.lastFingerId == touch.fingerId)
                {
                    if (touch.tapCount > this.tapCount)
                    {
                        this.tapCount = touch.tapCount;
                    }
                    if (this.touchPad)
                    {
                        this.position.x = Mathf.Clamp((touch.position.x - this.fingerDownPos.x) / (this.touchZone.width / 2), -1, 1);
                        this.position.y = Mathf.Clamp((touch.position.y - this.fingerDownPos.y) / (this.touchZone.height / 2), -1, 1);
                    }
                    else
                    {
                        float x = Mathf.Clamp(vector.x, this.guiBoundary.min.x, this.guiBoundary.max.x);
                        RectTransform rectTransform = this.gui.GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);

                        float y = Mathf.Clamp(vector.y, this.guiBoundary.min.y, this.guiBoundary.max.y);
                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        this.ResetJoystick();
                    }
                }
            }
        }

        if (!this.touchPad)
        {
            this.position.x = (this.gui.rectTransform.position.x + this.guiTouchOffset.x - this.guiCenter.x) / this.guiTouchOffset.x;
            this.position.y = (this.gui.rectTransform.position.y + this.guiTouchOffset.y - this.guiCenter.y) / this.guiTouchOffset.y;
        }

        float num4 = Mathf.Abs(this.position.x);
        float num5 = Mathf.Abs(this.position.y);
        if (num4 < this.deadZone.x)
        {
            this.position.x = 0;
        }
        else if (this.normalize)
        {
            this.position.x = Mathf.Sign(this.position.x) * (num4 - this.deadZone.x) / (1 - this.deadZone.x);
        }

        if (num5 < this.deadZone.y)
        {
            this.position.y = 0;
        }
        else if (this.normalize)
        {
            this.position.y = Mathf.Sign(this.position.y) * (num5 - this.deadZone.y) / (1 - this.deadZone.y);
        }
    }

    public virtual void Main()
    {
    }

    [NonSerialized]
    private static Joystick[] joysticks;

    [NonSerialized]
    private static bool enumeratedJoysticks;

    [NonSerialized]
    private static float tapTimeDelta = 0.3f;

    public bool touchPad;

    public Rect touchZone;

    public Vector2 deadZone;

    public bool normalize;

    public Vector2 position;

    public int tapCount;

    private int lastFingerId;

    private float tapTimeWindow;

    private Vector2 fingerDownPos;

    private float fingerDownTime;

    private float firstDeltaTime;

    private Image gui;

    private Rect defaultRect;

    private Boundary guiBoundary;

    private Vector2 guiTouchOffset;

    private Vector2 guiCenter;
}
