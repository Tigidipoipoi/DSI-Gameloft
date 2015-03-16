using UnityEngine;
using System.Collections;

public class ShakeManager : MonoBehaviour {
    #region Singleton
    static private ShakeManager s_Instance;
    static public ShakeManager instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        mainCamera = Camera.main;
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    #region members
    private float shakeAmt;
    public Camera mainCamera;
    #endregion

    public void LetsShake(float relative=500)
    {
        shakeAmt = relative * .0025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        //mainCamera.transform.position = originalCameraPosition;
    }

}
