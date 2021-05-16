package com.example.appmovil.io;

import com.example.appmovil.modelos.Aposentos;
import com.example.appmovil.modelos.Dispositivo;
import com.example.appmovil.modelos.Historial;
import com.example.appmovil.modelos.Usuario;
import com.google.gson.JsonObject;

import java.util.ArrayList;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Path;

// Mediante esta interface cada método abstracto va a representar una ruta específica de la API.
public interface ApiService {

    //http://localhost:3000/api/User/Login/123/123
    @POST("api/User/Login/{username}/{password}")
    Call<Boolean> verificacionLogin(
            @Path("username") String username,
            @Path("password") String password
    );

    //http://localhost:3000/api/User/Credenciales
    @POST("api/User/Credenciales")
    Call<Usuario> obtenerCredenciales(@Body JsonObject body);

    //http://localhost:3000/api/User/Aposento
    @POST("api/User/Aposento")
    Call<ResponseBody> registrarAposento(@Body JsonObject body);

    //http://localhost:3000/api/User/GetDispositivos
    @POST("api/User/GetDispositivos")
    Call<ArrayList<Dispositivo>> obtenerDispositivos(@Body JsonObject body);

    //http://localhost:3000/api/User/GetEstadoDispositivos
    @POST("api/User/GetEstadoDispositivos")
    Call<ArrayList<Dispositivo>> obtenerDispositivosConEstado(@Body JsonObject body);

    //http://localhost:3000/api/User/PasarDispositivo
    @POST("api/User/PasarDispositivo")
    Call<ResponseBody> pasarDispositivo(@Body JsonObject body);

    //http://localhost:3000/api/User/ActivarDispositivo
    @POST("api/User/ActivarDispositivo")
    Call<ResponseBody> activarDispositivo(@Body JsonObject body);

    //http://localhost:3000/api/User/DesactivarDispositivo
    @POST("api/User/DesactivarDispositivo")
    Call<ResponseBody> desactivarDispositivo(@Body JsonObject body);

    //http://localhost:3000/api/User/GetHistorial
    @POST("api/User/GetHistorial")
    Call<ArrayList<Historial>> obtenerHistorial(@Body JsonObject body);

    //http://localhost:3000/api/User/GetAposentos
    @POST("api/User/GetAposentos")
    Call<ArrayList<Aposentos>> obtenerAposentos(@Body JsonObject body);

}
