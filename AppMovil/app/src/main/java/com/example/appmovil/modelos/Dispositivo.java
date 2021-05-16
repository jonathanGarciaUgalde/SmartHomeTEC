package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Dispositivo {

    @SerializedName("numeroSerie")
    @Expose
    private Integer numeroSerie;

    @SerializedName("estadoActivo")
    @Expose
    private Boolean estadoActivo;

    @SerializedName("tipo")
    @Expose
    private String tipo;

    @SerializedName("nombreAposento")
    @Expose
    private String nombreAposento;

    public Dispositivo(Integer numeroSerie, Boolean estadoActivo, String tipo, String nombreAposento) {
        this.numeroSerie = numeroSerie;
        this.estadoActivo = estadoActivo;
        this.tipo = tipo;
        this.nombreAposento = nombreAposento;
    }

    public Integer getNumeroSerie() {
        return numeroSerie;
    }

    public void setNumeroSerie(Integer numeroSerie) {
        this.numeroSerie = numeroSerie;
    }

    public Boolean getEstadoActivo() {
        return estadoActivo;
    }

    public void setEstadoActivo(Boolean estadoActivo) {
        this.estadoActivo = estadoActivo;
    }

    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo;
    }

    public String getNombreAposento() {
        return nombreAposento;
    }

    public void setNombreAposento(String nombreAposento) {
        this.nombreAposento = nombreAposento;
    }

}
