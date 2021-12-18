using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    private Player Player;

    public LevelLoader levelLoader;

    private EnemyCount enemyCount;

    public float HPBonus = 0;
    public float MPBonus = 0;
    public float SpeedBonus = 0;
    public float DarkSphereDamageBonus = 0;
    public float DarkSphereSpeedBonus = 0;
    public float TeleportCooldownBonus = 0;
    public float SkeletonHPBonus = 0;
    public float SkeletonAtkBonus = 0;
    public float SkeletonSpdBonus = 0;
    public float AbsorbHPBonus = 0;

    public Text UpgradeDescription;
    public string UpgradeDescriptionText;

    private Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();

        Player = FindObjectOfType<Player>();

        //levelLoader = FindObjectOfType<LevelLoader>();

        enemyCount = FindObjectOfType<EnemyCount>();
    }

    public void IncreaseStatus() {
        if (enemyCount.UpgradeSelected == false)
        {
            Player.HPBonus += HPBonus;
            Player.HP += HPBonus;
            Player.MPBonus += MPBonus;
            Player.MP += MPBonus;
            Player.SpeedBonus += SpeedBonus;
            Player.DarkSphereDamageBonus += DarkSphereDamageBonus;
            Player.DarkSphereSpeedBonus += DarkSphereSpeedBonus;
            Player.TeleportCooldownBonus += TeleportCooldownBonus;
            Player.SkeletonHPBonus += SkeletonHPBonus;
            Player.SkeletonAtkBonus += SkeletonAtkBonus;
            Player.SkeletonSpdBonus += SkeletonSpdBonus;
            Player.AbsorbHPBonus += AbsorbHPBonus;

            EnemyCount.floor--;
            enemyCount.UpgradeSelected = true;

            AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);

            levelLoader.LoadThisLevel();
        }
    }

    public void OnOver()
    {
        trans.localScale = new Vector2(2f, 2f);
        UpgradeDescription.text = UpgradeDescriptionText;
    }

    public void NotOnOver()
    {
        trans.localScale = new Vector2(1f, 1f);
        UpgradeDescription.text = "";
    }


}
