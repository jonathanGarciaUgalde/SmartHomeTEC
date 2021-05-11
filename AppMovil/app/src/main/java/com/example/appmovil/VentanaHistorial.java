package com.example.appmovil;

import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import com.example.appmovil.ui.HistorialAdaper;

public class VentanaHistorial extends AppCompatActivity {

    private HistorialAdaper mAdapter;
    private RecyclerView mRecyclerView;

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


    }

}
