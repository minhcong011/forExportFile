// dnSpy decompiler from Assembly-CSharp.dll class: LoadExamples
using System;
using UnityEngine;

public class LoadExamples : MonoBehaviour
{
	public void LoadExample(string level)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(level);
	}
}
