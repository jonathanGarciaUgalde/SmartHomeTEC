package com.example.appmovil;

import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Aposentos;
import com.example.appmovil.utilidades.Utilidades;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VentanaGestionDispositivos extends AppCompatActivity {

    private EditText descripcion, tipo, marca, numero_serie, consumo;
    private ArrayList<HashMap<String, String>> todosDispositivos;
    private Spinner spinnerAposento;
    private Integer posicionDispositivoSeleccionado;
    private ArrayList<Aposentos> datos_aposentos;
    private ConexionSQLite conn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_gestion_dispositivos);

        descripcion = (EditText) findViewById(R.id.edit_descripcion);
        tipo = (EditText) findViewById(R.id.edit_tipo);
        marca = (EditText) findViewById(R.id.edit_marca);
        numero_serie = (EditText) findViewById(R.id.edit_numero_serie);
        consumo = (EditText) findViewById(R.id.edit_consumo);
        spinnerAposento = (Spinner) findViewById(R.id.spinner_aposento);

        conn = new ConexionSQLite(this, "dbusuarios", null, 1);

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

        Call<ArrayList<Aposentos>> call =  ApiAdapter.getApiService().obtenerAposentos(gsonObject);
        call.enqueue(new Callback<ArrayList<Aposentos>>() {
            @Override
            public void onResponse(Call<ArrayList<Aposentos>> call, Response<ArrayList<Aposentos>> response) {
                Log.v("mytag", "GET DATA USER");

                datos_aposentos = response.body();

                ArrayList<String> comboAposentos = new ArrayList<String>();

                for(int i = 0; i < datos_aposentos.size(); i++){
                    comboAposentos.add(datos_aposentos.get(i).getNombre() );
                }

                ArrayAdapter<CharSequence> adapter = new ArrayAdapter(getApplicationContext(),
                        android.R.layout.simple_spinner_item, comboAposentos);

                spinnerAposento.setAdapter(adapter);
                spinnerAposento.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
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
            public void onFailure(Call<ArrayList<Aposentos>> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API (ObtenerDispositivos) ");
            }
        });

    }

    public void btn_agregar_dispositivo(View view) {

        // Si se activa sincronizar datos -> Enviar datos a SQLite
        ConexionSQLite conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        SQLiteDatabase db = conn.getWritableDatabase();

        String insert ="INSERT INTO " +Utilidades.TABLA_DISPOSITIVO
                +" ( "
                +Utilidades.DESCRIPCION
                +","
                +Utilidades.TIPO
                +","
                +Utilidades.MARCA
                +","
                +Utilidades.NUMERO_SERIE
                +","
                +Utilidades.CONSUMO
                +","
                +Utilidades.APOSENTO
                +")"
                + " VALUES ("
                + "'" +  descripcion.getText().toString() + "'"
                + ", '" +  tipo.getText().toString() + "'"
                + ", '" +  marca.getText().toString() + "'"
                + ", '" +  numero_serie.getText().toString() + "'"
                + ", '" +  consumo.getText().toString() + "'"
                + ", '" +  datos_aposentos.get(posicionDispositivoSeleccionado).getNombre() + "'"
                + ")";

        db.execSQL(insert);

        db.close();

        Toast.makeText(this, "Dispositivo agregado localmente", Toast.LENGTH_SHORT).show();

        // Sino -> Enviar datos al API

    }

    public void btn_sincronizar(View view) {

        // Mapea inicialemente los datos a Json

        todosDispositivos = conn.getAllDispositivos();

        JSONArray json = new JSONArray();
        for(int i = 0 ; i < todosDispositivos.size() ; i++) {
            JSONObject jsonObjectProducto = new JSONObject();
            try {
                jsonObjectProducto.put("NumeroSerie", Integer.parseInt(todosDispositivos.get(i).get("NumeroSerie")));
                jsonObjectProducto.put("Marca", todosDispositivos.get(i).get("Marca"));
                jsonObjectProducto.put("Consumo", Double.parseDouble(todosDispositivos.get(i).get("Consumo")));
                jsonObjectProducto.put("CorreoPosedor", Utilidades.correoUsuario);
                jsonObjectProducto.put("NombreAposento", todosDispositivos.get(i).get("Aposento"));
                jsonObjectProducto.put("Tipo", todosDispositivos.get(i).get("Tipo"));
                jsonObjectProducto.put("Descripcion", todosDispositivos.get(i).get("Descripcion"));
            } catch (JSONException e) {
                e.printStackTrace();
            }
            json.put(jsonObjectProducto);
        }

        registrarServidor(json);

    }

    public void registrarServidor(JSONArray json) {

        for(int i = 0; i < json.length(); i++){

            try {
                JSONObject objeto = json.getJSONObject(i);
                // Make request for JSONObject
                JsonObjectRequest jsonObjReq = new JsonObjectRequest(
                        Request.Method.POST, getResources().getString(R.string.URL_REGISTRAR_DISPOSITIVO), objeto,
                        new com.android.volley.Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {
                                Log.d("log", response.toString() + " i am queen");
                            }
                        }, new com.android.volley.Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        VolleyLog.d("log", "Error: " + error.getMessage());
                    }
                }) {

                    @Override
                    public Map<String, String> getHeaders() throws AuthFailureError {
                        HashMap<String, String> headers = new HashMap<String, String>();
                        headers.put("Content-Type", "application/json; charset=utf-8");
                        return headers;
                    }

                };

                // Adding request to request queue
                Volley.newRequestQueue(this).add(jsonObjReq);

            } catch (JSONException e) {
                e.printStackTrace();
            }

        }

        Toast.makeText(this,"¡Sincronizacion de datos completa!", Toast.LENGTH_LONG).show();

        // Borrar todos los registros de la tabla Aposento
        conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        SQLiteDatabase db = conn.getWritableDatabase();
        db.execSQL("delete from "+ Utilidades.TABLA_DISPOSITIVO);
        db.close();

    }

    public void btn_visualizar_dispositivo(View view) {

        Intent visualizarDispositivo = new Intent(this, VentanaVisualizarDispositivos.class);
        startActivity(visualizarDispositivo);
    }

    public void btn_pasar_dispositivo_de_cuenta(View view) {

        Intent pasarDispositivoDeCuenta = new Intent(this, VentanaPasarDispositivoDeCuenta.class);
        startActivity(pasarDispositivoDeCuenta);
    }


}
