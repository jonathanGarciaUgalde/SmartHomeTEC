package com.example.appmovil;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Dispositivo;
import com.example.appmovil.modelos.DispositivoEncendidoApagado;
import com.example.appmovil.ui.EncenderApagarDispositivoAdapter;
import com.example.appmovil.utilidades.Utilidades;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import okhttp3.ResponseBody;
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

        Call<ArrayList<Dispositivo>> call = ApiAdapter.getApiService().obtenerDispositivosConEstado(gsonObject);
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {

                if (response.isSuccessful()){
                    Log.v("mytag", "GET DATA USER");
                    ArrayList<Dispositivo> datos_dispositivos = response.body();

                    if (datos_dispositivos.size() > 0) {
                        ArrayList<DispositivoEncendidoApagado> listaDispositivos = new ArrayList<>();

                        for (int i = 0; i < datos_dispositivos.size(); i++) {
                            DispositivoEncendidoApagado a1 = new DispositivoEncendidoApagado(datos_dispositivos.get(i).getNombreAposento(),
                                    datos_dispositivos.get(i).getTipo(),
                                    datos_dispositivos.get(i).getNumeroSerie(),
                                    datos_dispositivos.get(i).getEstadoActivo(),
                                    datos_dispositivos.get(i).getEstadoActivo());
                            listaDispositivos.add(a1);
                        }

                        mAdapter.setDataSet(listaDispositivos);
                    }else{
                        Toast.makeText(getApplicationContext(), "¡El usuario no posee dispositivos!", Toast.LENGTH_SHORT).show();
                    }

                }else{
                    Toast.makeText(getApplicationContext(), "¡Error al cargar el estado de los dispositivos del usuario!", Toast.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<ArrayList<Dispositivo>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
            }
        });

    }

    public void btn_encender_apagar_dispositivo(View view) {

        // Recibir cambios
        // Enviar Cuenta, Aposento, Dispositivo, Estado
        mAdapter.listaEstados();
        if (mAdapter.listaEstados().size() > 0) {
            for (int i = 0; i < mAdapter.listaEstados().size(); i++) {

                // Revisar el cambio de estado, si paso de falso a true enviar a ActivarDispositivo, caso contrario enviar a DesactivarDispositivo
                boolean estadoNuevo = (boolean) mAdapter.listaEstados().get(i).get(1);

                JsonObject gsonObject = new JsonObject();
                try {
                    JSONObject jsonObj_ = new JSONObject();
                    jsonObj_.put("NumeroSerie", mAdapter.listaEstados().get(i).get(0));
                    jsonObj_.put("EstadoActivo", estadoNuevo);

                    JsonParser jsonParser = new JsonParser();
                    gsonObject = (JsonObject) jsonParser.parse(jsonObj_.toString());

                    //print parameter
                    Log.v("MY gson.JSON:  ", "AS PARAMETER  " + gsonObject);

                } catch (JSONException e) {
                    e.printStackTrace();
                }

                if (estadoNuevo) {

                    Call<ResponseBody> call = ApiAdapter.getApiService().activarDispositivo(gsonObject);
                    call.enqueue(new Callback<ResponseBody>() {
                        @Override
                        public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                            Log.v("mytag", "GET DATA USER");
                        }
                        @Override
                        public void onFailure(Call<ResponseBody> call, Throwable t) {
                            t.printStackTrace();
                            Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
                        }
                    });

                } else {

                    Call<ResponseBody> call = ApiAdapter.getApiService().desactivarDispositivo(gsonObject);
                    call.enqueue(new Callback<ResponseBody>() {
                        @Override
                        public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                            Log.v("mytag", "GET DATA USER");
                        }
                        @Override
                        public void onFailure(Call<ResponseBody> call, Throwable t) {
                            t.printStackTrace();
                            Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
                        }
                    });

                }
            }

        }

        // Recuperar datos del cliente
        // Metodo GET

        /*JsonObject gsonObject = new JsonObject();
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

        Call<ArrayList<Dispositivo>> call = ApiAdapter.getApiService().obtenerDispositivosConEstado(gsonObject);
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {

                if (response.isSuccessful()){
                    Log.v("mytag", "GET DATA USER");
                    ArrayList<Dispositivo> datos_dispositivos = response.body();

                    if (datos_dispositivos.size() > 0) {
                        ArrayList<DispositivoEncendidoApagado> listaDispositivos = new ArrayList<>();

                        for (int i = 0; i < datos_dispositivos.size(); i++) {
                            DispositivoEncendidoApagado a1 = new DispositivoEncendidoApagado(datos_dispositivos.get(i).getNombreAposento(),
                                    datos_dispositivos.get(i).getTipo(),
                                    datos_dispositivos.get(i).getNumeroSerie(),
                                    datos_dispositivos.get(i).getEstadoActivo(),
                                    datos_dispositivos.get(i).getEstadoActivo());
                            listaDispositivos.add(a1);
                        }

                        mAdapter.setDataSet(listaDispositivos);
                    }else{
                        Toast.makeText(getApplicationContext(), "¡El usuario no posee dispositivos!", Toast.LENGTH_SHORT).show();
                    }

                }else{
                    Toast.makeText(getApplicationContext(), "¡Error al cargar el estado de los dispositivos del usuario!", Toast.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<ArrayList<Dispositivo>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
            }
        });*/

    }

    public void btn_historial_dispositivo(View view) {

        Intent historial = new Intent(this, VentanaHistorial.class);
        startActivity(historial);
    }
}
