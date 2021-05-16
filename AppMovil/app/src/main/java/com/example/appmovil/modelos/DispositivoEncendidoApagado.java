package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class DispositivoEncendidoApagado {

    @SerializedName("Aposento")
    @Expose
    private String aposento;

    @SerializedName("Dispositivo")
    @Expose
    private String dispositivo;

    @SerializedName("Serie")
    @Expose
    private Integer serie;

    @SerializedName("EstadoActual")
    @Expose
    private Boolean estadoActual;

    @SerializedName("EstadoNuevo")
    @Expose
    private Boolean estadoNuevo;

    public DispositivoEncendidoApagado(String aposento, String dispositivo, Integer serie, Boolean estadoActual, Boolean estadoNuevo) {
        this.aposento = aposento;
        this.dispositivo = dispositivo;
        this.serie = serie;
        this.estadoActual = estadoActual;
        this.estadoNuevo = estadoNuevo;
    }

    public String getAposento() {
        return aposento;
    }

    public void setAposento(String aposento) {
        this.aposento = aposento;
    }

    public String getDispositivo() {
        return dispositivo;
    }

    public void setDispositivo(String dispositivo) {
        this.dispositivo = dispositivo;
    }

    public Integer getSerie() {
        return serie;
    }

    public void setSerie(Integer serie) {
        this.serie = serie;
    }

    public Boolean getEstadoActual() {
        return estadoActual;
    }

    public void setEstadoActual(Boolean estadoActual) {
        this.estadoActual = estadoActual;
    }

    public Boolean getEstadoNuevo() {
        return estadoNuevo;
    }

    public void setEstadoNuevo(Boolean estadoNuevo) {
        this.estadoNuevo = estadoNuevo;
    }
}
