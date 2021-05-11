package com.example.appmovil;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.appmovil.utilidades.Utilidades;

/*
* Extiende de SQLiteOpenHelper quien se encarga de realizar la conexion con la BD
*
* */
public class ConexionSQLite extends SQLiteOpenHelper {

    /**
     *
     * @param context Contexto de la aplicacion
     * @param name Nombre de la BD
     * @param factory
     * @param version Version de la BD
     */
    public ConexionSQLite(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }

    // Generar las tablas(scripts) inicialmente de las entidades
    @Override
    public void onCreate(SQLiteDatabase db) {

        db.execSQL(Utilidades.CREAR_TABLA_USUARIO);
        db.execSQL(Utilidades.CREAR_TABLA_DISPOSITIVO);

    }

    // Verifica si existe antes una version antigua de la BD
    // Actualizar datos, como nombres de tablas, etc.
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS Utilidades.TABLA_APOSENTO");
        db.execSQL("DROP TABLE IF EXISTS Utilidades.TABLA_DISPOSITIVO");

        onCreate(db);
    }
}
