package com.example.appmovil.utilidades;

/* Clase con los campos y las tablas de la BD
* */
public class Utilidades {

    // Variable global del id (correoUsuario) del usuario
    public static String correoUsuario;

    // Constantes campos tabla aposento
    public static final String TABLA_APOSENTO = "aposento";
    public static final String CAMPO_NOMBRE = "nombre";
    public static final String CREAR_TABLA_USUARIO = "CREATE TABLE " +TABLA_APOSENTO+" ("+CAMPO_NOMBRE+" TEXT)";

    // Constantes campos tabla dispositivo
    public static final String TABLA_DISPOSITIVO = "dispositivo";
    public static final String DESCRIPCION = "descripcion";
    public static final String TIPO = "tipo";
    public static final String MARCA = "marca";
    public static final String NUMERO_SERIE = "numero_serie";
    public static final String CONSUMO = "consumo";
    public static final String APOSENTO = "aposento";
    public static final String CREAR_TABLA_DISPOSITIVO = "CREATE TABLE " +TABLA_DISPOSITIVO+" ("
            + DESCRIPCION +" TEXT,"
            + TIPO +" TEXT,"
            + MARCA +" TEXT,"
            + NUMERO_SERIE +" TEXT,"
            + CONSUMO +" TEXT,"
            + APOSENTO +" TEXT )";

}
