package com.example.appmovil.io;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

// El ApiAdapter se encarga de instanciar un objeto Retrofit (aplicando el patrón de diseño Singleton)
// y este objeto hará posible las peticiones. Además, se define la ruta base de la API
public class ApiAdapter {

    private static ApiService API_SERVICE;

    public static ApiService getApiService() {

        // Creamos un interceptor y le indicamos el log level a usar
        HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
        logging.setLevel(HttpLoggingInterceptor.Level.BODY);

        // Asociamos el interceptor a las peticiones
        OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
        httpClient.addInterceptor(logging);

        //String baseUrl = "https://apiprueba-b5f94-default-rtdb.firebaseio.com/";
        //String baseUrl = "http://192.168.1.110:3000/"; // Funciona para Physical Device
        //String baseUrl = "http://10.0.3.2:3000/"; // Funciona para GenyMotion
        String baseUrl = "http://10.0.2.2:3000/"; // Funciona para Emulador Android Studio

        if (API_SERVICE == null) {
            Retrofit retrofit = new Retrofit.Builder()
                    .baseUrl(baseUrl)
                    .addConverterFactory(GsonConverterFactory.create())
                    .client(httpClient.build()) // <-- usamos el log level
                    .build();
            API_SERVICE = retrofit.create(ApiService.class);
        }

        return API_SERVICE;
    }

}