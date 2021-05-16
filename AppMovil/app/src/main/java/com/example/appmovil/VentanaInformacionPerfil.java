package com.example.appmovil;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Usuario;
import com.example.appmovil.utilidades.Utilidades;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VentanaInformacionPerfil extends AppCompatActivity {

    private TextView nombre, apellidos, continente, pais, correo;
    private ListView direcciones;
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
        direcciones = (ListView) findViewById(R.id.list_direcciones);

        // Recuperar datos del cliente
        // Metodo GET

        JsonObject gsonObject = new JsonObject();
        try {
            JSONObject jsonObj_ = new JSONObject();
            jsonObj_.put("Correo", Utilidades.correoUsuario);


            JsonParser jsonParser = new JsonParser();
            gsonObject = (JsonObject) jsonParser.parse(jsonObj_.toString());

            //print parameter
            Log.v("MY gson.JSON:  ", "AS PARAMETER  " + gsonObject);

        } catch (JSONException e) {
            e.printStackTrace();
        }

        Call<Usuario> call =  ApiAdapter.getApiService().obtenerCredenciales(gsonObject);
        //Call<Usuario> call =  ApiAdapter.getApiService().getDataUser();
        call.enqueue(new Callback<Usuario>() {
            @Override
            public void onResponse(Call<Usuario> call, Response<Usuario> response) {

                if (response.isSuccessful()){
                    Log.v("mytag", "GET DATA USER");
                    Usuario datos_usuario = response.body();

                    nombre.setText(datos_usuario.getNombre());
                    apellidos.setText(datos_usuario.getApellidos());
                    continente.setText(datos_usuario.getRegion().getContinente());
                    pais.setText(datos_usuario.getRegion().getPais());
                    correo.setText(datos_usuario.getCorreo());

                    ArrayList<String> listaUbicaciones = new ArrayList();
                    for(int i = 0; i < datos_usuario.getDirecciones().size(); i++){
                        listaUbicaciones.add((i+1) + ": "+ datos_usuario.getDirecciones().get(i).getUbicacion());
                    }

                    ArrayAdapter adaptadorDirecciones = new ArrayAdapter(getApplicationContext(), R.layout.color_list_view, listaUbicaciones);
                    direcciones.setAdapter(adaptadorDirecciones);
                }else{
                    Toast.makeText(getApplicationContext(), "¡Error al cargar los datos del usuario!", Toast.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<Usuario> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });

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
