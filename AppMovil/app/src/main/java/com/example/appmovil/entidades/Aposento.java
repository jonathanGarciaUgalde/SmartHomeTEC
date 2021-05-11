package com.example.appmovil.entidades;

/*
*  Este paquete va a agrupar las tablas de la BD
* POJO (acrónimo de Plain Old Java Object) -> Un objeto POJO es una instancia de una clase que no
* extiende ni implementa nada en especial.
* Con el auge de JSON se utilizan POJO para serializar los objetos en formato json, con bibliotecas como Gson.
 * */
public class Aposento {

    private Integer id;
    private String nombre;

    public Aposento(Integer id, String nombre) {
        this.id = id;
        this.nombre = nombre;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }
}
