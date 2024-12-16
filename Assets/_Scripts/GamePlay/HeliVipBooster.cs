using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeliVipBooster : SingletonBase<HeliVipBooster>
{
    [SerializeField] private float speedMove;
    [SerializeField] private float speedRotate;
    [SerializeField] private Vector3 offSetInSky;
    [SerializeField] private Vector3 offSetInSkyLanding;
    [SerializeField] private Transform exitPos;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform carGp;
    [SerializeField] private Transform vipHolder;
    [SerializeField] private GameObject flag;
    [SerializeField] private GameObject buyBoosterPanel;
    [SerializeField] private GameObject adsIcon;
    [SerializeField] private TextMeshProUGUI boosterAmountText;
    [SerializeField] private GameObject boosterAmountTextGp;
    [SerializeField] private Transform boosterAmountTextPos;
    private void Start()
    {
        if(GameCache.GC.vipSlotBoosterAmount > 0)
        {
            boosterAmountTextGp.SetActive(true);
            boosterAmountText.text = GameCache.GC.vipSlotBoosterAmount.ToString();
            StartCoroutine(UpdateBoosterAmountTextPos());
        }
        else
        {
            adsIcon.SetActive(true);
        }
    }
    public void CheckBoosterAvailble()
    {
        if (GameController.Instance.IsUseBooster) return;
        if (CarManager.Instance.carIsMove.Count > 0) return;
        if (GameCache.GC.vipSlotBoosterAmount > 0)
        {
            if(GameCache.GC.vipSlotBoosterAmount == 0)
            {
                boosterAmountTextGp.SetActive(false);
                adsIcon.SetActive(true);
            }
            BoosterManager.Instance.UseBooster(3);
            boosterAmountText.text = GameCache.GC.vipSlotBoosterAmount.ToString();
        }
        else
        {
            buyBoosterPanel.SetActive(true);
        }
    }
    public IEnumerator PutCarToVipHolder(Transform carToPut)
    {
        Coroutine moveCarCoroutinue;
        CarController carController = carToPut.GetComponent<CarController>();
        GameController.Instance.PauseGame();
        carController.CurrentHolder = vipHolder.GetComponent<CarHolder>();

        if (carController.IsCarInConveyorBetl) carController.ConveyorBetlParent.RemoveCar(carController.gameObject);

        vipHolder.gameObject.SetActive(true);
        flag.SetActive(false);
        transform.LookAt(carToPut);
        yield return MoveToTarget(carToPut.position, offSetInSky);
        yield return MoveToTarget(carToPut.position, offSetInSkyLanding);
        yield return RotateTowardsTarget(carToPut);
        moveCarCoroutinue = StartCoroutine(MoveCar(carToPut));
        yield return MoveToTarget(carToPut.position, offSetInSky);

        transform.LookAt(vipHolder.position + offSetInSky);
        yield return MoveToTarget(vipHolder.position, offSetInSky);
        yield return MoveToTarget(vipHolder.position, offSetInSkyLanding);
        yield return RotateTowardsTarget(vipHolder);
        StopCoroutine(moveCarCoroutinue);
        carController.OpenRoof();
        yield return MoveToTarget(vipHolder.position, offSetInSky);

        StartCoroutine(SManManager.Instance.nomalModeWaitingLine.PutSManToVipCar(carToPut.GetComponent<CarController>()));

        transform.LookAt(exitPos);
        yield return MoveToTarget(exitPos.position, Vector3.zero);
        flag.SetActive(true);
        transform.position = spawnPos.position;

        transform.rotation = startPos.rotation;
        yield return MoveToTarget(startPos.position, Vector3.zero);
    }
    IEnumerator MoveCar(Transform carToMove)
    {
        while (true)
        {
            carToMove.rotation = transform.rotation;
            carToMove.transform.position = transform.position - offSetInSkyLanding;
            yield return null;
        }
    }
    IEnumerator MoveToTarget(Vector3 targetPos, Vector3 offSet)
    {
        while (Vector3.Distance(transform.position, targetPos + offSet) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos + offSet, speedMove * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator RotateTowardsTarget(Transform target)
    {
        Quaternion startRotation = transform.rotation;

        Quaternion targetRotation = target.rotation;

        float elapsedTime = 0f;
        while (elapsedTime < speedRotate)
        {
            Quaternion newRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / speedRotate);
            transform.rotation = new Quaternion(transform.rotation.x, newRotation.y, transform.rotation.z, transform.rotation.w);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = targetRotation;
    }
    IEnumerator UpdateBoosterAmountTextPos()
    {
        while (boosterAmountTextGp.activeSelf)
        {
            boosterAmountTextGp.transform.position = Camera.main.WorldToScreenPoint(boosterAmountTextPos.position);
            yield return null;
        }
    }
}
