package com.example.appmovil.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.List;

public class Usuario {

    @SerializedName("nombre")
    @Expose
    private String nombre;

    @SerializedName("apellidos")
    @Expose
    private String apellidos;

    @SerializedName("region")
    @Expose
    private Region region = null;

    /*@SerializedName("Correo")
    @Expose
    private String correo;

    @SerializedName("Password")
    @Expose
    private String password;

    @SerializedName("Direcciones")
    @Expose
    private ArrayList<String> direcciones;*/

    public Usuario(String nombre, String apellidos, Region region, String correo, String password, ArrayList<String> direcciones) {
        this.nombre = nombre;
        this.apellidos = apellidos;
        /*this.region = region;
        this.correo = correo;
        this.password = password;
        this.direcciones = direcciones;*/
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

     /*public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public ArrayList<String> getDirecciones() {
        return direcciones;
    }

    public void setDirecciones(ArrayList<String> direcciones) {
        this.direcciones = direcciones;
    }*/
}
