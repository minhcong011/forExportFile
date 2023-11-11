using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
	// Token: 0x02000063 RID: 99
	public class Restarter : MonoBehaviour
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000C760 File Offset: 0x0000A960
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "Player")
			{
				SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			}
		}
	}
}
