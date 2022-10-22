using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PK.SliderContoller
{
    public class SliderController : MonoBehaviour
    {
        #region SerializeField
        [Tooltip("if we are going to use image, assign it here")]
        [SerializeField] private Image fillImage;
        [Tooltip("if we are going to use slider, assign it here")]
        [SerializeField] private Slider slider;
        [Tooltip("Energy drop time interval")]
        [SerializeField] private float dropRate;
        [Tooltip("Max energy player can have")]
        [SerializeField] private float maxEnergy;
        [Tooltip("Amount of energy falling at certain intervals")]
        [SerializeField] private float dropAmount;
        [Tooltip("player's initial energy")]
        [SerializeField] private float energy;
        [Tooltip("the player starts running when the energy exceeds this value")]
        [SerializeField] private float theAmounOfEnergyForRun;
        #endregion

        #region private
        private float currentTime;
        private float getBackOriginalAmountTimer;
        private bool startReducingEnergy = true;
        private Coroutine getBackOriginalAmountCoroutine;
        #endregion

        public float EnergySetter
        {
            get { return energy; }
            set
            {
                energy = value;
                if (energy >= theAmounOfEnergyForRun) PlayerCanRun();
                else PlayerCantRun();
            }
        }
        //is called when the player has not enough energy to run.
        private void PlayerCantRun()
        {
            Debug.Log("Player not Runing");
        }
        //is called when the player has enough energy to run.
        private void PlayerCanRun()
        {
            Debug.Log("Player runing");
        }

        private void Awake()
        {
            currentTime = dropRate;
            slider.maxValue = maxEnergy;
        }

        private void OnEnable()
        {
            AddEnergySignal.AddEnergyEvent += AddEnergyAndUpdateSlider;
            ChangeAmountSignal.ChangeAmountEvent += ChangeDropAmount;
        }

        private void OnDisable()
        {
            AddEnergySignal.AddEnergyEvent -= AddEnergyAndUpdateSlider;
            ChangeAmountSignal.ChangeAmountEvent -= ChangeDropAmount;

        }

        private void Update()
        {
            if (!startReducingEnergy) return;
            EnergyDropAndSliderUpdate();
        }

        private void EnergyDropAndSliderUpdate()
        {
            currentTime -= Time.deltaTime;
            if (currentTime > 0) return;
            EnergySetter -= dropAmount;
            if (fillImage != null)
            {
                float _energy = EnergySetter / maxEnergy;
                fillImage.fillAmount = _energy;
            }
            if (slider != null) slider.value = energy;
            if (EnergySetter <= 0) EnergyOver();
            currentTime = dropRate;
        }

        //Called once when energy is 0
        private void EnergyOver()
        {

        }
       
        private void AddEnergyAndUpdateSlider(float amaount)
        {
            EnergySetter += amaount;
            if (EnergySetter > maxEnergy) EnergySetter = maxEnergy;
            if (fillImage != null) fillImage.fillAmount = energy / maxEnergy;
            if (slider != null) slider.value = energy;
        }

        private void ChangeDropAmount(float amount, float time)
        {
            if (getBackOriginalAmountCoroutine != null)
            {
                getBackOriginalAmountTimer += time;
                return;
            }
            else
            {
                getBackOriginalAmountTimer = time;
                getBackOriginalAmountCoroutine = StartCoroutine(GetBackOriginalAmount(amount));
            }
        }

        private IEnumerator GetBackOriginalAmount(float amount)
        {
            float original = dropAmount;
            dropAmount = amount;
            while (getBackOriginalAmountTimer > 0)
            {
                getBackOriginalAmountTimer--;
                Debug.Log(getBackOriginalAmountTimer);
                yield return new WaitForSeconds(1);
            }
            dropAmount = original;
            getBackOriginalAmountCoroutine = null;
        }
    }

}

public class ChangeAmountSignal
{
    public static event Action<float, float> ChangeAmountEvent;
    public static void Trigger(float amount, float time)
    { ChangeAmountEvent?.Invoke(amount, time); }
}
public class AddEnergySignal
{
    public static event Action<float> AddEnergyEvent;
    public static void Trigger(float amount)
    { AddEnergyEvent?.Invoke(amount); }
}
