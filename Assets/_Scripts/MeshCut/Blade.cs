using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME.Extensions;
using System.Threading;
using System;

namespace BLINDED_AM_ME
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private RuntimeAnimatorController slimeAnimator;
        public Material CapMaterial;

        private CancellationTokenSource _previousTaskCancel;
        private void OnEnable()
        {
            StartCoroutine(StartCut());
        }
        IEnumerator StartCut()
        {
            yield return new WaitForSeconds(0);

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (!hit.collider.gameObject.CompareTag("Meet")) yield break;
                var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(1f)).Token;

                // this will hold up everything
                StartCoroutine(CutCoroutine(hit.collider.gameObject, timeLimit));

                // this won't hold up everything
                //StartCoroutine(CutCoroutine(hit.collider.gameObject, timeLimit));
            }
            else
            {
                Debug.LogError("Missed");
            }
        }

        // this will hold up the UI thread
        private void Cut(GameObject target, CancellationToken cancellationToken = default)
        {
            try
            {
                _previousTaskCancel?.Cancel();
                _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cancellationToken = _previousTaskCancel.Token;
                cancellationToken.ThrowIfCancellationRequested();

                // get the victims mesh
                var leftSide = target;
                var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
                var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

                var materials = new List<Material>();
                leftMeshRenderer.GetSharedMaterials(materials);

                // the insides
                var capSubmeshIndex = 0;
                if (materials.Contains(CapMaterial))
                    capSubmeshIndex = materials.IndexOf(CapMaterial);
                else
                {
                    capSubmeshIndex = materials.Count;
                    materials.Add(CapMaterial);
                }

                // set the blade relative to victim
                var blade = new Plane(
                    leftSide.transform.InverseTransformDirection(transform.right),
                    leftSide.transform.InverseTransformPoint(transform.position));

                var mesh = leftMeshFilter.sharedMesh;
                //var mesh = leftMeshFilter.mesh;

                // Cut
                var pieces = mesh.Cut(blade, capSubmeshIndex, cancellationToken);

                leftSide.name = "LeftSide";
                leftMeshFilter.mesh = pieces.Item1;
                leftMeshRenderer.sharedMaterials = materials.ToArray();
                //leftMeshRenderer.materials = materials.ToArray();

                var rightSide = new GameObject("RightSide");
                var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
                var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

                rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
                rightSide.transform.localScale = leftSide.transform.localScale;

                rightMeshFilter.mesh = pieces.Item2;
                rightMeshRenderer.sharedMaterials = materials.ToArray();
                //rightMeshRenderer.materials = materials.ToArray();

                // Physics 
                Destroy(leftSide.GetComponent<Collider>());

                // Replace
                var leftCollider = leftSide.AddComponent<MeshCollider>();
                leftCollider.convex = true;
                leftCollider.sharedMesh = pieces.Item1;

                var rightCollider = rightSide.AddComponent<MeshCollider>();
                rightCollider.convex = true;
                rightCollider.sharedMesh = pieces.Item2;

                // rigidbody
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
        private void StartSlimeAnimation(GameObject objToAni)
        {
            //Animator animator = null;
            //animator = objToAni.GetComponent<Animator>();
            //if(animator == null) animator = objToAni.AddComponent<Animator>();

            //MeshCutAnimationController controller = objToAni.GetComponent<MeshCutAnimationController>();
            //if (controller == null) controller = objToAni.AddComponent<MeshCutAnimationController>();

            //animator.runtimeAnimatorController = slimeAnimator;

            //controller.animator = animator;
            //controller.StartCoroutine(controller.PlayNomalAni());

            if (objToAni.GetComponent<Jellyfier>() == null) objToAni.AddComponent<Jellyfier>();
        }
        private void AddRigibodyComponent(GameObject sideToAdd)
        {
            int force = 300;
            Rigidbody rb = null;
            try
            {
                rb = sideToAdd.GetComponent<Rigidbody>();
            }
            catch
            {

            }
            if(rb == null) rb = sideToAdd.AddComponent<Rigidbody>();
            rb.AddForce(UnityEngine.Random.Range(-force, force), force, UnityEngine.Random.Range(-force, force));
        }
        // this will not hold up the UI thread
        private IEnumerator CutCoroutine(GameObject target, CancellationToken cancellationToken = default)
        {
            _previousTaskCancel?.Cancel();
            _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cancellationToken = _previousTaskCancel.Token;

            // get the victims mesh
            var leftSide = target;
            var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
            var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

            var materials = new List<Material>();
            leftMeshRenderer.GetSharedMaterials(materials);

            // the insides
            var capSubmeshIndex = 0;
            if (materials.Contains(CapMaterial))
                capSubmeshIndex = materials.IndexOf(CapMaterial);
            else
            {
                capSubmeshIndex = materials.Count;
                materials.Add(CapMaterial);
            }

            // set the blade relative to victim
            var blade = new Plane(
                leftSide.transform.InverseTransformDirection(transform.right),
                leftSide.transform.InverseTransformPoint(transform.position));

            var mesh = leftMeshFilter.sharedMesh;
            //var mesh = leftMeshFilter.mesh;

            // Cut
            yield return mesh.CutCoroutine(blade,
                (pieces) =>
                {
                    leftSide.name = "LeftSide";
                    leftMeshFilter.mesh = pieces.Item1;
                    leftMeshRenderer.sharedMaterials = materials.ToArray();
                    //leftMeshRenderer.materials = materials.ToArray();

                    var rightSide = new GameObject("RightSide");
                    var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
                    var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

                    rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
                    rightSide.transform.localScale = leftSide.transform.localScale;

                    rightMeshFilter.mesh = pieces.Item2;
                    rightMeshRenderer.sharedMaterials = materials.ToArray();
                    //rightMeshRenderer.materials = materials.ToArray();

                    // Physics 
                    Destroy(leftSide.GetComponent<Collider>());

                    // Replace
                    var leftCollider = leftSide.AddComponent<MeshCollider>();
                    leftCollider.convex = true;
                    leftCollider.sharedMesh = pieces.Item1;

                    var rightCollider = rightSide.AddComponent<MeshCollider>();
                    rightCollider.convex = true;
                    rightCollider.sharedMesh = pieces.Item2;

                    // rigidbody
                    if (!leftSide.GetComponent<Rigidbody>()) AddRigibodyComponent(leftSide);

                    if (!rightSide.GetComponent<Rigidbody>())
                        AddRigibodyComponent(rightSide);

                    leftSide.gameObject.tag = "Meet";
                    rightSide.gameObject.tag = "Meet";

                    StartSlimeAnimation(leftSide);
                    StartSlimeAnimation(rightSide);
                    ObjectPooling.Instance.meetTrash.Add(leftSide);
                    ObjectPooling.Instance.meetTrash.Add(rightSide);
                }, capSubmeshIndex, cancellationToken);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            var top = transform.position + transform.up * 0.5f;
            var bottom = transform.position - transform.up * 0.5f;

            Gizmos.DrawRay(top, transform.forward * 5.0f);
            Gizmos.DrawRay(transform.position, transform.forward * 5.0f);
            Gizmos.DrawRay(bottom, transform.forward * 5.0f);
            Gizmos.DrawLine(top, bottom);
        }
    }
}
