using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MergeText : MonoBehaviour
{

	public Camera Cam;	
	RectTransform CanvasRect;






	public Text ScoreText;
	private void OnEnable()
	{
		ScoreText.text = "";
	}

	public void SetText(int _amount, Vector3 _position)
	{
		CanvasRect = transform.parent.GetComponent<RectTransform>();

		Vector3 screenPos = Cam.WorldToScreenPoint(_position);
	
		CanvasRect.position = screenPos;

		ScoreText.text = "+" + _amount;

		if(this.isActiveAndEnabled)
		StartCoroutine(	IWaitToHide());
	}

	private IEnumerator IWaitToHide()
	{
		yield return new WaitForSeconds(1.2f);
		gameObject.SetActive(false);
	}
}
