package com.example.appmovil.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class Dispositivo {

    @SerializedName("Aposento")
    @Expose
    private String aposento;

    @SerializedName("Dispositivos")
    @Expose
    private ArrayList<String> dispositivos;

    public Dispositivo(String aposento, ArrayList<String> dispositivos) {
        this.aposento = aposento;
        this.dispositivos = dispositivos;
    }

    public String getAposento() {
        return aposento;
    }

    public void setAposento(String aposento) {
        this.aposento = aposento;
    }

    public ArrayList<String> getDispositivos() {
        return dispositivos;
    }

    public void setDispositivos(ArrayList<String> dispositivos) {
        this.dispositivos = dispositivos;
    }
}
