package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Aposentos {

    @SerializedName("nombre")
    @Expose
    private String nombre;

    @SerializedName("correo")
    @Expose
    private String correo;

    public Aposentos(String nombre, String correo) {
        this.nombre = nombre;
        this.correo = correo;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

}
