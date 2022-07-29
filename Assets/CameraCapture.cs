using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class CameraCapture : MonoBehaviour
{
    private Camera _camera;
    private bool TakeScreenshot;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    
    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCamerRednginerlg;
    }

    
    private void RenderPipelineManager_endCamerRednginerlg(ScriptableRenderContext arg1, Camera arg2)
    {
        if (TakeScreenshot)
        {
            Debug.Log("tdd");
            TakeScreenshot = false;
                 int width = Screen.width;
                int height = Screen.height;
                //_camera.targetTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
                Texture2D screenshotTexturee = new Texture2D(width, height, TextureFormat.ARGB32, false);
                Rect rect = new Rect(0, 0, width, height);
                screenshotTexturee.ReadPixels(rect, 0, 0);
                screenshotTexturee.Apply();

                byte[] byterArray = screenshotTexturee.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byterArray);
        }
       
    }


    public void Capture()
    {
        //  ScreenCapture.CaptureScreenshot("out.png");
        // StartCoroutine(ScreenShot());
        TakeScreenshot = true;
    }
    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;

        Texture2D screenshotTexturee = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0,0,width,height);
        screenshotTexturee.ReadPixels(rect, 0, 0);
        screenshotTexturee.Apply();

        byte[] byterArray = screenshotTexturee.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byterArray);

    }


}
