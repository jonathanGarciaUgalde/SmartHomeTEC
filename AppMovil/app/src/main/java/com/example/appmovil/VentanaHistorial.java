package com.example.appmovil;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Dispositivo;
import com.example.appmovil.modelos.DispositivoEncendidoApagado;
import com.example.appmovil.modelos.Historial;
import com.example.appmovil.ui.HistorialAdaper;
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

public class VentanaHistorial extends AppCompatActivity {

    private HistorialAdaper mAdapter;
    private RecyclerView mRecyclerView;
    private Spinner spinner;
    private ArrayList<Dispositivo> datos_dispositivos;
    private Integer posicionDispositivoSeleccionado;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_historial);

        // Configuracion del RecyclerView

        mRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_historial_dispositivos);

        // Esta línea mejora el rendimiento, si sabemos que el contenido
        // no va a afectar al tamaño del RecyclerView
        mRecyclerView.setHasFixedSize(true); // Se le dice que la altura de todos los elementos es la misma

        // Nuestro RecyclerView usará un linear layout manager
        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        mRecyclerView.setLayoutManager(layoutManager); // (Asocia RecyclerView con un Layout Manager)

        // Asociamos un adapter (ver más adelante cómo definirlo)
        mAdapter = new HistorialAdaper(); // define como se va a renderizar la informacion que se tiene
        mRecyclerView.setAdapter(mAdapter);
        // Fin RecyclerVew

        spinner = (Spinner) findViewById(R.id.spinner_id);

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

        Call<ArrayList<Dispositivo>> call =  ApiAdapter.getApiService().obtenerDispositivos(gsonObject);
        call.enqueue(new Callback<ArrayList<Dispositivo>>() {
            @Override
            public void onResponse(Call<ArrayList<Dispositivo>> call, Response<ArrayList<Dispositivo>> response) {
                Log.v("mytag", "GET DATA USER");
                datos_dispositivos = response.body();

                ArrayList<String> comboDispositivos = new ArrayList<String>();

                for(int i = 0; i < datos_dispositivos.size(); i++){
                    comboDispositivos.add(datos_dispositivos.get(i).getTipo() +
                            " (" + datos_dispositivos.get(i).getNumeroSerie() + ")" );
                }

                ArrayAdapter<CharSequence> adapter = new ArrayAdapter(getApplicationContext(),
                        android.R.layout.simple_spinner_item, comboDispositivos);

                spinner.setAdapter(adapter);
                spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                                                      @Override
                                                      public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                                                          posicionDispositivoSeleccionado = position;
                                                          //dispositivoSeleccionado = parent.getItemAtPosition(position).toString();
                                                      }

                                                      @Override
                                                      public void onNothingSelected(AdapterView<?> parent) {

                                                      }
                                                  }
                );
            }

            @Override
            public void onFailure(Call<ArrayList<Dispositivo>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
            }
        });

    }

    public void btn_revisar_historial(View view) {

        // Enviar datos (numeroSerie)

        JsonObject gsonObject = new JsonObject();
        try {
            JSONObject jsonObj_ = new JSONObject();

            jsonObj_.put("NumeroSerie", datos_dispositivos.get(posicionDispositivoSeleccionado).getNumeroSerie());

            JsonParser jsonParser = new JsonParser();
            gsonObject = (JsonObject) jsonParser.parse(jsonObj_.toString());

            //print parameter
            Log.v("MY gson.JSON:  ", "AS PARAMETER  " + gsonObject);

        } catch (JSONException e) {
            e.printStackTrace();
        }

        Call<ArrayList<Historial>> call =  ApiAdapter.getApiService().obtenerHistorial(gsonObject);
        call.enqueue(new Callback<ArrayList<Historial>>() {
            @Override
            public void onResponse(Call<ArrayList<Historial>> call, Response<ArrayList<Historial>> response) {

                if (response.isSuccessful()){
                    ArrayList<Historial> datos_historial_dispositivos = response.body();

                    if (datos_historial_dispositivos.size() > 0){
                        ArrayList<Historial> listaHistorialDispositivos = new ArrayList<>();

                        for(int i = 0; i < datos_historial_dispositivos.size(); i++){
                            Historial a1 = new Historial(datos_historial_dispositivos.get(i).getFechaActivacion(),
                                    datos_historial_dispositivos.get(i).getFechaDesactivacion(),
                                    datos_historial_dispositivos.get(i).getHoraActivacion(),
                                    datos_historial_dispositivos.get(i).getHoraDesactivacion(),
                                    datos_historial_dispositivos.get(i).getNumeroSerie());
                            listaHistorialDispositivos.add(a1);
                        }

                        mAdapter.setDataSet(datos_historial_dispositivos);

                    }else{
                        Toast.makeText(getApplicationContext(), "¡No hay historial para este dispositivo!", Toast.LENGTH_SHORT).show();
                    }

                }else{
                    Toast.makeText(getApplicationContext(), "¡No hay historial para este dispositivo!", Toast.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<ArrayList<Historial>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });

    }
}
