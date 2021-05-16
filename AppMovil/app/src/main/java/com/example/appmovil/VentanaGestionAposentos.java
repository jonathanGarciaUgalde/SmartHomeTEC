package com.example.appmovil;

import android.app.ProgressDialog;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.appmovil.utilidades.Utilidades;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import retrofit2.Response;

public class VentanaGestionAposentos extends AppCompatActivity {

    private EditText nombre_aposento;
    private ArrayList<HashMap<String, String>> todosAposentos;
    private ConexionSQLite conn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_gestion_aposentos);

        nombre_aposento = (EditText) findViewById(R.id.edit_nombre_aposento);

        conn = new ConexionSQLite(this, "dbusuarios", null, 1);

    }

    public void btn_registrar_aposento(View view) {

        // Si se activa sincronizar datos -> Enviar datos a SQLite

        SQLiteDatabase db = conn.getWritableDatabase();

        // INSERT INTO aposento(nombre) values('david');

        String insert ="INSERT INTO " +Utilidades.TABLA_APOSENTO
                +" ( "
                +Utilidades.CAMPO_CORREO
                +","
                +Utilidades.CAMPO_NOMBRE
                +")"
                + " VALUES ("
                + "'" +  Utilidades.correoUsuario + "'"
                + ", '" +  nombre_aposento.getText().toString() + "'"
                + ")";

        db.execSQL(insert);

        db.close();

        Toast.makeText(this, "Aposento agregado localmente", Toast.LENGTH_SHORT).show();

        // Sino -> Enviar datos al API

        /*JsonObject gsonObject = new JsonObject();
        try {
            JSONObject jsonObj_ = new JSONObject();
            jsonObj_.put("Correo","d@gmail.com");
            jsonObj_.put("Nombre","sala");

            JsonParser jsonParser = new JsonParser();
            gsonObject = (JsonObject) jsonParser.parse(jsonObj_.toString());

            //print parameter
            Log.e("MY gson.JSON:  ", "AS PARAMETER  " + gsonObject);

        } catch (JSONException e) {
            e.printStackTrace();
        }

        Call<ResponseBody> call =  ApiAdapter.getApiService().registrarAposento(gsonObject);
        //Call<Usuario> call =  ApiAdapter.getApiService().getDataUser();
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {

                Log.v("mytag", "GET DATA USER");
                ResponseBody datos_usuario = response.body();

                //Toast.makeText(VentanaInformacionPerfil.this, datos_usuario.getNombre().toString(), Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onFailure(Call<ResponseBody> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });

        Toast.makeText(this, "Aposento agregado en PostgreSQL", Toast.LENGTH_SHORT).show(); */

    }

    public void btn_sincronizar(View view) {

        // Mapea inicialemente los datos a Json
        ConexionSQLite conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        todosAposentos = conn.getAllAposentos();

        JSONArray json = new JSONArray();
        for(int i = 0 ; i < todosAposentos.size() ; i++) {
            JSONObject jsonObjectProducto = new JSONObject();
            try {
                jsonObjectProducto.put("Correo", todosAposentos.get(i).get("Correo"));
                jsonObjectProducto.put("Nombre", todosAposentos.get(i).get("Nombre"));
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
                        Request.Method.POST, getResources().getString(R.string.URL_REGISTRAR_APOSENTO), objeto,
                        new com.android.volley.Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {
                                Log.d("mytag", "Registro realizado en la BD centralizada");
                            }
                        }, new com.android.volley.Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        VolleyLog.d("log", "Error: " + error.getMessage());
                        //Toast.makeText(getApplicationContext(), "El aposento es duplicado.", Toast.LENGTH_SHORT).show();
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

        Toast.makeText(getApplicationContext(),"¡Sincronizacion de datos completa!", Toast.LENGTH_LONG).show();

        // Borrar todos los registros de la tabla Aposento
        conn = new ConexionSQLite(this, "dbusuarios", null, 1);
        SQLiteDatabase db = conn.getWritableDatabase();
        db.execSQL("delete from "+ Utilidades.TABLA_APOSENTO);
        db.close();

    }


}
