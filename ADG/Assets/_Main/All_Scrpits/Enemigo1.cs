using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemigo1 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;


    void start()
    {
        ani = GetComponent<Animator>();
    }

public void Comportamiento_Enemigo()
    {
        cronometro += 1 * Time.deltaTime;

        if(cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
            ani.SetBool("walk", false);
            break;

            case 1:
            grado = Random.Range(0, 360);
            rutina++;
            break;

            case 2:
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
            ani.SetBool("walk", true);
            break;



        }
    }

void Update()
    {
        Comportamiento_Enemigo();
    }

}

