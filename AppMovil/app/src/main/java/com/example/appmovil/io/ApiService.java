package com.example.appmovil.io;

import com.example.appmovil.model.Dispositivo;
import com.example.appmovil.model.Usuario;

import java.util.ArrayList;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import retrofit2.http.Query;

// Mediante esta interface cada método abstracto va a representar una ruta específica de la API.
public interface ApiService {

    //http://localhost:3000/api/User/verify/Login/123/123
    @POST("api/User/verify/Login/{username}/{password}/")
    Call<Boolean> getLoginRespueta(
            @Path("username") String username,
            @Path("password") String password
    );


    @GET("usuarios/305.json")
    Call<Usuario> getDataUser();

    @GET("usuarios/305.json")
    Call<ArrayList<Dispositivo>> getDataDevices();

    /*@POST("api/User")
    Call<ResponseBody> postUser(@Body Registro registro);

    @GET("api/Menu/All")
    Call<ArrayList<Menu>> getMenu();

    @POST("api/Orden")
    Call<ResponseBody> postFeedback(@Body Feedback feedback );

    @POST("api/Carrito")
    Call<ResponseBody> postCarrito(@Body Carrito carrito );

    @POST("api/Orden/{id}")
    Call<ResponseBody> postPedido(@Path("id") String id);

    @DELETE("api/User/{id}")
    Call<ResponseBody> deleteUser(@Path("id") String id);

    // Consultar las ordenes activas (asignadas) del usuario
    @GET("api/Orden/orden/{id}")
    Call<ArrayList<EstadoPedido>> getOrdenes(@Path("id") String id);

    @PUT("api/User/{id}")
    Call<ResponseBody> putUser(@Path("id") String id,
                               @Body Registro registro);*/


}
