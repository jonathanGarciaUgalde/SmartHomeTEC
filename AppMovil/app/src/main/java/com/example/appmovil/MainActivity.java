package com.example.appmovil;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.utilidades.Utilidades;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private EditText correo, password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        correo = (EditText) findViewById(R.id.txt_correo);
        password = (EditText) findViewById(R.id.txt_password);

    }

    public void btn_iniciar_sesion(View view) {

        // Consulta si existe el usuario en la base de datos

        Call<Boolean> call =  ApiAdapter.getApiService().getLoginRespueta("123", "123");
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                if (response.isSuccessful()){
                    Boolean respuestaServer = response.body(); // Cedula(Id) del cliente

                    // Permite o no el acceso al sistema
                    if(respuestaServer){

                        // Ir a otra ventana
                        Intent informacionPerfil = new Intent(getApplicationContext(), VentanaInformacionPerfil.class);
                        startActivity(informacionPerfil);

                    }else{
                        Toast.makeText(MainActivity.this, "Usuario Incorrecto", Toast.LENGTH_SHORT).show();
                    }

                    Log.v("mytag", "Respuesta exitosa");
                }
            }

            @Override
            public void onFailure(Call<Boolean> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });

        // Asignar a la variable correoUsuario el correo (Id) del usuario
        Utilidades.correoUsuario = correo.getText().toString();

        //Intent informacionPerfil = new Intent(this, VentanaInformacionPerfil.class);
        //startActivity(informacionPerfil);

    }
}