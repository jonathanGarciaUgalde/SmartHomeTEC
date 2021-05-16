package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import java.util.ArrayList;

public class Historial {

    @SerializedName("fechaActivacion")
    @Expose
    private String fechaActivacion;

    @SerializedName("fechaDesactivacion")
    @Expose
    private String fechaDesactivacion;

    @SerializedName("horaActivacion")
    @Expose
    private String horaActivacion;

    @SerializedName("horaDesactivacion")
    @Expose
    private String horaDesactivacion;

    @SerializedName("numeroSerie")
    @Expose
    private Integer numeroSerie;

    public Historial(String fechaActivacion, String fechaDesactivacion, String horaActivacion, String horaDesactivacion, Integer numeroSerie) {
        this.fechaActivacion = fechaActivacion;
        this.fechaDesactivacion = fechaDesactivacion;
        this.horaActivacion = horaActivacion;
        this.horaDesactivacion = horaDesactivacion;
        this.numeroSerie = numeroSerie;
    }

    public String getFechaActivacion() {
        return fechaActivacion;
    }

    public void setFechaActivacion(String fechaActivacion) {
        this.fechaActivacion = fechaActivacion;
    }

    public String getFechaDesactivacion() {
        return fechaDesactivacion;
    }

    public void setFechaDesactivacion(String fechaDesactivacion) {
        this.fechaDesactivacion = fechaDesactivacion;
    }

    public String getHoraActivacion() {
        return horaActivacion;
    }

    public void setHoraActivacion(String horaActivacion) {
        this.horaActivacion = horaActivacion;
    }

    public String getHoraDesactivacion() {
        return horaDesactivacion;
    }

    public void setHoraDesactivacion(String horaDesactivacion) {
        this.horaDesactivacion = horaDesactivacion;
    }

    public Integer getNumeroSerie() {
        return numeroSerie;
    }

    public void setNumeroSerie(Integer numeroSerie) {
        this.numeroSerie = numeroSerie;
    }

}

