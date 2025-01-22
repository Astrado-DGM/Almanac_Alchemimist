using UnityEngine;
using System.Collections.Generic;

public class Recurso{
    public string nombreRecurso;//nombre recurso
    public int id;//id 
    public Texture imagenInventario;//imagen del recurso 
    public int rareza;//brillo del titulo del recurso?
    public string color;//el color determina las propiedades del ingrediente ingame

     public Recurso(AparicionRecursos entrada){
        this.nombreRecurso = entrada.nombreRecurso;
        this.id = entrada.id;
        this.imagenInventario = entrada.imagenInventario;
        this.rareza = entrada.rareza;
        this.color = entrada.color;
     }
}
public class Inventario : MonoBehaviour
{
    //DM tiene que manejar el guardado de informacion, pues es de los unicos objetos que se mantiene en la escena
    // O un nuevo objeto que se mantenga en la escenna, si quieres orden esto es mejor
    public AparicionRecursos detallesObjeto;
    public int tamanoInventario = 5;
    private int tamanoActual=0;//rescatar
    protected List<Recurso> slots = new List<Recurso>();//rescatar
    public Texture fondo;
    public Texture espacioVacio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tamanoActual = slots.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool agregarInventario(AparicionRecursos entrada){
        if(tamanoActual == tamanoInventario){return false;}
        Recurso item = new Recurso(entrada);
        slots.Add(item);
        slots.Sort((a, b) => b.id.CompareTo(a.id));

        tamanoActual++;
        lista();
        return true;
    }
    public void lista(){
        for(int i=0;i<tamanoActual;i++){
            Debug.Log(slots[i].nombreRecurso+"--"+slots[i].id);
        }
        Debug.Log("*****");
    }
    /*
    void OnGUI(){
        GUI.Box(new Rect(30,10,200,200),fondo);
        for(int i=0;i<tamanoInventario;i++){    
            if(i<=tamanoActual){
                Texture actualMaterial = slots[i].imagenInventario;
                if(GUI.Button(new Rect(7, 7, 210+(1+i*7),210+(1+i*7)), actualMaterial)){Debug.Log("apretado: "+slots[i].nombreRecurso);};
            }else{
                GUI.Box(new Rect(7,7,21+(1+i*7),210+(1+i*7)),espacioVacio);
            }
        }
    }*/
    
    
}
