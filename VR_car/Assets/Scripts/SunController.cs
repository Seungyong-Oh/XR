using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [Header("Sun")]
    [SerializeField] Light sun;
    [SerializeField] Gradient sunColor;
    [SerializeField] AnimationCurve sunIntensity;

    [Header("Environment")]
    [SerializeField] AnimationCurve lightingIntensityMultiplier;
    [SerializeField] AnimationCurve reflectionsIntensityMultiplier;

    [Header("Building")]
    [SerializeField] Texture tx1, tx2; // 夜景用ビルテクスチャ
    private GameObject[] bldg; // ビルのBaked Mesh 2-344
    private Texture[] originalTex; // ビルのテクスチャを格納

    [Header("Light")]
    [SerializeField] Light headLight1, headLight2;

    void Start()
    {
        // ビルのテクスチャを格納しておく
        bldg = new GameObject[344];
        originalTex = new Texture[344];
        for (int i = 0; i < 343; i++)
        {
            bldg[i] = GameObject.Find("Baked Mesh " + (i + 2).ToString());
            originalTex[i] = bldg[i].GetComponent<Renderer>().material.mainTexture;
        }
    }

    void Update()
    {
        float tim = (Time.time % 60f) / 30f - 0.5f;

        // 太陽の角度
        float rotX;
        float rotY;
        if (tim < 0.5f)
            rotX = tim * 90f;
        else
            rotX = (1f - tim) * 90f;
        rotY = 90f + 180f * tim;
        sun.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);

        // 太陽の明るさテクスチャヘッドライト
        sun.intensity = sunIntensity.Evaluate(tim) * 2;
        if (sun.intensity <= 0 && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
            ChangeTexture(false);
            headLight1.enabled = true;
            headLight2.enabled = true;
        }
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(true);
            ChangeTexture(true);
            headLight1.enabled = false;
            headLight2.enabled = false;
        }

        // 太陽の色
        sun.color = sunColor.Evaluate(tim);

        // 周囲の明るさ
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(tim);

        // 反射光
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(tim);
    }

    // テクスチャ貼り替え
    void ChangeTexture(bool day)
    {
        if (day) // 昼
        {
            for (int i = 0; i < 343; i++)
            {
                bldg[i].GetComponent<Renderer>().material.mainTexture = originalTex[i];
            }
        }
        else // 夜
        {
            for (int i = 0; i < 343; i++)
            {
                if (i % 2 == 0)
                    bldg[i].GetComponent<Renderer>().material.mainTexture = tx1;
                else
                    bldg[i].GetComponent<Renderer>().material.mainTexture = tx2;
            }
        }
    }
}
