package com.example.appmovil;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

public class VentanaInformacionPerfil extends AppCompatActivity {

    private TextView nombre, apellidos, continente, pais, correo;
    private Bundle datosMainActivity;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_informacion_perfil);

        nombre = (TextView) findViewById(R.id.txt_nombre);
        apellidos = (TextView) findViewById(R.id.txt_apellidos);
        continente = (TextView) findViewById(R.id.txt_continente);
        pais = (TextView) findViewById(R.id.txt_pais);
        correo = (TextView) findViewById(R.id.txt_correo);

        // Recuperar datos del cliente
        // Metodo GET
        /*Call<Usuario> call = ApiAdapter.getApiService().getDataUser();
        call.enqueue(new Callback<Usuario>() {
            @Override
            public void onResponse(Call<Usuario> call, Response<Usuario> response) {

                Log.v("mytag", "GET DATA USER");
                Usuario datos_usuario = response.body();

                nombre.setText(datos_usuario.getNombre());
                apellidos.setText(datos_usuario.getApellidos());
                continente.setText(datos_usuario.getRegion().get(0).getContinente());
                pais.setText(datos_usuario.getRegion().get(0).getPais());
                correo.setText(datos_usuario.getCorreo());

            }

            @Override
            public void onFailure(Call<Usuario> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });*/

    }

    public void btn_gestion_smarthome(View view) {

        Intent ventanaAposentos = new Intent(this, VentanaGestionAposentos.class);
        startActivity(ventanaAposentos);

    }

    public void btn_gestion_dispositivos(View view) {

        Intent ventanaDispositivos = new Intent(this, VentanaGestionDispositivos.class);
        startActivity(ventanaDispositivos);

    }

    public void btn_control_dispositivos(View view) {

        Intent ventanaControlDispositivos = new Intent(this, VentanaControlDispositivos.class);
        startActivity(ventanaControlDispositivos);

    }
}
