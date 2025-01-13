using UnityEngine;
using System.Collections.Generic;

public class DM_generacion_cuevas : MonoBehaviour
{
    [Header("Valores referencia")]
    public float Multiplicador_Perlin = 10f;//Multiplcador de sonido perlin
    public int ancho = 56; //ancho mapa
    public int largo = 56; //largo mapa     Aumentar conforme se avanza de nivel, talvez +2
    public float offsetX ; //variable azar que modifica el sonido perlin
    public float offsetY ; //variable azar que modifica el sonido perlin
    [Header("Objeto suelo")]
    public GameObject casilla;//referencia al objeto que genera como suelo
    
    //lista de objetos que genera para eliminarla luego
    private List<GameObject> listaCasillas = new List<GameObject>();
    private int[ , ] mapa ;//array de 0 y 1, representa el mapa generado, 0 siendo el suelo

    const int suelo=0;
    const int vacio=1;
    const int sueloReal=2;
    void Start(){ 
        fillMap();
    }
    
    
    void Update(){}
    
    public int[,] getMap(){//devuelve el array del mapa
        return this.mapa;
    }
    void OnGUI(){//boton de pruebas, actualiza el mapa
        if (GUI.Button(new Rect(10, 10, 150, 100), "Actualizar")){
            emptyMap();
            fillMap();
        }
    }

    private void emptyMap(){//repasamos lista de objetos suelo y los eliminamos
        for (int i = 0; i < listaCasillas.Count; i++){
            Destroy(listaCasillas[i]);
        }
        listaCasillas.Clear();
    }
    
    private void fillMap(){//crea el mapa y llena los suelos
        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
        mapa = new int [ancho, largo];    //inicializamos los valores offset y recreamos el mapa

        for (int i = 0; i < ancho; i++){//por el tamaño y el largo creamos el mapa y generamos el suelo
            for (int j = 0; j < largo; j++){
                    float innerI = (float)i / largo * Multiplicador_Perlin + offsetX;//calculo de valor que va dentro del perlin
                    float innerJ = (float)j / ancho * Multiplicador_Perlin + offsetY;
                    float perlin = Mathf.PerlinNoise(innerI, innerJ); //tomamos los valores calculados y los metemos en la funcion perlin

                    if (perlin > 0.5f){//si el valor es mayor a .5 marcamos en el mapa 1, esto representara las paredes
                        mapa[i, j] = vacio;
                    }
                    else{//si es menor a .5, en el mapa se pone 0 y se genera en la respectiva coordenada el suelo
                        mapa[i, j] = suelo;          
                        //GameObject nuevaCasilla = Instantiate(casilla,new Vector3(0 + (i * 2), 1, 0 + (j * 2)),Quaternion.Euler(new Vector3(90, 0, 0)));
                        //listaCasillas.Add(nuevaCasilla);//el objeto suelo se añade a la lista de casillas para futura referencia del objeto
                    }          
                }
            }
            micelio();
            creaSuelo();
        }
        
    
    private void creaSuelo(){
        for(int i=0;i<ancho;i++){
            for(int j=0;j<largo;j++){
                if(mapa[i,j]==sueloReal){
                    GameObject nuevaCasilla = Instantiate(casilla,new Vector3(0 + (i * 2), 1, 0 + (j * 2)),Quaternion.Euler(new Vector3(90, 0, 0)));
                    listaCasillas.Add(nuevaCasilla);//el objeto suelo se añade a la lista de casillas para futura referencia del objeto
                }
            }
        }
    }
        //Sistema micelio

    
    
    private int tamanoActual=0;
    private int tamanoNuevo=0;    
     private List<int> coordenadasActualesX = new List<int>();
     private List<int> coordenadasActualesY = new List<int>();
     private List<int> coordenadasNuevasX = new List<int>();
     private List<int> coordenadasNuevasY = new List<int>();
    private void micelio(){
     


        for(int i=0;i<ancho;i++){
            for(int j=0;j<largo;j++){
                if(mapa[i,j]==suelo){
                    tamanoNuevo=0;
                    coordenadasNuevasX.Clear();
                    coordenadasNuevasY.Clear();
                    expandeVecinos(i,j);
                }
                if (tamanoNuevo >= tamanoActual)
                {
                    tamanoActual = tamanoNuevo;
                    // Guardamos las coordenadas de la regin "ganadora"
                    coordenadasActualesX = new List<int>(coordenadasNuevasX);
                    coordenadasActualesY = new List<int>(coordenadasNuevasY);
                }
                else
                {
                    // Descartamos esta regin
                    for (int k=0; k < coordenadasNuevasX.Count; k++)
                    {
                        int x = coordenadasNuevasX[k];
                        int y = coordenadasNuevasY[k];
                        mapa[x, y] = vacio;
                    }
                    tamanoNuevo=0;
                }
            }
        }
        coordenadasActualesX.Clear();
        coordenadasActualesY.Clear();
        coordenadasNuevasX.Clear();
        coordenadasNuevasY.Clear();
        tamanoActual=0;
        tamanoNuevo=0;

    }
    private void expandeVecinos(int x,int y){
        if(mapa[x,y]!=suelo){return;}
            mapa[x,y]=sueloReal;
            tamanoNuevo++;
            coordenadasNuevasX.Add(x);
            coordenadasNuevasY.Add(y);
        
        if(x+1<ancho){
            expandeVecinos(x+1,y);
        }
        if(x-1>=0){
            expandeVecinos(x-1,y);
        }
        if(y+1<largo){
            expandeVecinos(x,y+1);
        }
        if(y-1>=0){
            expandeVecinos(x,y-1);
        }
    }
    //TODO , revisar que el camino mas largo y setearlo como truepath, de modo que no haya islas ***
    // O TODO, conectar todas las islas flotantes con el mas grande *
    // o TODO, ambos, ignorar las mas leanas y si es por 1 sola casilla de diferencia conectarla **
    //TODO revisar las 4 casillas vecinas de una posicion, caso especial con bordes y esquinas 
}
