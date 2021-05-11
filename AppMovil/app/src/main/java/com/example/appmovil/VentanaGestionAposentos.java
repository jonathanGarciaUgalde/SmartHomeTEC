package com.example.appmovil;

import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import com.example.appmovil.utilidades.Utilidades;

public class VentanaGestionAposentos extends AppCompatActivity {

    private EditText nombre_aposento;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_gestion_aposentos);

        nombre_aposento = (EditText) findViewById(R.id.edit_nombre_aposento);
    }

    public void btn_registrar_aposento(View view) {

        // Si se activa sincronizar datos -> Enviar datos a SQLite
        ConexionSQLite conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        SQLiteDatabase db = conn.getWritableDatabase();

        // INSERT INTO aposento(nombre) values('david');

        /*String insert ="INSERT INTO "+Utilidades.TABLA_APOSENTO
                +" ( "
                +Utilidades.CAMPO_ID+","+Utilidades.CAMPO_NOMBRE+")" +
                " VALUES (" + "123" + ", '" +  nombre_aposento.getText().toString() + "')";*/

        String insert = "INSERT INTO " +Utilidades.TABLA_APOSENTO
                +"("
                +Utilidades.CAMPO_NOMBRE +")"
                + " VALUES ('" +  nombre_aposento.getText().toString() + "')";

        db.execSQL(insert);

        db.close();

        // Sino -> Enviar datos al API

    }

}
