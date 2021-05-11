package com.example.appmovil;

import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.appmovil.utilidades.Utilidades;

public class VentanaGestionDispositivos extends AppCompatActivity {

    private EditText descripcion, tipo, marca, numero_serie, consumo, aposento;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_gestion_dispositivos);

        descripcion = (EditText) findViewById(R.id.edit_descripcion);
        tipo = (EditText) findViewById(R.id.edit_tipo);
        marca = (EditText) findViewById(R.id.edit_marca);
        numero_serie = (EditText) findViewById(R.id.edit_numero_serie);
        consumo = (EditText) findViewById(R.id.edit_consumo);
        aposento = (EditText) findViewById(R.id.edit_aposento);

    }

    public void btn_agregar_dispositivo(View view) {

        Toast.makeText(getApplicationContext(), Utilidades.correoUsuario, Toast.LENGTH_LONG).show();

        // Si se activa sincronizar datos -> Enviar datos a SQLite
        ConexionSQLite conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        SQLiteDatabase db = conn.getWritableDatabase();

        String insert ="INSERT INTO " +Utilidades.TABLA_DISPOSITIVO
                +" ( "
                +Utilidades.DESCRIPCION
                +","
                +Utilidades.TIPO
                +","
                +Utilidades.MARCA
                +","
                +Utilidades.NUMERO_SERIE
                +","
                +Utilidades.CONSUMO
                +","
                +Utilidades.APOSENTO
                +")"
                + " VALUES ("
                + "'" +  descripcion.getText().toString() + "'"
                + ", '" +  tipo.getText().toString() + "'"
                + ", '" +  marca.getText().toString() + "'"
                + ", '" +  numero_serie.getText().toString() + "'"
                + ", '" +  consumo.getText().toString() + "'"
                + ", '" +  aposento.getText().toString() + "'"
                + ")";

        db.execSQL(insert);

        db.close();

        // Sino -> Enviar datos al API

    }

    public void btn_visualizar_dispositivo(View view) {

        Intent visualizarDispositivo = new Intent(this, VentanaVisualizarDispositivos.class);
        startActivity(visualizarDispositivo);
    }

    public void btn_pasar_dispositivo_de_cuenta(View view) {

        Intent pasarDispositivoDeCuenta = new Intent(this, VentanaPasarDispositivoDeCuenta.class);
        startActivity(pasarDispositivoDeCuenta);
    }

    public void btn_sincronizar(View view) {

        
    }
}
