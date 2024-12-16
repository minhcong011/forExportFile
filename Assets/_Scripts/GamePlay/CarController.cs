using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
public class CarController : MonoBehaviour
{
    [SerializeField] private bool test;
    [SerializeField] private int carId;
    public int CarId { get { return carId; } }
    [SerializeField] private int colorID;
    public int ColorID { get { return colorID; } set { colorID = value; } }

    [SerializeField] private Transform[] seatsList;
    public int NumOfSeats { get { return seatsList.Length; } }

    private int currentSeatsIsUse;
    public int CurrentSeatsIsUse { get { return currentSeatsIsUse; } }

    [SerializeField] private GameObject seatsGp;
    [SerializeField] private GameObject arrow;
    [SerializeField] private ParticleSystem smokeEf;

    [SerializeField] private MeshRenderer carSprite;

    [SerializeField] private int maxNumOfSeats;

    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private MeshRenderer[] topModels;
    [SerializeField] private Animator animator;

    private const float speedMoveFoward = 30;
    private const float speedMove = 30;

    private bool isMoving;
    private Coroutine moveCoroutinue;
    private Vector3 startPos;

    private CarHolder currentHolder;
    public CarHolder CurrentHolder { get { return currentHolder; } set { currentHolder = value; } }

    private bool finishMove;
    public bool FinishMove { get { return finishMove; } }

    private int numCarForward;
    public int NumCarForward
    {
        get
        {
            return numCarForward;
        }
    }
    private List<CarController> carForwardCarControllers = new();

    private int seatsFinishSpawn;

    public int SeatsFinishSpawn { get { return seatsFinishSpawn; } set { seatsFinishSpawn = value; } }


    private bool isCarInConveyorBetl;

    public bool IsCarInConveyorBetl { get => isCarInConveyorBetl; set => isCarInConveyorBetl = value; }
    public ConveyorBelt ConveyorBetlParent { get => conveyorBetlParent; set => conveyorBetlParent = value; }

    private ConveyorBelt conveyorBetlParent;

    private List<SManController> smanPutToCar = new();
    public void UpScale()
    {
        float newScale = 1.3f;
        transform.localScale = new (newScale, newScale, newScale);
    }
    public int GetNumCarBlockToSort()
    {
        return GetNumCarBlockToSort(gameObject);
    }
    public int GetNumCarBlockToSort(GameObject carToGet, List<GameObject> visited = null)
    {
        if (visited == null) visited = new();
        if (visited.Contains(carToGet)) return 0;

        visited.Add(carToGet);

        int numCarBlock = numCarForward;
        List<CarController> tmp = new List<CarController>(carForwardCarControllers);

        foreach (CarController car in tmp)
        {
            if (car.gameObject != carToGet)
            {
                numCarBlock += car.GetNumCarBlockToSort(car.gameObject, visited);
            }
        }
        return numCarBlock;
    }

    public void GetNumCarForward()
    {
        Vector3 boxSize = new Vector3(1.07f, 0.2f, 0.8f);
        LayerMask carLayer = LayerMask.GetMask("Car");
        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.forward);

        RaycastHit[] carFowardRayHits = Physics.BoxCastAll(ray.origin, boxSize / 2, ray.direction, Quaternion.identity, 100f, carLayer);

