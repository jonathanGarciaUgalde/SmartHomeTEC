package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class Usuario {

    @SerializedName("nombre")
    @Expose
    private String nombre;

    @SerializedName("apellidos")
    @Expose
    private String apellidos;

    @SerializedName("region")
    @Expose
    private Region region;

    @SerializedName("correo")
    @Expose
    private String correo;

    @SerializedName("direccion")
    @Expose
    private ArrayList<Direccion> direcciones;

    public Usuario(String nombre, String apellidos, Region region, String correo, ArrayList<Direccion> direcciones) {
        this.nombre = nombre;
        this.apellidos = apellidos;
        this.region = region;
        this.correo = correo;
        this.direcciones = direcciones;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

   public String getApellidos() {
        return apellidos;
    }

    public void setApellidos(String apellidos) {
        this.apellidos = apellidos;
    }

    public Region getRegion() {
        return region;
    }

    public void setRegion(Region region) {
        this.region = region;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public ArrayList<Direccion> getDirecciones() {
        return direcciones;
    }

    public void setDirecciones(ArrayList<Direccion> direcciones) {
        this.direcciones = direcciones;
    }

}
