// ILSpyBased#2
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Water
{
    [ExecuteInEditMode]
    public class Water : MonoBehaviour
    {
        public enum WaterMode
        {
            Simple,
            Reflective,
            Refractive
        }

        public WaterMode waterMode = WaterMode.Refractive;

        public bool disablePixelLights = true;

        public int textureSize = 256;

        public float clipPlaneOffset = 0.07f;

        public LayerMask reflectLayers = -1;

        public LayerMask refractLayers = -1;

        private Dictionary<Camera, Camera> m_ReflectionCameras = new Dictionary<Camera, Camera>();

        private Dictionary<Camera, Camera> m_RefractionCameras = new Dictionary<Camera, Camera>();

        private RenderTexture m_ReflectionTexture;

        private RenderTexture m_RefractionTexture;

        private WaterMode m_HardwareWaterSupport = WaterMode.Refractive;

        private int m_OldReflectionTextureSize;

        private int m_OldRefractionTextureSize;

        private static bool s_InsideWater;

        public void OnWillRenderObject()
        {
            if (base.enabled && (bool)base.GetComponent<Renderer>() && (bool)base.GetComponent<Renderer>().sharedMaterial && base.GetComponent<Renderer>().enabled)
            {
                Camera current = Camera.current;
                if ((bool)current && !Water.s_InsideWater)
                {
                    Water.s_InsideWater = true;
                    this.m_HardwareWaterSupport = this.FindHardwareWaterSupport();
                    WaterMode waterMode = this.GetWaterMode();
                    Camera camera = default(Camera);
                    Camera camera2 = default(Camera);
                    this.CreateWaterObjects(current, out camera, out camera2);
                    Vector3 position = base.transform.position;
                    Vector3 up = base.transform.up;
                    int pixelLightCount = QualitySettings.pixelLightCount;
                    if (this.disablePixelLights)
                    {
                        QualitySettings.pixelLightCount = 0;
                    }
                    this.UpdateCameraModes(current, camera);
                    this.UpdateCameraModes(current, camera2);
                    if (waterMode >= WaterMode.Reflective)
                    {
                        float w = 0f - Vector3.Dot(up, position) - this.clipPlaneOffset;
                        Vector4 plane = new Vector4(up.x, up.y, up.z, w);
                        Matrix4x4 zero = Matrix4x4.zero;
                        Water.CalculateReflectionMatrix(ref zero, plane);
                        Vector3 position2 = current.transform.position;
                        Vector3 position3 = zero.MultiplyPoint(position2);
                        camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
                        Vector4 clipPlane = this.CameraSpacePlane(camera, position, up, 1f);
                        camera.projectionMatrix = current.CalculateObliqueMatrix(clipPlane);
                        camera.cullingMatrix = current.projectionMatrix * current.worldToCameraMatrix;
                        camera.cullingMask = (-17 & this.reflectLayers.value);
                        camera.targetTexture = this.m_ReflectionTexture;
                        bool invertCulling = GL.invertCulling;
                        GL.invertCulling = !invertCulling;
                        camera.transform.position = position3;
                        Vector3 eulerAngles = current.transform.eulerAngles;
                        camera.transform.eulerAngles = new Vector3(0f - eulerAngles.x, eulerAngles.y, eulerAngles.z);
                        camera.Render();
                        camera.transform.position = position2;
                        GL.invertCulling = invertCulling;
                        base.GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
                    }
                    if (waterMode >= WaterMode.Refractive)
                    {
                        camera2.worldToCameraMatrix = current.worldToCameraMatrix;
                        Vector4 clipPlane2 = this.CameraSpacePlane(camera2, position, up, -1f);
                        camera2.projectionMatrix = current.CalculateObliqueMatrix(clipPlane2);
                        camera2.cullingMatrix = current.projectionMatrix * current.worldToCameraMatrix;
                        camera2.cullingMask = (-17 & this.refractLayers.value);
                        camera2.targetTexture = this.m_RefractionTexture;
                        camera2.transform.position = current.transform.position;
                        camera2.transform.rotation = current.transform.rotation;
                        camera2.Render();
                        base.GetComponent<Renderer>().sharedMaterial.SetTexture("_RefractionTex", this.m_RefractionTexture);
                    }
                    if (this.disablePixelLights)
                    {
                        QualitySettings.pixelLightCount = pixelLightCount;
                    }
                    switch (waterMode)
                    {
                        case WaterMode.Simple:
                            Shader.EnableKeyword("WATER_SIMPLE");
                            Shader.DisableKeyword("WATER_REFLECTIVE");
                            Shader.DisableKeyword("WATER_REFRACTIVE");
                            break;
                        case WaterMode.Reflective:
                            Shader.DisableKeyword("WATER_SIMPLE");
                            Shader.EnableKeyword("WATER_REFLECTIVE");
                            Shader.DisableKeyword("WATER_REFRACTIVE");
                            break;
                        case WaterMode.Refractive:
                            Shader.DisableKeyword("WATER_SIMPLE");
                            Shader.DisableKeyword("WATER_REFLECTIVE");
                            Shader.EnableKeyword("WATER_REFRACTIVE");
                            break;
                    }
                    Water.s_InsideWater = false;
                }
            }
        }

        private void OnDisable()
        {
            if ((bool)this.m_ReflectionTexture)
            {
                UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
                this.m_ReflectionTexture = null;
            }
            if ((bool)this.m_RefractionTexture)
            {
                UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
                this.m_RefractionTexture = null;
            }
            Dictionary<Camera, Camera>.Enumerator enumerator = this.m_ReflectionCameras.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    UnityEngine.Object.DestroyImmediate(enumerator.Current.Value.gameObject);
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            this.m_ReflectionCameras.Clear();
            enumerator = this.m_RefractionCameras.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    UnityEngine.Object.DestroyImmediate(enumerator.Current.Value.gameObject);
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            this.m_RefractionCameras.Clear();
        }

        private void Update()
        {
            if ((bool)base.GetComponent<Renderer>())
            {
                Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
                if ((bool)sharedMaterial)
                {
                    Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
                    float @float = sharedMaterial.GetFloat("_WaveScale");
                    Vector4 vector2 = new Vector4(@float, @float, @float * 0.4f, @float * 0.45f);
                    double num = (double)Time.timeSinceLevelLoad / 20.0;
                    Vector4 value = new Vector4((float)Math.IEEERemainder((double)(vector.x * vector2.x) * num, 1.0), (float)Math.IEEERemainder((double)(vector.y * vector2.y) * num, 1.0), (float)Math.IEEERemainder((double)(vector.z * vector2.z) * num, 1.0), (float)Math.IEEERemainder((double)(vector.w * vector2.w) * num, 1.0));
                    sharedMaterial.SetVector("_WaveOffset", value);
                    sharedMaterial.SetVector("_WaveScale4", vector2);
                }
            }
        }

        private void UpdateCameraModes(Camera src, Camera dest)
        {
            if (!((UnityEngine.Object)dest == (UnityEngine.Object)null))
            {
                dest.clearFlags = src.clearFlags;
                dest.backgroundColor = src.backgroundColor;
                if (src.clearFlags == CameraClearFlags.Skybox)
                {
                    Skybox component = ((Component)src).GetComponent<Skybox>();
                    Skybox component2 = ((Component)dest).GetComponent<Skybox>();
                    if (!(bool)component || !(bool)component.material)
                    {
                        component2.enabled = false;
                    }
                    else
                    {
                        component2.enabled = true;
                        component2.material = component.material;
                    }
                }
                dest.farClipPlane = src.farClipPlane;
                dest.nearClipPlane = src.nearClipPlane;
                dest.orthographic = src.orthographic;
                dest.fieldOfView = src.fieldOfView;
                dest.aspect = src.aspect;
                dest.orthographicSize = src.orthographicSize;
            }
        }

        private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
        {
            WaterMode waterMode = this.GetWaterMode();
            reflectionCamera = null;
            refractionCamera = null;
            if (waterMode >= WaterMode.Reflective)
            {
                if (!(bool)this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.textureSize)
                {
                    if ((bool)this.m_ReflectionTexture)
                    {
                        UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
                    }
                    this.m_ReflectionTexture = new RenderTexture(this.textureSize, this.textureSize, 16);
                    this.m_ReflectionTexture.name = "__WaterReflection" + base.GetInstanceID();
                    this.m_ReflectionTexture.isPowerOfTwo = true;
                    this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
                    this.m_OldReflectionTextureSize = this.textureSize;
                }
                this.m_ReflectionCameras.TryGetValue(currentCamera, out reflectionCamera);
                if (!(bool)reflectionCamera)
                {
                    GameObject gameObject = new GameObject("Water Refl Camera id" + base.GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
                    reflectionCamera = gameObject.GetComponent<Camera>();
                    reflectionCamera.enabled = false;
                    reflectionCamera.transform.position = base.transform.position;
                    reflectionCamera.transform.rotation = base.transform.rotation;
                    reflectionCamera.gameObject.AddComponent<FlareLayer>();
                    gameObject.hideFlags = HideFlags.HideAndDontSave;
                    this.m_ReflectionCameras[currentCamera] = reflectionCamera;
                }
            }
            if (waterMode >= WaterMode.Refractive)
            {
                if (!(bool)this.m_RefractionTexture || this.m_OldRefractionTextureSize != this.textureSize)
                {
                    if ((bool)this.m_RefractionTexture)
                    {
                        UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
                    }
                    this.m_RefractionTexture = new RenderTexture(this.textureSize, this.textureSize, 16);
                    this.m_RefractionTexture.name = "__WaterRefraction" + base.GetInstanceID();
                    this.m_RefractionTexture.isPowerOfTwo = true;
                    this.m_RefractionTexture.hideFlags = HideFlags.DontSave;
                    this.m_OldRefractionTextureSize = this.textureSize;
                }
                this.m_RefractionCameras.TryGetValue(currentCamera, out refractionCamera);
                if (!(bool)refractionCamera)
                {
                    GameObject gameObject2 = new GameObject("Water Refr Camera id" + base.GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
                    refractionCamera = gameObject2.GetComponent<Camera>();
                    refractionCamera.enabled = false;
                    refractionCamera.transform.position = base.transform.position;
                    refractionCamera.transform.rotation = base.transform.rotation;
                    refractionCamera.gameObject.AddComponent<FlareLayer>();
                    gameObject2.hideFlags = HideFlags.HideAndDontSave;
                    this.m_RefractionCameras[currentCamera] = refractionCamera;
                }
            }
        }

        private WaterMode GetWaterMode()
        {
            if (this.m_HardwareWaterSupport < this.waterMode)
            {
                return this.m_HardwareWaterSupport;
            }
            return this.waterMode;
        }

        private WaterMode FindHardwareWaterSupport()
        {
            if (!(bool)base.GetComponent<Renderer>())
            {
                return WaterMode.Simple;
            }
            Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
            if (!(bool)sharedMaterial)
            {
                return WaterMode.Simple;
            }
            string tag = sharedMaterial.GetTag("WATERMODE", false);
            if (tag == "Refractive")
            {
                return WaterMode.Refractive;
            }
            if (tag == "Reflective")
            {
                return WaterMode.Reflective;
            }
            return WaterMode.Simple;
        }

        private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
        {
            Vector3 point = pos + normal * this.clipPlaneOffset;
            Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
            Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
            Vector3 vector = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
            return new Vector4(vector.x, vector.y, vector.z, 0f - Vector3.Dot(lhs, vector));
        }

        private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
        {
            reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
            reflectionMat.m01 = -2f * plane[0] * plane[1];
            reflectionMat.m02 = -2f * plane[0] * plane[2];
            reflectionMat.m03 = -2f * plane[3] * plane[0];
            reflectionMat.m10 = -2f * plane[1] * plane[0];
            reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
            reflectionMat.m12 = -2f * plane[1] * plane[2];
            reflectionMat.m13 = -2f * plane[3] * plane[1];
            reflectionMat.m20 = -2f * plane[2] * plane[0];
            reflectionMat.m21 = -2f * plane[2] * plane[1];
            reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
            reflectionMat.m23 = -2f * plane[3] * plane[2];
            reflectionMat.m30 = 0f;
            reflectionMat.m31 = 0f;
            reflectionMat.m32 = 0f;
            reflectionMat.m33 = 1f;
        }
    }
}


