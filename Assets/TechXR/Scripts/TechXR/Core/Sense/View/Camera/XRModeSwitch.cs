using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TechXR.Core.Sense
{
    /// <summary>
    /// Class to switch modes : VR/AR, & Stereo/Mono in VR
    /// </summary>
    public class XRModeSwitch : MonoBehaviour
    {
        #region PUBLIC_FIELDS
        public float EyeSeparation = 0.06f;
        public float NearClipPlane = 0.01f;
        public float FarClipPlane = 500.0f;
        public float FieldOfView = 60.0f;
        //
        [HideInInspector]
        public bool IsAR;
        [HideInInspector]
        public bool IsStereo;
        #endregion // PUBLIC_FIELDS
        //
        #region PRIVATE_FIELDS
        private GameObject leftEyeCamera;
        private GameObject rightEyeCamera;
        private GameObject monoCamera;
        #endregion // PRIVATE_FIELDS
        //
        #region MONOBEHAVIOUR_METHODS
        // Start is called before the first frame update
        void Start()
        {
            // create mono camera setup
            monoCamera = new GameObject("leftEyeCamera");
            monoCamera.transform.SetParent(transform);
            monoCamera.transform.localPosition = Vector3.zero;
            monoCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            var cameraM = monoCamera.AddComponent<Camera>();
            cameraM.fieldOfView = FieldOfView;
            cameraM.aspect *= 1;
            cameraM.nearClipPlane = NearClipPlane;
            cameraM.farClipPlane = FarClipPlane;
            // create stereo camera setup
            // create left camera
            leftEyeCamera = new GameObject("leftEyeCamera");
            leftEyeCamera.transform.SetParent(transform);
            leftEyeCamera.transform.localPosition = new Vector3(-EyeSeparation / 2, 0, 0);
            leftEyeCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            var cameraLE = leftEyeCamera.AddComponent<Camera>();
            cameraLE.rect = new Rect(0, 0, 0.5f, 1);
            cameraLE.fieldOfView = FieldOfView;
            cameraLE.aspect *= 1;
            cameraLE.nearClipPlane = NearClipPlane;
            cameraLE.farClipPlane = FarClipPlane;
            // create right camera
            rightEyeCamera = new GameObject("rightEyeCamera");
            rightEyeCamera.transform.SetParent(transform);
            rightEyeCamera.transform.localPosition = new Vector3(EyeSeparation / 2, 0, 0);
            rightEyeCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            var cameraRE = rightEyeCamera.AddComponent<Camera>();
            cameraRE.rect = new Rect(0.5f, 0, 0.5f, 1);
            cameraRE.fieldOfView = FieldOfView;
            cameraRE.aspect *= 1;
            cameraRE.nearClipPlane = NearClipPlane;
            cameraRE.farClipPlane = FarClipPlane;
        }
        // Update is called once per frame
        void Update()
        {
            gameObject.GetComponent<Camera>().enabled = IsAR;
            monoCamera.SetActive(!IsAR);
            leftEyeCamera.SetActive(IsStereo);
            rightEyeCamera.SetActive(IsStereo);
        }
        #endregion // MONOBEHAVIOUR_METHODS
        //
        #region PRIVATE_METHODS
        private void ChangeToVR()
        {
            IsStereo = true;
            IsAR = false;
            gameObject.GetComponent<GyroScript>().enabled = true;
            //Screen.orientation = ScreenOrientation.LandscapeRight;
            //Screen.orientation = ScreenOrientation.AutoRotation;
        }
        //
        private void ChangeToAR()
        {
            IsStereo = false;
            IsAR = true;
            gameObject.GetComponent<GyroScript>().enabled = false;
        }
        #endregion // PRIVATE_METHODS
    }
}
