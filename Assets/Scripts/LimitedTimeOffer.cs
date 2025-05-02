// dnSpy decompiler from Assembly-CSharp.dll class: LimitedTimeOffer
using System;
using UnityEngine;

public class LimitedTimeOffer : MonoBehaviour
{
	private void Start()
	{
	}

	public void OkClicked()
	{
		OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
		if (openIABPurchaseHandler != null)
		{
			openIABPurchaseHandler.PurchasePressed(4);
		}
	}
}
