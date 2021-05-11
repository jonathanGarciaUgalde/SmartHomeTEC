package com.example.appmovil;

import android.os.Bundle;
import android.util.Log;
import android.widget.EditText;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.model.Dispositivo;
import com.example.appmovil.ui.DispositivoAdapter;

import java.util.ArrayList;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VentanaVisualizarDispositivos extends AppCompatActivity {

    private DispositivoAdapter mAdapter;
    private RecyclerView mRecyclerView;
    private EditText descripcion, tipo, marca, numero_serie, consumo, aposento;

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

        // Metodo GET -> Obtener dispositivos
        Call<ArrayList<Dispositivo>> call =  ApiAdapter.getApiService().getDataDevices();
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {
                if (response.isSuccessful()){

                    ArrayList<Dispositivo> respuestaServerDispositivos = response.body();

                    mAdapter.setDataSet(respuestaServerDispositivos);

                    Log.v("mytag", "Respuesta exitosa");
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Dispositivo>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });
    }

}
