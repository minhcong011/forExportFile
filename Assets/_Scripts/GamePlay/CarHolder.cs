using UnityEngine;

public class CarHolder : MonoBehaviour
{
    [SerializeField] private GameObject lockBg;
    public bool isLook;
    public Transform entrancePos;
    private bool isHaveCar;
    public bool IsHaveCar { get { return isHaveCar; } set { isHaveCar = value; } }

    private bool isChoose;
    public bool IsChoose { get { return isChoose; } set { isChoose = value; } }
    private void Start()
    {
        ShowLockBg();
    }
    private void ShowLockBg()
    {
        if (isLook) lockBg.SetActive(true);
    }
    public void Unlock()
    {
        isLook = false;
        lockBg.SetActive(false);
    }
    public void ResetHolder()
    {
        IsHaveCar = false;
        IsChoose = false;
    }
}
