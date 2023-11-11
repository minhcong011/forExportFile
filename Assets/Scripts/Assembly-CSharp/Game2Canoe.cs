// ILSpyBased#2
using UnityEngine;
using UnityEngine.AI;

public class Game2Canoe : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private GameObject CameraCanoe;

    private GameObject CameraLookCanoe;

    private float Timer1 = 4f;

    private float Timer2 = 3f;

    private float Timer3 = 3f;

    private float CamX1;

    private float CamY1;

    private int State1;

    private int State2 = 1;

    private int State3 = 1;

    private void Start()
    {
        if (VariblesGlobal.game2_Final == 0)
        {
            this.navMeshAgent = base.GetComponent<NavMeshAgent>();
            this.navMeshAgent.SetDestination(GameObject.Find("SphereCanoe").transform.position);
            this.CameraCanoe = GameObject.Find("CameraCanoe");
            this.CameraLookCanoe = GameObject.Find("CameraLookCanoe");
            this.CamX1 = this.CameraLookCanoe.transform.position.x;
            this.CamY1 = this.CameraLookCanoe.transform.position.y;
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(base.gameObject);
        }
    }

    private void Update()
    {
        if (this.State1 == 0)
        {
            this.Timer1 -= Time.deltaTime;
            if (this.Timer1 > 0f)
            {
                this.CamX1 -= 40f * Time.deltaTime;
                this.CamY1 += 1f * Time.deltaTime;
                this.CameraLookCanoe.transform.position = new Vector3(this.CamX1, this.CamY1, this.CameraLookCanoe.transform.position.z);
            }
            else
            {
                this.State2 = 0;
                this.State1 = 1;
            }
        }
        if (this.State2 == 0)
        {
            this.Timer2 -= Time.deltaTime;
            if (this.Timer2 < 0f)
            {
                this.State2 = 1;
                this.State3 = 0;
            }
        }
        if (this.State3 == 0)
        {
            this.Timer3 -= Time.deltaTime;
            if (this.Timer3 > 0f)
            {
                this.CamX1 += 48f * Time.deltaTime;
                this.CamY1 -= 2f * Time.deltaTime;
                this.CameraLookCanoe.transform.position = new Vector3(this.CamX1, this.CamY1, this.CameraLookCanoe.transform.position.z);
            }
        }
        this.CameraCanoe.transform.LookAt(this.CameraLookCanoe.transform.position);
    }
}


