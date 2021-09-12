using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;

    void Start()
    {
        NextDongle();
    }

    private Dongle Getdongle()
    {
        GameObject instant = Instantiate(donglePrefab, dongleGroup);
        Dongle dongle = instant.GetComponent<Dongle>();
        return dongle;
    }
    private void NextDongle()
    {
        Dongle newDongle = Getdongle();
        lastDongle = newDongle;
    }

    public void TouchDown()
    {
        if (lastDongle == null)
        {
            return;
        }

        lastDongle.Drage();
    }

    public void TouchUp()
    {
        if (lastDongle == null)
        {
            return;
        }

        lastDongle.Drop();
        lastDongle = null;
    }
}