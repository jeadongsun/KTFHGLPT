using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using Vuplex.WebView;
using XCharts.Runtime;
using Random = UnityEngine.Random;

public class MainMenuContrl : MonoBehaviour
{
    public GameObject RenYuanZuZhiMenu;
    public GameObject KongXianRenYuanMenu;
    public GameObject ZuoYeGongDanMenu;
    public GameObject BottomMenu;
    public GameObject BottomMenu2;
    public GameObject FengKongYuJingMenu;
    public GameObject JiHuaAnPaiMenu;
    public GameObject ShiShiXiaoLvMenu;
    public GameObject Bg2;
    public Text TitleText;

    public HttpRequestContrl httpRequestC;

    //计划安排
    public Text JiHuaAnPaiTime_Text;
    public Text KCYHS_SL_Text;
    public Text AZYHS_SL_Text;
    public Text TSYHS_SL_Text;
    public Text ZHNYDY_SL_Text;
    public Text FHJCY_SL_Text;
    public Text KTKZMK_SL_Text;
    public Image Kc_Img;
    public Image AZ_Img;
    public Image TS_Img;
    public Text KC_JD_Text;
    public Text AZ_JD_Text;
    public Text TS_JD_Text;

    //风控预警
    public Text JianYiJG_SL_Text;
    public Text JianYiZH_SL_Text;
    public GameObject FKYJ_Item;
    public GameObject FengKongYuJing_Content;

    //空闲人员
    public Text KC_SL_Text;
    public Text AZ_SL_Text;
    public Text TS_SL_Text;
    public GameObject KXRY_Item_Prefab;
    public GameObject Content;
    public GameObject KXRY_Scrollbar;

    //统计累计数据
    public Text LJKCGDCS_SL_Text;
    public Text LJAZGDCS_SL_Text;
    public Text LJTSGDCS_SL_Text;
    public Text LJWCKCCS_SL_Text;
    public Text LJWCAZCS_SL_Text;
    public Text LJWCTSCS_SL_Text;

    //作业工单
    public Text JD_SL_Text;
    public Text ZT_SL_Text;
    public Text WC_SL_Text;
    public GameObject ZYGD_Item;
    public GameObject ZuoYeGongDanContent;

    //人员组织规模 
    public Text TDGMRS_Text;
    public Text KCRY_SL_Text;
    public Text TSRY_SL_Text;
    public Text SSRY_SL_Text;
    public Text GGRY_SL_Text;
    public Text GSTD_SL_Text;
    public Text WWTD_SL_Text;
    public Text WWDW_SL_Text;

    //实施效率
    public Text HWKCSC_SL_Text;
    public Text HWANSC_SL_Text;
    public Text HWTSSC_SL_Text;
    public Text HWKCXH_SL_Text;
    public Text HWAZXH_SL_Text;
    public Text HWTSXH_SL_Text;


    public GameObject BarChart;
    public GameObject BarChart2;

    public ScrollRect KXRY_Scroll;
    public ScrollRect ZYGD_Scroll;
    public ScrollRect FXGJ_Scroll;
    private float KXRY_Scroll_Time = 0.0025f;

    private int KXRY_Count;
    private int ZYGD_Count;
    private int FXGJ_Count;

    public GameObject CanvasWebViewPrefab;
    
    //调试工单
    public Text YiJieRuDY_SL_Text;
    public Text YiJieRuJJ_SL_Text;
    public Text ZuoRiDY_SL_Text;
    public Text ZuoRiJJ_SL_Text;
    public Text YiJieRuKT_SL_Text;
    public Text ZuoRiKT_SL_Text;
    private void Awake()
    {
        // http://192.168.1.219:8001/white-list/large-screen-map
        RenYuanZuZhiMenu.transform.localPosition = new Vector3(-1000, 0, 0);
        KongXianRenYuanMenu.transform.localPosition = new Vector3(-1000, 0, 0);
        ZuoYeGongDanMenu.transform.localPosition = new Vector3(-1000, 0, 0);

        BottomMenu.transform.localPosition = new Vector3(0, -1000, 0);
        BottomMenu2.transform.localPosition = new Vector3(0, -1000, 0);

        FengKongYuJingMenu.transform.localPosition = new Vector3(1000, 0, 0);
        JiHuaAnPaiMenu.transform.localPosition = new Vector3(1000, 0, 0);
        ShiShiXiaoLvMenu.transform.localPosition = new Vector3(1000, 0, 0);

        Bg2.transform.localPosition = new Vector3(0, 1000, 0);
    }

