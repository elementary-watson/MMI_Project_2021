using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Reference_Script : MonoBehaviour
{
    //lights
    [SerializeField] GameObject lights_ref;
    [SerializeField] Lightswitch_Logic lsLogic_object;
    //Elec
    [SerializeField] GameObject mnb_ref;
    [SerializeField] MainNumberBox mnb_object;
    [SerializeField] GameObject meb_ref;
    [SerializeField] MainElecBox meb_object;
    [SerializeField] GameObject mcc_ref;
    [SerializeField] MainClickCabinet mcc_object;
    //Unterhaltung
    [SerializeField] GameObject mWaterDis_ref;
    [SerializeField] MainWaterDispenser mWaterDis_object;
    [SerializeField] GameObject msGame__ref;
    [SerializeField] MainSkriptGame msGame_object;
    [SerializeField] GameObject mRadionN_ref;
    [SerializeField] MainRadioNumber mRadionN_object;
    //Energy
    [SerializeField] GameObject fillGauge_ref;
    [SerializeField] Main_Fillgauge_Task fillGauge_object;
    [SerializeField] GameObject mLever_ref;
    [SerializeField] MainLeverScript mLever_object;
    [SerializeField] GameObject mEnergNum_ref;
    [SerializeField] MainEnergyNumberScript mEnergNum_object;
    //Labor
    [SerializeField] GameObject mSingleTube_ref;
    [SerializeField] MainSingleTubeScript mSingleTube_object;
    [SerializeField] GameObject mComp_ref;
    [SerializeField] MainComputer mComp_object;
    [SerializeField] GameObject mClickLab_ref;
    [SerializeField] MainClickLabor mClickLab_object;
    //Medical
    [SerializeField] GameObject mTablet_ref;
    [SerializeField] MainTablet mTablet_object;
    [SerializeField] GameObject mClMediKit_ref;
    [SerializeField] MainClickMediKit mClMediKit_object;
    [SerializeField] GameObject mSinkTask_ref;
    [SerializeField] Main_Sink_Task mSinkTask_object;


    void Start()
    {
        
    }
    public void callSetupAll()
    {
        lsLogic_object.setup();

        mnb_object.setup();
        meb_object.setup();
        mcc_object.setup();

        mWaterDis_object.setup();
        msGame_object.setup();
        mRadionN_object.setup();

        fillGauge_object.setup();
        mLever_object.setup();
        mEnergNum_object.setup();

        mSingleTube_object.setup();
        mComp_object.setup();
        mClickLab_object.setup();

        mTablet_object.setup();
        mClMediKit_object.setup();
        mSinkTask_object.setup();


        lights_ref.SetActive(false);

        mnb_ref.SetActive(false);
        meb_ref.SetActive(false);
        mcc_ref.SetActive(false);

        mWaterDis_ref.SetActive(false);
        msGame__ref.SetActive(false);
        mRadionN_ref.SetActive(false);

        fillGauge_ref.SetActive(false);
        mLever_ref.SetActive(false);
        mEnergNum_ref.SetActive(false);

        mSingleTube_ref.SetActive(false);
        mComp_ref.SetActive(false);
        mClickLab_ref.SetActive(false);

        mTablet_ref.SetActive(false);
        mClMediKit_ref.SetActive(false);
        mSinkTask_ref.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
