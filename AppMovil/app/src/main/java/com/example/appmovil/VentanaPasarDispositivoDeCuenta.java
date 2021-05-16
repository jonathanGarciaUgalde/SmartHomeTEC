package com.example.appmovil;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.appmovil.io.ApiAdapter;
import com.example.appmovil.modelos.Dispositivo;
import com.example.appmovil.modelos.Usuario;
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

public class VentanaPasarDispositivoDeCuenta extends AppCompatActivity {

    private EditText cuentaNueva;
    private Spinner spinner;
    private Integer posicionDispositivoSeleccionado;
    private ArrayList<Dispositivo> datos_dispositivos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.ventana_pasar_dispositivo_de_cuenta);

        cuentaNueva = (EditText) findViewById(R.id.txt_cuentaNueva);
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

    public void btn_pasar_dispositivo(View view) {

        // Enviar datos (Cuenta_Actual, Cuenta_Nueva, Aposento, Dispositivo)

        JsonObject gsonObject = new JsonObject();
        try {
            JSONObject jsonObj_ = new JSONObject();
            jsonObj_.put("PosedorActual", Utilidades.correoUsuario);
            jsonObj_.put("FuturoPosedor", cuentaNueva.getText());
            jsonObj_.put("NumeroSerie", datos_dispositivos.get(posicionDispositivoSeleccionado).getNumeroSerie());

            JsonParser jsonParser = new JsonParser();
            gsonObject = (JsonObject) jsonParser.parse(jsonObj_.toString());

            //print parameter
            Log.v("MY gson.JSON:  ", "AS PARAMETER  " + gsonObject);

        } catch (JSONException e) {
            e.printStackTrace();
        }

        Call<ResponseBody> call =  ApiAdapter.getApiService().pasarDispositivo(gsonObject);
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                if (response.isSuccessful()){
                    Toast.makeText(getApplicationContext(), "El usuario " + Utilidades.correoUsuario
                                    + " ha pasado exitosamente el dispositivo " + datos_dispositivos.get(posicionDispositivoSeleccionado).getNumeroSerie()
                                    + " a " + cuentaNueva.getText()
                            , Toast.LENGTH_LONG).show();
                }else{
                    Toast.makeText(getApplicationContext(), "El usuario " + Utilidades.correoUsuario
                                    + " no puede pasar un dispositivo a un usuario no registrado: " + cuentaNueva.getText()
                            , Toast.LENGTH_LONG).show();
                }

            }

            @Override
            public void onFailure(Call<ResponseBody> call, Throwable t) {
                t.printStackTrace();
                Log.e("Error", "Error loading from API");
            }
        });

    }
}