        foreach (RaycastHit car in carFowardRayHits)
        {
            carForwardCarControllers.Add(CarManager.Instance.carControllderDictionary[car.transform.gameObject]);
        }
        numCarForward = carFowardRayHits.Length;
    }
    public void StartMove()
    {
        if (CarManager.Instance.CheckCarHolderNotAvailble()) return;

        CarManager.Instance.carIsMove.Add(this);

        currentHolder = CarManager.Instance.GetCarHolderAvailable();
        if (currentHolder == null) return;
        currentHolder.IsChoose = true;
        startPos = transform.position;
        if (finishMove) return;
        if (!isMoving)
        {
            smokeEf.Play();
            moveCoroutinue = StartCoroutine(MoveCoroutinue());
            isMoving = true;
        }
    }
    public void SetColor(CarColorSheet colorSheet)
    {
        carSprite.material.color = colorSheet.colorBase1.color;
        foreach(MeshRenderer mr in topModels)
        {
            mr.material.color = colorSheet.colorBase1.color;
        }
    }
    public void OpenRoof()
    {
        seatsGp.SetActive(true);
        arrow.SetActive(false);
        animator.Play("CarOpenRoof");
    }
    public void CloseRoof()
    {
        animator.Play("CarCloseRoof");
    }
    IEnumerator MoveCoroutinue()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * speedMoveFoward * Time.deltaTime);
            if (transform.position.x < -12)
            {
                yield return MoveToHolder(1);
                yield break;
            }
            if (transform.position.z > 8)
            {
                yield return MoveToHolder(2);
                yield break;
            }
            if (transform.position.x > 12)
            {
                yield return MoveToHolder(3);
                yield break;
            }
            if (transform.position.z < -17)
            {
                if (transform.position.x >= 0)
                {
                    yield return MoveToHolder(4);
                    yield break;
                }
                if (transform.position.x < 0)
                {
                    yield return MoveToHolder(5);
                    yield break;
                }
            }
            yield return null;
        }
    }
    private IEnumerator MoveToHolder(int holderID)
    {
        finishMove = true;
        yield return CarManager.Instance.MoveCarToHolder(this, holderID);
        if (isCarInConveyorBetl) ConveyorBelt.Instance.StopMove = false;
        smokeEf.Stop();
    }
    public IEnumerator MoveToPos(Vector3 targetPos)
    {
        if (!smokeEf.isPlaying) smokeEf.Play();
        transform.LookAt(targetPos);
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speedMove * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator ReturnStartPos()
    {
        currentHolder.IsChoose = false;
        currentHolder = null;
        smokeEf.Stop();
        while (Vector3.Distance(transform.position, startPos) > 0.1f && !isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speedMove * Time.deltaTime);
            yield return null;
        }
        if (isCarInConveyorBetl)
        {
            conveyorBetlParent.StopMove = false;
        }
        if (!isMoving) transform.position = startPos;
    }
    public IEnumerator PutSManToSeats(SManController sMan)
    {
        smanPutToCar.Add(sMan);
        bool seatsIsFull = false;
        Transform seats = GetSeatsAvaible();
        currentSeatsIsUse++;
        SManManager.Instance.sManMoveToCar.Add(sMan);
        StartCoroutine(PutSManToSeatsCoroutinue());
        IEnumerator PutSManToSeatsCoroutinue()
        {
            if (GetSeatsIsFull()) seatsIsFull = true;
            sMan.transform.SetParent(seats, true);
            sMan.PlayJoinCarAnimation(seats);

            yield return sMan.MoveToPos(seats.position);

            AudioManager.Instance.PlaySound("PutSmanToCar");
            GameCache.GC.coin++;
            UICoinText.Instance.UpdateCoinText();
            SManManager.Instance.sManMoveToCar.Remove(sMan);
            SManManager.Instance.NumSManRemain--;
            smanPutToCar.Remove(sMan);
            GamePlayUIManager.Instance.UpdateAmountSManRemainText();
            if (seatsIsFull)
            {
                currentHolder.ResetHolder();
                while (smanPutToCar.Count != 0) yield return null;
                yield return new WaitForSeconds(0.1f);
                AudioManager.Instance.PlaySound("CartLeave");
                if (!GameCache.GC.vibrateOff) HapticFeedback.LightFeedback();
                StartCoroutine(CarManager.Instance.MoveCarToExitPos(this));
            }
            if (sMan.carSpawn != null)
            {
                if (sMan.carSpawn != this && SManManager.Instance.seatsCanSpawnInCar[this] > 0)
                {
                    SManManager.Instance.ChangeSeatsCanSpawnInCar(sMan.carSpawn, 1);
                    SManManager.Instance.ChangeSeatsCanSpawnInCar(this, -1);
                }
            }
            else
            {
                SManManager.Instance.ChangeSeatsCanSpawnInCar(this, -1);
            }
            //if (GameModeController.gameMode == GameMode.Nomal) UICarFinishPercent.Instance.UpdateSlider();
        }
        yield return null;
    }
    private Transform GetSeatsAvaible()
    {
        return seatsList[currentSeatsIsUse];
    }
    public bool GetSeatsIsFull()
    {
        return CurrentSeatsIsUse == NumOfSeats;
    }
    public void DisableSmokeEf()
    {
        smokeEf.Stop();
    }
    public void RandomColor()
    {
        CarColorSheet colorRand = ColorManager.Instance.GetRandomCarColorSheet();
        SetColor(colorRand);
    }
    public int GetSeatsCanUse()
    {
        return NumOfSeats - currentSeatsIsUse;
    }

    public int GetSeatsCanSpawn()
    {
        return NumOfSeats - seatsFinishSpawn;
    }
    private void HandleBreakOtherCar()
    {
        if (!GameCache.GC.vibrateOff) HapticFeedback.LightFeedback();
        CarManager.Instance.carIsMove.Remove(this);
        isMoving = false;
        StopCoroutine(moveCoroutinue);
        StartCoroutine(ReturnStartPos());
    }
    private void OnTriggerEnter(Collider collision)
    {
        string gameobjectTag = collision.gameObject.tag;
        if (gameobjectTag == "Car" && !collision.gameObject.GetComponent<CarController>().FinishMove && !finishMove)
        {
            if (isMoving)
            {
                HandleBreakOtherCar();
            }
        }
    }
}
