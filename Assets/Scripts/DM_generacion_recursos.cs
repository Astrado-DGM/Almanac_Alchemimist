using UnityEngine;
using System.Collections.Generic;
using System.Linq ;
public class DM_generacion_recursos : MonoBehaviour
{
    private DM_generacion_cuevas GeneracionCuevas;
    private int[,] mapa;
    public int cantidadRecursos;
    public int ZonaActual=0;
    
    
    void Start(){
        GeneracionCuevas = GetComponent<DM_generacion_cuevas>();//definimos el script de generacion cuevas para compartir el mapa
        if(GeneracionCuevas != null){
            mapa = GeneracionCuevas.getMapa();//actualizamos el mapa respecto la cueva actual
            cantidadRecursos = 4;//(mapa.GetLength(0) * mapa.GetLength(1))/49;//cambiar esto para que aumente la cantidad de recursos cada 3-5 niveles
        }else{Debug.Log("error al cargar script genr start()");}
    }
    public void genera(){//llamada para generar todos los objetos en un nuevo mapa
        limpiarObjetos();//eliminamos los objetos del mapa
        if(GeneracionCuevas != null){
            mapa = GeneracionCuevas.getMapa();//cargamos el nuevo mapa
            cantidadRecursos = 4;//(mapa.GetLength(0)*mapa.GetLength(1))/49;//reseteamos la cantidad de recursos maxima
        }
        spawnRecursos();
    }
    private void spawnRecursos(){
        int ancho = mapa.GetLength(0);//tomamos el tamaño del mapa
        int largo = mapa.GetLength(1);
        while(cantidadRecursos!=0){//tratamos de crear todos los objetos contados en el mapa
            int x = Random.Range(0,ancho);//tomamos coordenadas al azar
            int y = Random.Range(0,largo);
            if(mapa[x,y]==2){//si las coordenadas coinciden con suelo en el mapa, seguimos
                Vector3 posicion = new Vector3(x*2,1,y*2);//creamos un vector 3d apartir de las coordenadas para la craecion del objeto
                chanceAparicion(posicion,x,y);
            }
        }
            cantidadRecursos = 4;//(mapa.GetLength(0)*mapa.GetLength(1))/49;//resetamos la cantidad de objetos
        return;
    }

    [Header("Objetos Recursos")]
    public GameObject florAzul;
    public GameObject florBlanca;
    public GameObject florRoja;
    //añadir aqui los siguientes objetos que se creen

    private void chanceAparicion(Vector3 cord,int x,int y){
        int idEscogida = Random.Range(0, 3);
        //if(!poolRecursos.Contains(idEscogida)){return;}
        float chance = Random.Range(0f , 1f);
        GameObject recursoSelecionado=florBlanca;

        switch(idEscogida){//extender lista de casos conforme añadas objetos
            case 0:
                recursoSelecionado = florBlanca;
                break;
            case 1:
                recursoSelecionado = florRoja;
                break;
            case 2:
                recursoSelecionado = florAzul;
                break;
        }

        float probabilidad = .5f / recursoSelecionado.GetComponent<plantillaRecurso>().getRareza();// creamos un valor dado por la rareza del recurso,
        if(probabilidad > chance && ZonaActual == recursoSelecionado.GetComponent<plantillaRecurso>().getZonaDisponible()){//lo comparamos con un valor azar de chance y vemos si el recurso puede aparecer en esta zona
            GameObject recurso = Instantiate(recursoSelecionado,cord, Quaternion.Euler(0, 0, 0));//creamos el objeto   TODO: direcion que mira, siempre tiene que ser hacia el jugaodr
            mapa[x,y]=3;//seteamos el mapa como 3, para futuro uso y que no se repita en objeto en esta casilla
            //GameObject nuevaCasilla = Instantiate(casilla,new Vector3(0 + (x * 2), 0, 0 + (y * 2)),Quaternion.Euler(new Vector3(90, 0, 0)));
            
            cantidadRecursos--;
            
        }
    }
    public int[,] getMapa(){
        return mapa;
    }
    public void limpiarObjetos(){
        GameObject[] objeto = GameObject.FindGameObjectsWithTag("Material");
        for (int i = 0; i < objeto.GetLength(0); i++){
            Destroy(objeto[i]);
        }
        
    }
}

