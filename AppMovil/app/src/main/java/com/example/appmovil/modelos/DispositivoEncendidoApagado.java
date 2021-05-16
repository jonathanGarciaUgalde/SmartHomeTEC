package com.example.appmovil.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class DispositivoEncendidoApagado {

    @SerializedName("Aposento")
    @Expose
    private String aposento;

    @SerializedName("Dispositivo")
    @Expose
    private String dispositivo;

    @SerializedName("EstadoActual")
    @Expose
    private String estadoActual;

    @SerializedName("EstadoNuevo")
    @Expose
    private String estadoNuevo;

    public DispositivoEncendidoApagado(String aposento, String dispositivo, String estadoActual, String estadoNuevo) {
        this.aposento = aposento;
        this.dispositivo = dispositivo;
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

    public String getEstadoActual() {
        return estadoActual;
    }

    public void setEstadoActual(String estadoActual) {
        this.estadoActual = estadoActual;
    }

    public String getEstadoNuevo() {
        return estadoNuevo;
    }

    public void setEstadoNuevo(String estadoNuevo) {
        this.estadoNuevo = estadoNuevo;
    }
}
