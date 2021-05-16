package com.example.appmovil.modelos;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Region {

    @SerializedName("continente")
    @Expose
    private String continente;

    @SerializedName("pais")
    @Expose
    private String pais;

    public Region(String continente, String pais) {
        this.continente = continente;
        this.pais = pais;
    }

    public String getContinente() {
        return continente;
    }

    public void setContinente(String continente) {
        this.continente = continente;
    }

    public String getPais() {
        return pais;
    }

    public void setPais(String pais) {
        this.pais = pais;
    }
}
