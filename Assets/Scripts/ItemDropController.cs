using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ItemDropController : MonoBehaviour
{

	public float speed;

	[SerializeField]
	private Transform dropperSpawnPoint;

	[SerializeField]
	private Transform dropperClaw;

	[SerializeField]
	private ParticleSystem mergeParticles;

	private DropItem itemToDrop;

	public GameObject OutofBound;

	private bool IsStart;


	public static ItemDropController Instance;
	
	GameObject Newpartical;

	[HideInInspector] public bool PlayGame;
	

	
	Vector2 presspos, actualpos;
	Vector3 tmp,mvm;

	Data QueuedFruit,NowFruit;

	private void Awake()
	{
		Instance = this;
	}
	private void Start() 
	{
		
		GenerateQueuedFruit();
		SpawnNewDroppable();

		Newpartical = Instantiate(mergeParticles.gameObject);
		Newpartical.SetActive(false);
	}

	private void Update()
	{
		if(PlayGame)
		SwerveMovement();
	}
		private void SwerveMovement()
	    {


		if (Input.GetMouseButtonDown(0))
        {
            presspos = Input.mousePosition;
        }

		if (Input.GetMouseButton(0))
        {
			actualpos = Input.mousePosition;
			tmp = dropperClaw.position;

			float xdiff = (actualpos.x - presspos.x) * Time.smoothDeltaTime * speed;

            tmp.x += xdiff;
            tmp.x = Mathf.Clamp(tmp.x, -1.6f, 1.6f);
            dropperClaw.position = tmp;

            presspos = actualpos;

		}

		if (Input.GetMouseButtonUp(0))
        {
            mvm = Vector3.zero;

			
			DropBall();
        }

	}

	bool FirstTime;
	Transform HighFruit ;



	public void CheckForItemsOutOfBounds(Transform DropFruit)
	{
		
		if(!FirstTime || HighFruit == null)
		{
			FirstTime = true;
			HighFruit = DropFruit;
		}

		if(DropFruit.position.y > HighFruit.position.y )
		{
			HighFruit = DropFruit;
		}

		
		if(HighFruit.position.y > 1.2f)
		{
			OutofBound.SetActive(true);

			if(DropFruit.position.y > 2.4f)
			{
				UiManger.Intence.ShowLoss();
			}

		}
		else
		{
			OutofBound.SetActive(false);
		}
	}


	private void DropBall()
	{
		itemToDrop.transform.SetParent(null);
		itemToDrop.SetPhysicsActive();

		NowFruit = QueuedFruit;
		GenerateQueuedFruit();
		SpawnNewDroppable();


	}	

	



	private void SpawnNewDroppable()
	{
		GameObject NewFruitObject = Instantiate(NowFruit.itemObject.gameObject,dropperSpawnPoint.position,Quaternion.identity,dropperSpawnPoint); 
		itemToDrop = NewFruitObject.GetComponent<DropItem>();

		UiManger.Intence.UpdateNextItems(NowFruit.itemSprite,QueuedFruit.itemSprite);
	}

	

	public void MergeItem(Vector3 _postion,DropItemId itemId)
	{
		if(!OneTime)
		{
			OneTime = true;
		}
		else if(OneTime)
		{
			OneTime = false;

			UiManger.Intence.ShowScore(DropItemData.Instance.GetDataForId(itemId).scoreForMerge);
			UiManger.Intence.ShowMergeText(DropItemData.Instance.GetDataForId(itemId).scoreForMerge,_postion);

			if(itemId < DropItemId.Item9)
			itemId++;
			

			GameObject NewFruitObject = Instantiate(DropItemData.Instance.GetDataForId(itemId).itemObject.gameObject,_postion,Quaternion.identity); 
			NewFruitObject.GetComponent<DropItem>().SetPhysicsActive();

			
			Newpartical.transform.localPosition = NewFruitObject.transform.localPosition;
			Newpartical.SetActive(true);
			Newpartical.GetComponent<ParticleSystem>().Play();

			

			


		}
		
	}
	bool OneTime;



	private void GenerateQueuedFruit()
	{
		int QueuedFruitNumber = generateRandomNumber(0,6);

		QueuedFruit = DropItemData.Instance.dataList[QueuedFruitNumber];

		if(!IsStart)
		{
			int NowFruitNumber = generateRandomNumber(0,3);

			NowFruit = DropItemData.Instance.dataList[NowFruitNumber];

			IsStart = true;
		}

	}

	private bool IsOverPauseButton()
	{
		return false;
	}


	
private static int lastRandomNumber;
 
 public static int generateRandomNumber(int min, int max) {
 
     int result = Random.Range(min, max);
 
     if(result == lastRandomNumber) {
 
             return generateRandomNumber(min, max);
 
     }
      
     lastRandomNumber = result;
     return result;
 
 }
}
