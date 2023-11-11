using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Token: 0x02000021 RID: 33
public class chapter1dog2 : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x00004FEF File Offset: 0x000031EF
	private void Start()
	{
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.navMeshAgent.speed = 1.5f;
		this.SetAnimate("isWalking2");
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00005018 File Offset: 0x00003218
	private void Update()
	{
		if (VariblesGlobal.BadEndDog1 == 1 && this.Action == 0)
		{
			this.Action = 1;
		}
		if (this.Action == 1)
		{
			this.Action = 2;
			base.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("GrandPa1").transform.position);
		}
		if (this.Action == 2)
		{
			this.Timer1 -= Time.deltaTime;
			if (this.Timer1 <= 0f)
			{
				this.Action = 3;
				this.navMeshAgent.speed = 11f;
				this.SetAnimate("isAttacking");
				Object.Instantiate(Resources.Load("sound/soundDogAttack"));
			}
		}
		if (this.Action == 3)
		{
			this.Timer2 -= Time.deltaTime;
			if (this.Timer2 <= 0f)
			{
				this.Action = 4;
				Object.Instantiate(Resources.Load("soundHuman/SoundDeathMe"));
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelDeath")) as GameObject;
				gameObject.name = "mPanelDead";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00005140 File Offset: 0x00003340
	private void SetAnimate(string AnimT)
	{
		if (this.AnimT2 != AnimT)
		{
			this.AnimT2 = AnimT;
			base.GetComponent<Animator>().SetBool("isIdle", false);
			base.GetComponent<Animator>().SetBool("isWalking", false);
			base.GetComponent<Animator>().SetBool("isWalking2", false);
			base.GetComponent<Animator>().SetBool("isAttacking", false);
			base.GetComponent<Animator>().SetBool("isEat", false);
			base.GetComponent<Animator>().SetBool(AnimT, true);
		}
	}

	// Token: 0x040000DC RID: 220
	private NavMeshAgent navMeshAgent;

	// Token: 0x040000DD RID: 221
	private string AnimT2;

	// Token: 0x040000DE RID: 222
	private Scene sceneName;

	// Token: 0x040000DF RID: 223
	private int Action;

	// Token: 0x040000E0 RID: 224
	private float Timer1 = 5f;

	// Token: 0x040000E1 RID: 225
	private float Timer2 = 0.7f;
}