    public void Start()
    {
        Debug.Log("WebViewUrl:" + CanvasWebViewPrefab.GetComponent<CanvasWebViewPrefab>().InitialUrl);
        RenYuanZuZhiMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);
        KongXianRenYuanMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);
        ZuoYeGongDanMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);

        BottomMenu.transform.DOLocalMoveY(0, 1.5f, false).SetEase(Ease.InOutSine);
        BottomMenu2.transform.DOLocalMoveY(0, 1.5f, false).SetEase(Ease.InOutSine);

        FengKongYuJingMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);
        JiHuaAnPaiMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);
        ShiShiXiaoLvMenu.transform.DOLocalMoveX(0, 1.5f, false).SetEase(Ease.InOutSine);

        Bg2.transform.DOLocalMoveY(422, 1.5f, false).SetEase(Ease.InOutSine);

        TitleText.DOText("空调负荷调试管理平台", 0.8f).SetDelay(1f);

        StartCoroutine("InitDataContrl");
    }

    private void Update()
    {
        
    }

    IEnumerator ScrollRectContrl()
    {
        KXRY_Scroll.verticalNormalizedPosition = KXRY_Scroll.verticalNormalizedPosition - KXRY_Scroll_Time;
        yield return new WaitForSeconds(0.01f);
        if (KXRY_Scroll.verticalNormalizedPosition <= 0)
        {
            StopCoroutine("ScrollRectContrl");
        }
        else
        {
            StartCoroutine("ScrollRectContrl");
        }
    }
    IEnumerator ScrollRectContrl_ZY()
    {
        ZYGD_Scroll.verticalNormalizedPosition = ZYGD_Scroll.verticalNormalizedPosition - KXRY_Scroll_Time;
        yield return new WaitForSeconds(0.01f);
        if (ZYGD_Scroll.verticalNormalizedPosition <= 0)
        {
            StopCoroutine("ScrollRectContrl_ZY");
        }
        else
        {
            StartCoroutine("ScrollRectContrl_ZY");
        }
    }
    IEnumerator ScrollRectContrl_GJ()
    {
        FXGJ_Scroll.verticalNormalizedPosition = FXGJ_Scroll.verticalNormalizedPosition - KXRY_Scroll_Time;
        yield return new WaitForSeconds(0.01f);
        if (FXGJ_Scroll.verticalNormalizedPosition <= 0)
        {
            StopCoroutine("ScrollRectContrl_GJ");
        }
        else
        {
            StartCoroutine("ScrollRectContrl_GJ");
        }
    }

    public IEnumerator InitDataContrl()
    {
        //获取 人员组织规模
        StartCoroutine(httpRequestC.DoRequestGet("getStaffOrganization", data =>
        {
            Debug.Log("获取人员组织规模:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                TDGMRS_Text.text = jd2["totalTeam"].ToString();
                KCRY_SL_Text.text = jd2["reconnStaff"].ToString() + "人";
                TSRY_SL_Text.text = jd2["debugStaff"].ToString() + "人";
                SSRY_SL_Text.text = jd2["buildStaff"].ToString() + "人";

                if (jd2["controlStaff"] == null)
                {
                    GGRY_SL_Text.text = "null";
                }
                else
                {
                    GGRY_SL_Text.text = jd2["controlStaff"].ToString() + "人";
                }

                GSTD_SL_Text.text = jd2["companyTeam"].ToString();
                WWTD_SL_Text.text = jd2["outsourcingTeam"].ToString();
                WWDW_SL_Text.text = jd2["outsourcingCompany"].ToString();

                var chart = BarChart.GetComponent<BarChart>();
               
                chart.RemoveData();
                
                var xAxis = chart.GetOrAddChartComponent<XAxis>();
                xAxis.splitNumber = jd2["cityStaffList"].Count;
                
                var yAxis = chart.GetOrAddChartComponent<YAxis>();
                yAxis.type =  Axis.AxisType.Value;
                
              
                chart.AddSerie<Bar>("");

                for (int i = 0; i < jd2["cityStaffList"].Count; i++)
                {
                    if (jd2["cityStaffList"][i]["city"] == null)
                    {
                        chart.AddXAxisData(jd2["cityStaffList"][i]["city"].ToString().Replace("公司",""));
                        chart.AddData(0, int.Parse(jd2["cityStaffList"][i]["peopleCount"].ToString()));
                    }
                    else
                    {
                        chart.AddXAxisData(jd2["cityStaffList"][i]["city"].ToString().Replace("公司",""));
                        chart.AddData(0, int.Parse(jd2["cityStaffList"][i]["peopleCount"].ToString()));
                        
                    }
                }
            }
        }));
        //获取计划安排
        StartCoroutine(httpRequestC.DoRequestGet("planWorkOrder", data =>
        {
            Debug.Log("获取计划安排:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                
                DateTime yesterday = DateTime.Now.AddDays(-1);
                JiHuaAnPaiTime_Text.text = yesterday.Year + "/" + yesterday.Month + "/" + yesterday.Day + " 安排";
                KCYHS_SL_Text.text = jd2["surveyOmNumber"].ToString();
                AZYHS_SL_Text.text = jd2["installOmNumber"].ToString();
                TSYHS_SL_Text.text = jd2["debugOmNumber"].ToString();
                ZHNYDY_SL_Text.text = jd2["smartEnergySum"].ToString();
                FHJCY_SL_Text.text = jd2["loadDetectorSum"].ToString();
                KTKZMK_SL_Text.text = jd2["airConditioningModuleSum"].ToString();

                float f1 = Convert.ToSingle(jd2["completeSurveyOm"].ToString()) /Convert.ToSingle(jd2["surveyOmNumber"].ToString());
                float f2 = Convert.ToSingle(jd2["completeInstallOm"].ToString()) / Convert.ToSingle(jd2["installOmNumber"].ToString());
                float f3 = Convert.ToSingle(jd2["completeDebugOm"].ToString()) / Convert.ToSingle(jd2["debugOmNumber"].ToString());
                
             
                if (float.IsNaN(f1))
                {
                    Kc_Img.fillAmount = 0;
                    KC_JD_Text.text = "0";
                }
                else
                {
                    Kc_Img.fillAmount = f1;
                    
                    string str = (f1 * 100).ToString();
                    string[] strs = str.Split('.');
                    KC_JD_Text.text = strs[0];
                }

                if (float.IsNaN(f2))
                {
                    AZ_JD_Text.text = "0";
                    AZ_Img.fillAmount = 0;
                }
                else
                {
                    AZ_Img.fillAmount = f2;

                    string str = (f2 * 100).ToString();
                    string[] strs = str.Split('.');
                    AZ_JD_Text.text = strs[0];
                }

                if (float.IsNaN(f3))
                {
                    TS_Img.fillAmount = 0;

                    TS_JD_Text.text = "0";
                }
                else
                {
                    TS_Img.fillAmount = f3;
                    
                    string str = (f3 * 100).ToString();
                    string[] strs = str.Split('.');
                    TS_JD_Text.text = strs[0];
                }
            }
        }));

        //获取 风控预警
        StartCoroutine(httpRequestC.DoRequestGet("getRiskControlWarn", data =>
        {
            Debug.Log("获取风控预警:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                JianYiJG_SL_Text.text = jd2["warnStaff"].ToString();
                JianYiZH_SL_Text.text = jd2["recallStaff"].ToString();

                foreach (Transform item in FengKongYuJing_Content.transform)
                {
                    Destroy(item.gameObject);
                }

                FXGJ_Count = jd2["efficiencyList"].Count;
                for (int i = 0; i < jd2["efficiencyList"].Count; i++)
                {
                    GameObject obj = GameObject.Instantiate(FKYJ_Item, transform.position, transform.rotation);
                    obj.transform.SetParent(FengKongYuJing_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.GetComponent<FKYJ_ItemData>().Name_Text.text = jd2["efficiencyList"][i]["staffName"].ToString();
                    //obj.GetComponent<FKYJ_ItemData>().LeiXing_Text.text = jd2["efficiencyList"][i]["roleType"].ToString();
                    switch (jd2["efficiencyList"][i]["workOrderType"].ToString())
                    {
                        case "2":
                            obj.GetComponent<FKYJ_ItemData>().LeiXing_Text.text = "实施";
                            break;
                        case "3":
                            obj.GetComponent<FKYJ_ItemData>().LeiXing_Text.text = "调试";
                            break;
                    }
                    obj.GetComponent<FKYJ_ItemData>().DiQu_Text.text = jd2["efficiencyList"][i]["city"].ToString();
                    obj.GetComponent<FKYJ_ItemData>().GZXL_Text.text = jd2["efficiencyList"][i]["workEfficiency"].ToString() + "台/天";
                    //obj.GetComponent<FKYJ_ItemData>().SSDW_Text.text = jd2["efficiencyList"][i]["staffOrgName"].ToString();
                    SetTextWithEllipsis(obj.GetComponent<FKYJ_ItemData>().SSDW_Text,jd2["efficiencyList"][i]["staffOrgName"].ToString(),6);
                    if (i % 2 == 0)
                    {

                    }
                    else
                    {
                        obj.GetComponent<Image>().enabled = false;
                    }
                }
            }
            
            if (FXGJ_Count > 5)
            {
                StartCoroutine("ScrollRectContrl_GJ");
            } 
        }));
        
        //获取 统计累计数据
        StartCoroutine(httpRequestC.DoRequestGet("cumulativeData", data =>
        {
            Debug.Log("获取统计累计数据:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                LJKCGDCS_SL_Text.text = jd2["cumulativeSurveyOrder"].ToString();
                LJAZGDCS_SL_Text.text = jd2["cumulativeInstallOrder"].ToString();
                LJTSGDCS_SL_Text.text = jd2["cumulativeDebugOrder"].ToString();
                LJWCKCCS_SL_Text.text = jd2["cumulativeSurveyOm"].ToString();
                LJWCAZCS_SL_Text.text = jd2["cumulativeInstallOm"].ToString();
                LJWCTSCS_SL_Text.text = jd2["cumulativeDebugOrderOm"].ToString();
            }
        }));

        //获取 作业工单
        StartCoroutine(httpRequestC.DoRequestGet("getWorkOrder", data =>
        {
            Debug.Log("获取统计累计数据:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                JD_SL_Text.text = jd2["pendingOrderSum"].ToString();
                ZT_SL_Text.text = jd2["onTheWaySum"].ToString();
                WC_SL_Text.text = jd2["finishOrderSum"].ToString();
                foreach (Transform item in ZuoYeGongDanContent.transform)
                {
                    Destroy(item.gameObject);
                }

                ZYGD_Count = jd2["wordOrderInfoIndexVO"].Count;
                for (int i = 0; i < jd2["wordOrderInfoIndexVO"].Count; i++)
                {
                    GameObject obj = GameObject.Instantiate(ZYGD_Item, transform.position, transform.rotation);
                    obj.transform.SetParent(ZuoYeGongDanContent.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);

                    if (jd2["wordOrderInfoIndexVO"][i]["omUserName"] == null)
                    {
                        obj.GetComponent<ZYGD_ItemData>().Name_Text.text = "Null";
                    }
                    else
                    {
                        //obj.GetComponent<ZYGD_ItemData>().Name_Text.text = jd2["wordOrderInfoIndexVO"][i]["omUserName"].ToString();
                        SetTextWithEllipsis(obj.GetComponent<ZYGD_ItemData>().Name_Text,jd2["wordOrderInfoIndexVO"][i]["omUserName"].ToString(),5);
                    }

                    if (jd2["wordOrderInfoIndexVO"][i]["workOrderType"].ToString() == "1")
                    {
                        obj.GetComponent<ZYGD_ItemData>().LeiXing_Text.text = "勘察工单";
                    }
                    else if (jd2["wordOrderInfoIndexVO"][i]["workOrderType"].ToString() == "2")
                    {
                        obj.GetComponent<ZYGD_ItemData>().LeiXing_Text.text = "实施工单";
                    }
                    else if (jd2["wordOrderInfoIndexVO"][i]["workOrderType"].ToString() == "3")
                    {
                        obj.GetComponent<ZYGD_ItemData>().LeiXing_Text.text = "调试工单";
                    }
                    else if (jd2["wordOrderInfoIndexVO"][i]["workOrderType"].ToString() == "4")
                    {
                        obj.GetComponent<ZYGD_ItemData>().LeiXing_Text.text = "安装调试";
                    }

                    if (jd2["wordOrderInfoIndexVO"][i]["city"] == null)
                    {
                        obj.GetComponent<ZYGD_ItemData>().DiQu_Text.text = "Null";
                    }
                    else
                    {
                        obj.GetComponent<ZYGD_ItemData>().DiQu_Text.text =
                            jd2["wordOrderInfoIndexVO"][i]["city"].ToString();
                    }

                    if (jd2["wordOrderInfoIndexVO"][i]["engineerName"] == null)
                    {
                        obj.GetComponent<ZYGD_ItemData>().RenYuan_Text.text = "Null";
                    }
                    else
                    {
                       // obj.GetComponent<ZYGD_ItemData>().RenYuan_Text.text = jd2["wordOrderInfoIndexVO"][i]["engineerName"].ToString();
                       SetTextWithEllipsis(obj.GetComponent<ZYGD_ItemData>().RenYuan_Text,jd2["wordOrderInfoIndexVO"][i]["engineerName"].ToString(),5);
                    }

                    //obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = jd2["wordOrderInfoIndexVO"][i]["status"].ToString();
                    switch (jd2["wordOrderInfoIndexVO"][i]["status"].ToString())
                    {
                        case "1":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待下发";
                            break;
                        case "91":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "回退待重新下发";
                            break;
                        case "2":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待指派";
                            break;
                        case "92":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "回退待重新指派";
                            break;
                        case "3":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待接单";
                            break;
                        case "4":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待签到";
                            break;
                        case "5":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "勘查中";
                            break;
                        case "6":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待重新提交";
                            break;
                        case "7":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "待完工确认";
                            break;
                        case "10":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "已归档";
                            break;
                        case "11":
                            obj.GetComponent<ZYGD_ItemData>().ZhuangTai_Text.text = "作废";
                            break;
                    }

                    if (i % 2 == 0)
                    {

                    }
                    else
                    {
                        obj.GetComponent<Image>().enabled = false;
                    }
                }
                
                if (ZYGD_Count > 5)
                {
                    StartCoroutine("ScrollRectContrl_ZY");
                }
            }
        }));

        //获取 实施效率
        StartCoroutine(httpRequestC.DoRequestGet("operationEfficiency", data =>
        {
            Debug.Log("获取实施效率:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                HWKCSC_SL_Text.text = jd2["surveyDuration"].ToString();
                HWANSC_SL_Text.text = jd2["installDuration"].ToString();
                HWTSSC_SL_Text.text = jd2["debugDuration"].ToString();
                HWKCXH_SL_Text.text = jd2["surveyConsume"].ToString();
                HWAZXH_SL_Text.text = jd2["installConsume"].ToString();
                HWTSXH_SL_Text.text = jd2["debugConsume"].ToString();

                var chart = BarChart2.GetComponent<BarChart>();
               
                
                var xAxis = chart.GetOrAddChartComponent<XAxis>();
                xAxis.splitNumber = jd2["efficiencyHistogramVO"].Count;
                
                var yAxis = chart.GetOrAddChartComponent<YAxis>();
                yAxis.type =  Axis.AxisType.Value;
                 
                chart.RemoveData();
                chart.AddSerie<Bar>(""); 
                chart.GetSerie(0).serieName = "户均勘查时长";
                chart.AddSerie<Bar>("");
                chart.GetSerie(1).serieName = "户均安装时长";
                chart.AddSerie<Bar>("");
                chart.GetSerie(2).serieName = "户均调试时长";
                
                 

                for (int i = 0; i < jd2["efficiencyHistogramVO"].Count; i++)
                {
                    chart.AddXAxisData(jd2["efficiencyHistogramVO"][i]["orgName"].ToString());
                    chart.AddData(0, Double.Parse(jd2["efficiencyHistogramVO"][i]["surveyDuration"].ToString()));
                    chart.AddData(1, Double.Parse(jd2["efficiencyHistogramVO"][i]["installDuration"].ToString()));
                    chart.AddData(2, Double.Parse(jd2["efficiencyHistogramVO"][i]["debugDuration"].ToString()));
                }
            }
        }));
        
        //获取 空闲人员
        StartCoroutine(httpRequestC.DoRequestGet("getIdleStaff", data =>
        {
            Debug.Log("获取空闲人员:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                KC_SL_Text.text = jd2["reconnStaff"].ToString();
                AZ_SL_Text.text = jd2["buildStaff"].ToString();
                TS_SL_Text.text = jd2["debugStaff"].ToString();

                foreach (Transform item in Content.transform)
                {
                    Destroy(item.gameObject);
                }

                KXRY_Count = jd2["idleStaffInfoVOList"].Count;
                for (int i = 0; i < jd2["idleStaffInfoVOList"].Count; i++)
                {
                    GameObject obj = GameObject.Instantiate(KXRY_Item_Prefab, transform.position, transform.rotation);
                    obj.transform.SetParent(Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.GetComponent<KXRY_ItemData>().Name_Text.text =
                        jd2["idleStaffInfoVOList"][i]["staffName"].ToString();
                    obj.GetComponent<KXRY_ItemData>().JiNeng_Text.text =
                        jd2["idleStaffInfoVOList"][i]["skill"].ToString();

                    if (jd2["idleStaffInfoVOList"][i]["city"] == null)
                    {
                        obj.GetComponent<KXRY_ItemData>().DiQu_Text.text = "Null";
                    }
                    else
                    {
                        obj.GetComponent<KXRY_ItemData>().DiQu_Text.text =
                            jd2["idleStaffInfoVOList"][i]["city"].ToString();
                    }

                    if (jd2["idleStaffInfoVOList"][i]["duration"] == null)
                    {
                        obj.GetComponent<KXRY_ItemData>().Time_Text.text = "0h";
                    }
                    else
                    {
                        obj.GetComponent<KXRY_ItemData>().Time_Text.text =
                            jd2["idleStaffInfoVOList"][i]["duration"].ToString() + "h";
                    }

                    if (i % 2 == 0)
                    {

                    }
                    else
                    {
                        obj.GetComponent<Image>().enabled = false;
                    }
                }
                if (KXRY_Count > 5)
                {
                    StartCoroutine("ScrollRectContrl");
                }
            }
            
        }));
        
        //调试工单接入设备数
        StartCoroutine(httpRequestC.DoRequestGet("getAccessDevice", data =>
        {
            Debug.Log("获取空闲人员:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "200")
            {
                YiJieRuDY_SL_Text.text = jd2["smartEnergyTotal"].ToString();
                YiJieRuJJ_SL_Text.text = jd2["loadDetectorTotal"].ToString();
                ZuoRiDY_SL_Text.text = jd2["airConditioningModuleTotal"].ToString();
                ZuoRiJJ_SL_Text.text = jd2["smartEnergyYday"].ToString();
                YiJieRuKT_SL_Text.text = jd2["loadDetectorYday"].ToString();
                ZuoRiKT_SL_Text.text = jd2["airConditioningModuleYday"].ToString();
            }
        }));
        
        
        yield return new WaitForSeconds(10f);
        KXRY_Scroll.verticalNormalizedPosition = 1;
        ZYGD_Scroll.verticalNormalizedPosition = 1;
        FXGJ_Scroll.verticalNormalizedPosition = 1;
        
        StartCoroutine("InitDataContrl");
        Resources.UnloadUnusedAssets();
    }

    public string DictionaryTostr(Dictionary<string, string> param)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var entry in param)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(entry.Key)) || null == entry.Value)
            {
                continue;
            }

            builder.Append(Convert.ToString(entry.Key) + "=" + Convert.ToString(entry.Value) + "&");
        }

        return builder.ToString().Substring(0, builder.ToString().LastIndexOf("&"));
        ;
    }
    public static void SetTextWithEllipsis(Text textComponent, string value, int characterVisibleCount)
    {
 
        var updatedText = value;
 
        // 判断是否需要过长显示省略号
        if (value.Length > characterVisibleCount)
        {
            updatedText = value.Substring(0, characterVisibleCount - 1);
            updatedText += "…";
        }
 
        // update text
        textComponent.text = updatedText;
    }
}
