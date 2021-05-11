package com.example.appmovil;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void btn_iniciar_sesion(View view) {

        Intent informacionPerfil = new Intent(this, VentanaInformacionPerfil.class);
        startActivity(informacionPerfil);
    }
}