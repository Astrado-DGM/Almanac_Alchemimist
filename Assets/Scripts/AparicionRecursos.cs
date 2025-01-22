using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Recursos", menuName = "Scriptable Objects/AparicionRecursos")]
public class AparicionRecursos : ScriptableObject
{
    public string nombreRecurso;//nombre recurso
    public int zonaAparicion;//zona en la que el recurso puede aparecer
    public int id;//id para futuro uso de inventario
    public Sprite imagen;//imagen del recurso 
    public Texture imagenInventario; //imagen que aparece en el inventario
    public Vector2Int coordenadas;//coordendas en las que aparezen
    public int rareza;//rareza determina que tan problable sera que aparezca 
    public string color;//el color determina las propiedades del ingrediente ingame
}
