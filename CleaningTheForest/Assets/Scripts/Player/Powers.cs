using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powers : MonoBehaviour
{
    private Player player;

    [Header("Powers")]

    public GameObject[] powers;
    public int selectedPower;
    private bool isChangingPower;

    [Header("Charge Power")]

    public float chargePowerLimit;
    public float chargePower = 0;
    public bool isPowerCharged;

    [Header("UI")]

    public GameObject powersPanel;

    public GameObject[] childPowerPanel;

    public GameObject chargeBar;
    public GameObject fillChargeBar;

    public Text powerTextName;
    public Text powerTextCost;

    public Animator powerDescriptionAnim;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (player.Started)
        {
            OnFillBar();

            OnChargePower();
        }
    }

    void Update()
    {
        if (player.Started)
        {
            OnChangePower();
        }
    }

    void OnChangePower() {

        if (Input.GetKeyDown(KeyCode.S)) {
            StartCoroutine(NextPower());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(PreviousPower());
        }

        Debug.Log("" + selectedPower);
        chargePowerLimit = powers[selectedPower].GetComponent<PowerCost>().chargeTime;
        powerTextName.text = powers[selectedPower].GetComponent<PowerCost>().powerName;
        powerTextCost.text = "" + powers[selectedPower].GetComponent<PowerCost>().cost;
        ChangePowerAnimation();

    }

    IEnumerator NextPower() {
        if (!isChangingPower)
        {
            selectedPower++;
            if (selectedPower > powers.Length - 1)
            {
                selectedPower = 0;
            }

            powersPanel.GetComponent<Animator>().SetTrigger("Next");
            isChangingPower = true;

            powerDescriptionAnim.SetTrigger("Enabled");
            powerDescriptionAnim.Play("DescriptionDisabled");

            yield return new WaitForSeconds(0.5f);

            isChangingPower = false;
        }
    }

    IEnumerator PreviousPower() {
        if (!isChangingPower)
        {
            selectedPower--;
            if (selectedPower < 0)
            {
                selectedPower = powers.Length - 1;
            }

            powersPanel.GetComponent<Animator>().SetTrigger("Back");
            isChangingPower = true;

            yield return new WaitForSeconds(0.5f);

            powerDescriptionAnim.SetTrigger("Enabled");

            isChangingPower = false;
        }

    }

    public void ChangePowerAnimation() {
        childPowerPanel[selectedPower].transform.SetAsLastSibling();
    }

    void OnFillBar() {
        fillChargeBar.GetComponent<Transform>().localScale = new Vector3((chargePower/chargePowerLimit), 1, 1);

        if (player.comboCount >= powers[selectedPower].GetComponent<PowerCost>().cost && chargePower > 0)
        {
            chargeBar.SetActive(true);
        }
        else {
            chargeBar.SetActive(false);
        }
    }

    void OnChargePower()
    {
        if (player.comboCount >= powers[selectedPower].GetComponent<PowerCost>().cost && player.isShielded == false)
        {
            // Charging the power
            if (Input.GetKey(KeyCode.Space))
            {
                chargePower += Time.timeScale;

                if (chargePower >= chargePowerLimit)
                {
                    chargePower = chargePowerLimit;

                    isPowerCharged = true;
                }
            }
            else
            {
                if (isPowerCharged) {
                    player.PlayerGFX.SetTrigger("Attack");
                    Instantiate(powers[selectedPower], player.transform.position, Quaternion.identity);
                    player.comboCount -= powers[selectedPower].GetComponent<PowerCost>().cost;
                }
                chargePower = 0;
                isPowerCharged = false;
            }
        }
        else {
            chargePower = 0;
            isPowerCharged = false;
        }
    }

}
