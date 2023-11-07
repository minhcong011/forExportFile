using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class DropItem : MonoBehaviour
{
	[SerializeField]
	private DropItemId itemId;

	[SerializeField]
	private Rigidbody2D rBody;

	[NonSerialized]
	public float timeUntilOutOfBoundsCheck;

	bool IsActive;


	public DropItemId GetItemId => default(DropItemId);

	public bool CanCheckOutOfBounds => false;

	Vector3 MyScale;

	private void Awake()
	{
		MyScale = transform.localScale;
		transform.localScale = Vector3.zero;
		rBody = GetComponent<Rigidbody2D>();
		SetPhysicsPaused();
		transform.DOScale(MyScale,0.5f);
	}

	private void Update() 
	{

	}

	private void OnCollisionEnter2D(Collision2D _collision)
	{
		if(_collision.gameObject.GetComponent<DropItem>() != null && transform.parent == null )
		{
				if(_collision.gameObject.GetComponent<DropItem>().itemId == itemId && IsActive == true)
				{
					ItemDropController.Instance.MergeItem(transform.position,itemId);
					
					Destroy(gameObject);
				} 
		}
		
	}

	public void SetPhysicsActive()
	{
		rBody.isKinematic = false;
		IsActive = true;
		StartCoroutine(Wait());
	}

	public void SetPhysicsPaused()
	{
		rBody.isKinematic = true;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);
		ItemDropController.Instance.CheckForItemsOutOfBounds(transform);
	}

	
}
