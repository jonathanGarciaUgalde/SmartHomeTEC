package com.example.appmovil;

import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.appmovil.utilidades.Utilidades;

import java.util.ArrayList;

public class VentanaPasarDispositivoDeCuenta extends AppCompatActivity {

    private EditText cuentaNueva;
    private Spinner spinner;
    private String dispositivoSeleccionado;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_pasar_dispositivo_de_cuenta);

        cuentaNueva = (EditText) findViewById(R.id.txt_cuentaNueva);
        spinner = (Spinner) findViewById(R.id.spinner_id);

        ArrayList<String> comboDispositivos = new ArrayList<String>();
        comboDispositivos.add("Comedor: Tv");
        comboDispositivos.add("Comedor: Wifi");
        comboDispositivos.add("Sala: Bombillo");
        comboDispositivos.add("Sala: Radio");

        ArrayAdapter<CharSequence> adapter = new ArrayAdapter(this,
                android.R.layout.simple_spinner_item, comboDispositivos);

        spinner.setAdapter(adapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                                              @Override
                                              public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                                                  dispositivoSeleccionado = parent.getItemAtPosition(position).toString();
                                              }

                                              @Override
                                              public void onNothingSelected(AdapterView<?> parent) {

                                              }
                                          }
        );

    }

    public void btn_pasar_dispositivo(View view) {

        Toast.makeText(this,
                cuentaNueva.getText() + " " + dispositivoSeleccionado,
                Toast.LENGTH_LONG).show();

        // Enviar datos (Cuenta_Actual, Cuenta_Nueva, Aposento, Dispositivo)

    }
}
