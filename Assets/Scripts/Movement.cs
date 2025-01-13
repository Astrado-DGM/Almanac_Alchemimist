using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public float tiempo = 1.5f;//tiempo entre permitir moverse, si no existe se movera a velocidad luz por frame, donde alteramos las cordenadas
    public bool canMove = true; //bool , puede moverse, desactivar tras moverse, acivar tras 1.5 segundos

    void Start()
    {
        tiempo = 1.5f;
    }
 

    void Update()
    {
            //timer movimiento
        if(canMove == false){
            tiempo -= Time.deltaTime;
            if(tiempo <= 0.0f){
                canMove = true;
                tiempo = 1.5f;
            }   
        }

        //movimiento jugador,       TODO revisar distancia y tomar en cuenta para casillas
        if (Input.GetKey(KeyCode.UpArrow) && canMove) {
            transform.Translate(0,0,2);//self z cord + 2
	        
            canMove = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) && canMove) {
	        transform.Translate(0,0,-2);//self z cord -2
            canMove = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && canMove) {
	        transform.Translate(-2,0,0);//self x cord -2
            //transform.Translate(x,y,z);
            canMove = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && canMove) {
	        transform.Translate(2,0,0);//self x cord +2
            canMove = false;
        }
    }
}
