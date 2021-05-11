package com.example.appmovil;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.model.Dispositivo;
import com.example.appmovil.model.DispositivoEncendidoApagado;
import com.example.appmovil.ui.DispositivoAdapter;
import com.example.appmovil.ui.EncenderApagarDispositivoAdapter;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VentanaControlDispositivos extends AppCompatActivity {

    private EncenderApagarDispositivoAdapter mAdapter;
    private RecyclerView mRecyclerView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_control_dispositivos);

        // Configuracion del RecyclerView

        mRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_encender_apagar_dispositivos);

        // Esta línea mejora el rendimiento, si sabemos que el contenido
        // no va a afectar al tamaño del RecyclerView
        mRecyclerView.setHasFixedSize(true); // Se le dice que la altura de todos los elementos es la misma

        // Nuestro RecyclerView usará un linear layout manager
        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        mRecyclerView.setLayoutManager(layoutManager); // (Asocia RecyclerView con un Layout Manager)

        // Asociamos un adapter (ver más adelante cómo definirlo)
        mAdapter = new EncenderApagarDispositivoAdapter(); // define como se va a renderizar la informacion que se tiene
        mRecyclerView.setAdapter(mAdapter);
        // Fin RecyclerVew

        // Metodo GET -> Obtener dispositivos
        Call<ArrayList<Dispositivo>> call =  ApiAdapter.getApiService().getDataDevices();
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {
                if (response.isSuccessful()){

                    ArrayList<Dispositivo> respuestaServerDispositivos = response.body();

                    /*ArrayList<String> listaDispositivos = new ArrayList<String>();
                    for (int i = 0; i < respuestaServerDispositivos.size(); i++){
                        for (int j = 0; j < respuestaServerDispositivos.get(i).getDispositivos().size(); j++){
                            listaDispositivos.add(respuestaServerDispositivos.get(i).getAposento() + ": " +
                                    respuestaServerDispositivos.get(i).getDispositivos().get(j));
                        }
                    }

                    for (int i = 0; i < listaDispositivos.size(); i++){
                        Log.v("mytag", listaDispositivos.get(i));
                    }*/

                    ArrayList<DispositivoEncendidoApagado> listaDispositivos = new ArrayList<DispositivoEncendidoApagado>();
                    DispositivoEncendidoApagado a1 = new DispositivoEncendidoApagado("Comedor", "Wifi", "On", "On");
                    DispositivoEncendidoApagado a2 = new DispositivoEncendidoApagado("Comedor", "Bombillo", "Off", "Off");
                    DispositivoEncendidoApagado a3 = new DispositivoEncendidoApagado("Sala", "Tv", "On", "On");
                    DispositivoEncendidoApagado a4 = new DispositivoEncendidoApagado("Sala", "Bombillo", "On", "On");
                    listaDispositivos.add(a1);
                    listaDispositivos.add(a2);
                    listaDispositivos.add(a3);
                    listaDispositivos.add(a4);
                    mAdapter.setDataSet(listaDispositivos);

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

    public void btn_encender_apagar_dispositivo(View view) {

        // Recibir cambios
        // Enviar Cuenta, Aposento, Dispositivo, Estado
        mAdapter.listaOrdenes();
    }

    public void btn_historial_dispositivo(View view) {

        Intent historial = new Intent(this, VentanaHistorial.class);
        startActivity(historial);
    }
}
