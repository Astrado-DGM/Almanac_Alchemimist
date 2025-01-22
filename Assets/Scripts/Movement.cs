using UnityEngine;
using System.Threading;
using System;
public class Movement : MonoBehaviour
{
    
    public float tiempo = 0.4f;//tiempo entre permitir moverse, si no existe se movera a velocidad luz por frame, donde alteramos las cordenadas
    
    public float tiempoRotar=0.5f;
    private bool canMove = true; //bool , puede moverse, desactivar tras moverse, acivar tras 1.5 segundos
    private bool canRotate = true;
    private int[,] mapa;
    public int coordX;
    public int coordY;
    
    private int largo;
    private int ancho;
    void Start(){
        Rigidbody rb = GetComponent<Rigidbody>();if(rb != null){rb.isKinematic = true;}
        GameObject.Find("Main Camera").GetComponent<Transform>().position = transform.position;
        actualizaMapa();
        actualizaPos();
        ajustaCamera();
        traeCamara();
    }
    private void actualizaMapa(){
        this.mapa = GameObject.Find("DM").GetComponent<DM_generacion_cuevas>().getMapa();
        largo = this.mapa.GetLength(1);
        ancho = this.mapa.GetLength(0);
        actualizaPos();
    }

    private void timeR(){
        if(canRotate== true){return;}
        for(float i=0f;i<tiempoRotar;i+=0.1f){
            Thread.Sleep(100);
        }
        canRotate=true;
        return;
    }
    private void timeM(){
        if(canMove==true){return;}
        for(float i=0f;i<tiempo;i+=0.1f){
            Thread.Sleep(100);
        }
        canMove = true;
        return;
    }

    void Update()
    {
        
        
        if(Input.GetKey("e") && canRotate==true){
            giraCamara(90f);
            canRotate = false;
            Thread timerRotar = new Thread(()=>timeR());
                timerRotar.Start();
        }
        if(Input.GetKey("q") && canRotate==true){
            giraCamara(-90f);
            canRotate = false;
            Thread timerRotar = new Thread(()=>timeR());
                timerRotar.Start();
        }
        
        //movimiento jugador,       TODO revisar distancia y tomar en cuenta para casillas
        if (Input.GetKey("w") && canMove) {
        
            transform.Translate(0,0,2);//self x cord +2
            actualizaPos();
            if((coordX>=largo || coordX <0 || coordY>=ancho||coordY<0)){
                
                transform.Translate(0,0,-2);//self x cord +2
                actualizaPos();
            }else if(mapa[coordX,coordY]==1){
                
                transform.Translate(0,0,-2);//self x cord +2
                    actualizaPos();
            }else{
                //transform.Translate(x,y,z);
                canMove = false;
                traeCamara();
                Thread timerMovimiento = new Thread(()=>timeM());
                timerMovimiento.Start();
            }
        }
        if (Input.GetKey("s") && canMove) {
            
                
                transform.Translate(0,0,-2);//self x cord +2
                actualizaPos();
                if((coordX>=largo || coordX <0 || coordY>=ancho||coordY<0)){
                    
                    transform.Translate(0,0,2);//self x cord +2
                    actualizaPos();
                }else if(mapa[coordX,coordY]==1){
                    
                    transform.Translate(0,0,2);//self x cord +2
                     actualizaPos();
                }else{                   
                    
                    //transform.Translate(x,y,z);
                    canMove = false;
                    traeCamara();
                    Thread timerMovimiento = new Thread(()=>timeM());
                    timerMovimiento.Start();
                }
            }
        if (Input.GetKey("a") && canMove) {
            
                
                
                transform.Translate(-2,0,0);//self x cord +2
                actualizaPos();
                if((coordX>=largo || coordX <0 || coordY>=ancho||coordY<0)){
                    
                    transform.Translate(2,0,0);//self x cord +2
                    actualizaPos();
                }else if(mapa[coordX,coordY]==1){
                    
                    transform.Translate(2,0,0);//self x cord +2
                     actualizaPos();
                }else{
                                        
                    //transform.Translate(x,y,z);
                    canMove = false;
                    traeCamara();
                    Thread timerMovimiento = new Thread(()=>timeM());
                    timerMovimiento.Start();
                }
                
            
        }
        if (Input.GetKey("d") && canMove) {
            
                
                transform.Translate(2,0,0);//self x cord +2
                actualizaPos();
                if((coordX>=largo || coordX <0 || coordY>=ancho||coordY<0)){
                    
                    transform.Translate(-2,0,0);//self x cord +2
                    actualizaPos();
                }else if(mapa[coordX,coordY]==1){
                    transform.Translate(-2,0,0);//self x cord +2
                     actualizaPos();
                }else{
                    //transform.Translate(x,y,z);
                    canMove = false;
                    traeCamara();
                    Thread timerMovimiento = new Thread(()=>timeM());
                    timerMovimiento.Start();
                }
        }
    }
    private void actualizaPos(){
        
        int valx =(int)Math.Round(transform.position.x) ;
        int valy = (int)Math.Round(transform.position.z) ;
        coordX = valx/2;
        coordY = valy/2;
    }
    private void traeCamara(){
        GameObject.Find("Main Camera").GetComponent<Transform>().position = transform.position;
    }
    private void giraCamara(float grado){
        GameObject.Find("Main Camera").GetComponent<Transform>().Rotate(0f,grado,0f,Space.Self);
        transform.Rotate(0f,grado,0f,Space.Self);
    }
    private void ajustaCamera(){
        GameObject.Find("Main Camera").GetComponent<Transform>().rotation = transform.rotation;
    }
    public Vector3 getVector3Player(){
        return transform.position;
    }
}
