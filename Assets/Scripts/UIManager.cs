using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int health;
    public int energy;
    public int ammunition;

    public int numOfHearts;
    public int numOfEnergyBars;
    public int numOfAmmunitions;

    public Image[] hearts;
    public Image[] energyBars;
    public Image[] ammunitions;

    public Sprite fullHeart;
    public Sprite fullEnergy;
    public Sprite fullAmmunition;

    // Update is called once per frame
    void Update()
    {
        for (int idxHeart = 0; idxHeart < hearts.Length; ++idxHeart)
        {
            if (idxHeart < numOfHearts)
                hearts[idxHeart].enabled = true;
            else
                hearts[idxHeart].enabled = false;
        }

        for (int idxEnergy = 0; idxEnergy < energyBars.Length; ++idxEnergy)
        {
            if (idxEnergy < numOfEnergyBars)
                energyBars[idxEnergy].enabled = true;
            else
                energyBars[idxEnergy].enabled = false;
        }

        for (int idxAmmunition = 0; idxAmmunition < ammunitions.Length; ++idxAmmunition)
        {
            if (idxAmmunition < numOfAmmunitions)
                ammunitions[idxAmmunition].enabled = true;
            else
                ammunitions[idxAmmunition].enabled = false;
        }
    }
}
