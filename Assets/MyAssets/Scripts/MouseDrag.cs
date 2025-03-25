using DG.Tweening;
using RDG;
using System.Collections;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private Vector2 touchStart;
    private float dragThreshold = 20f;
    [HideInInspector] public bool isHorizontalMover;
    private MovableItem movableItem;
    private GameManager gameManager;

    private static MouseDrag currentDraggingObject = null;

    private void Start()
    {
        movableItem = GetComponent<MovableItem>();
        gameManager = FindFirstObjectByType<GameManager>();
        isHorizontalMover = transform.rotation.eulerAngles.y == 90;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    CheckTouchObject(touch);
                    break;

                case TouchPhase.Moved:
                    if (currentDraggingObject == this)
                    {
                        Vibration.Vibrate(10);
                        Vector2 touchEnd = touch.position;
                        Vector2 delta = touchEnd - touchStart;
                        if (delta.magnitude < dragThreshold) return;

                        string direction = "";
                        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                            direction = delta.x > 0 ? "Right" : "Left";
                        else
                            direction = delta.y > 0 ? "Forward" : "Back";

                        Debug.Log($"Dragged {gameObject.name} {direction}");
                        StartCoroutine(Move(direction));
                        currentDraggingObject = null; // Giải phóng quyền kéo
                    }
                    break;

                case TouchPhase.Ended:
                    if (currentDraggingObject == this)
                        currentDraggingObject = null;
                    break;
            }
        }
    }

    private void CheckTouchObject(Touch touch)
    {
        if (currentDraggingObject != null)
            return; // Đã có đối tượng khác đang kéo, bỏ qua

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Chỉ cho phép di chuyển nếu chạm đúng vào object này
                OnTouchDown();
            }
        }
    }

    private void OnTouchDown()
    {
        GetComponent<MovableItem>().suggestBorder.SetActive(false);

        if (BoosterManager.Instance.useBreakBooster)
        {
            BoosterManager.Instance.useBreakBooster = false;
            CheckWin();
            BoosterManager.Instance.StartBreakMoveable(gameObject);
        }

        if (gameManager.levelNo > 10 && gameManager.moveLimitValue <= 0)
            return;

        if (movableItem.canDrag && !movableItem.isLocked && !gameManager.isDragging)
        {
            currentDraggingObject = this;
        }
    }

    public IEnumerator Move(string dir)
    {
        Debug.Log("Move");
        bool isMoved = false;

        if (isHorizontalMover && (dir == "Forward" || dir == "Back"))
            yield break;
        if (!isHorizontalMover && (dir == "Right" || dir == "Left"))
            yield break;

        if (movableItem.CanMove(dir))
            gameManager.DecreseMoveLimitText();

        while (movableItem.CanMove(dir))
        {
            gameManager.isDragging = true;
            Vector3 moveOffset = Vector3.zero;
            isMoved = true;

            if (isHorizontalMover)
                moveOffset = (dir == "Right" ? transform.right : -transform.right) * 1;
            else
                moveOffset = (dir == "Forward" ? transform.right : -transform.right) * 1;

            yield return transform.DOMove(transform.position + moveOffset, 0.05f).SetEase(Ease.Linear).WaitForCompletion();
            gameManager.isDragging = false;
        }

        if (isMoved)
            AudioManager.Instance.Play("Move");
    }

    private void CheckWin()
    {
        if (FindFirstObjectByType<MovableItem>())
            Debug.Log("NotYet");
        else
            gameManager.Win();
    }
}
