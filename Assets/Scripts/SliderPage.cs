using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderPage : MonoBehaviour {
    [Tooltip("Images页面集合")]
    public Transform m_images;
    // Use this for initialization
    void Start () {
        //初始化Image页面 排列位置
        for (int idx = 0; idx < m_images.childCount; idx++)
        {
            //初始化m_images的父物体位置
            if (idx.Equals(0))
            {
                
                m_images.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                //设置第一个image的位置
                m_images.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                //排列每个Images的位置（除了第一个image外）
                float PreWidth = .5f * (m_images.GetChild(idx-1).GetComponent<RectTransform>().rect.width + m_images.GetChild(idx).GetComponent<RectTransform>().rect.width);
                m_images.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = new Vector2(idx * PreWidth, 0);
            }
            //随机设置image的颜色
            Color color = new Color((Random.Range(0, 256) / 255f), (Random.Range(0, 256) / 255f), (Random.Range(0, 256) / 255f));
            m_images.GetChild(idx).GetComponent<Image>().color = color;
        }

    }
	
	// Update is called once per frame
	void Update () {
        OnSliderPage();
    }

    bool isDown = false;
    float mousePosX;
    float runtimePos;
    float deltaX;
    int sliderIndex = 0;
    void OnSliderPage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDown = true;
            mousePosX = Input.mousePosition.x;
            runtimePos = m_images.GetComponent<RectTransform>().anchoredPosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
            float width = m_images.GetChild(0).GetComponent<RectTransform>().rect.width;
            if (Mathf.Abs(deltaX) >= (width / 2) || Mathf.Abs(Input.GetAxis("Mouse X")) >= 3)
            {
                if (deltaX > 0)
                {
                    //向右
                    sliderIndex++;
                    if (sliderIndex > 0)
                        sliderIndex = 0;
                }
                else
                {
                    //向左
                    sliderIndex--;
                    if (sliderIndex < (-m_images.childCount+1))
                        sliderIndex = -m_images.childCount + 1;
                }
                m_images.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
            }
            else
            {
                m_images.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
                //恢复
            }
        }
        if (isDown)
        {
            deltaX = Input.mousePosition.x - mousePosX;
            m_images.GetComponent<RectTransform>().anchoredPosition = new Vector2(deltaX + runtimePos, 0);
        }
    }

}
