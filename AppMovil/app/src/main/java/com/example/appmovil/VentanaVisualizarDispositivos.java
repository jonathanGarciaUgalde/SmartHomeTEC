package com.example.appmovil;

import android.os.Bundle;
import android.util.Log;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Dispositivo;
import com.example.appmovil.ui.DispositivoAdapter;
import com.example.appmovil.utilidades.Utilidades;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VentanaVisualizarDispositivos extends AppCompatActivity {

    private DispositivoAdapter mAdapter;
    private RecyclerView mRecyclerView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_visualizar_dispositivos);

        // Configuracion del RecyclerView

        mRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_dispositivos);

        // Esta línea mejora el rendimiento, si sabemos que el contenido
        // no va a afectar al tamaño del RecyclerView
        mRecyclerView.setHasFixedSize(true); // Se le dice que la altura de todos los elementos es la misma

        // Nuestro RecyclerView usará un linear layout manager
        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        mRecyclerView.setLayoutManager(layoutManager); // (Asocia RecyclerView con un Layout Manager)

        // Asociamos un adapter (ver más adelante cómo definirlo)
        mAdapter = new DispositivoAdapter(); // define como se va a renderizar la informacion que se tiene
        mRecyclerView.setAdapter(mAdapter);
        // Fin RecyclerVew

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

        Call<ArrayList<Dispositivo>> call = ApiAdapter.getApiService().obtenerDispositivos(gsonObject);
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {

                if (response.isSuccessful()){
                    Log.v("mytag", "GET DATA USER");
                    ArrayList<Dispositivo> datos_dispositivos = response.body();

                    if (datos_dispositivos.size() > 0){
                        ArrayList<Dispositivo> listaDispositivos = new ArrayList<Dispositivo>();

                        for(int i = 0; i < datos_dispositivos.size(); i++){
                            Log.v("mytag",  datos_dispositivos.get(i).getTipo());
                            Dispositivo a1 = new Dispositivo(datos_dispositivos.get(i).getNumeroSerie(),
                                    datos_dispositivos.get(i).getEstadoActivo(),
                                    datos_dispositivos.get(i).getTipo(),
                                    datos_dispositivos.get(i).getNombreAposento());
                            listaDispositivos.add(a1);
                        }

                        mAdapter.setDataSet(listaDispositivos);
                    }else{
                        Toast.makeText(getApplicationContext(), "¡El usuario no posee dispositivos!", Toast.LENGTH_SHORT).show();
                    }

                }else{
                    Toast.makeText(getApplicationContext(), "¡Error al cargar los dispositivos del usuario!", Toast.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<ArrayList<Dispositivo>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
            }
        });


    }


}
