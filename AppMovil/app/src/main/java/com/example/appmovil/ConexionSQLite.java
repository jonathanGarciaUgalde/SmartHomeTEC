package com.example.appmovil;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import com.example.appmovil.utilidades.Utilidades;
import java.util.ArrayList;
import java.util.HashMap;

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
        db.execSQL(Utilidades.CREAR_TABLA_APOSENTO);
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

    // Eliminar registro de una tabla
    public void borrarRegistros(SQLiteDatabase db) {
        db.execSQL("DELETE FROM Utilidades.TABLA_APOSENTO");
        db.close();
    }

    // Recuperar datos (aposentos) de SQLite
    public ArrayList<HashMap<String, String>> getAllAposentos() {

        SQLiteDatabase database = this.getWritableDatabase(); // Abrir la BD
        ArrayList<HashMap<String, String>> todosAposentos;
        todosAposentos = new ArrayList<HashMap<String, String>>();
        String selectQuery = "SELECT * FROM aposento";
        Cursor cursor = database.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                HashMap<String, String> map = new HashMap<String, String>();
                map.put("Correo", cursor.getString(0));
                map.put("Nombre", cursor.getString(1));

                todosAposentos.add(map);
            } while (cursor.moveToNext());
        }
        database.close();
        return todosAposentos;
    }

    // Recuperar datos (dispositivos) de SQLite
    public ArrayList<HashMap<String, String>> getAllDispositivos() {

        SQLiteDatabase database = this.getWritableDatabase(); // Abrir la BD
        ArrayList<HashMap<String, String>> todosDispositivos;
        todosDispositivos = new ArrayList<HashMap<String, String>>();
        String selectQuery = "SELECT * FROM dispositivo";
        Cursor cursor = database.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                HashMap<String, String> map = new HashMap<String, String>();
                map.put("Descripcion", cursor.getString(0));
                map.put("Tipo", cursor.getString(1));
                map.put("Marca", cursor.getString(2));
                map.put("NumeroSerie", cursor.getString(3));
                map.put("Consumo", cursor.getString(4));
                map.put("Aposento", cursor.getString(5));

                todosDispositivos.add(map);
            } while (cursor.moveToNext());
        }
        database.close();
        return todosDispositivos;
    }
}
