package com.example.appmovil.entidades;

/*
*  Este paquete va a almacenar las tablas de la BD SQLite como objetos POJO
* POJO (acrónimo de Plain Old Java Object) -> Un objeto POJO es una instancia de una clase que no
* extiende ni implementa nada en especial.
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
