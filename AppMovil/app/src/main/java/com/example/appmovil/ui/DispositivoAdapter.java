package com.example.appmovil.ui;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.recyclerview.widget.RecyclerView;
import com.example.appmovil.R;
import com.example.appmovil.modelos.Dispositivo;
import java.util.ArrayList;

// Convierte nuestra data en elementos visibles por parte del usuario
public class DispositivoAdapter extends RecyclerView.Adapter<DispositivoAdapter.ViewHolder> {

    private ArrayList<Dispositivo> mDataSet;

    // Obtener referencias de los componentes visuales para cada elemento
    // Es decir, referencias de los EditText, TextViews, Buttons
    public class ViewHolder extends RecyclerView.ViewHolder {
        // en este ejemplo cada elemento consta solo de un título

        TextView aposento, dispositivo, serie;

        public ViewHolder(View itemView) {
            super(itemView);

            aposento = (TextView) itemView.findViewById(R.id.textViewAposento);
            dispositivo = (TextView) itemView.findViewById(R.id.textViewDipositivos);
            serie = (TextView) itemView.findViewById(R.id.textViewSerie);

        }
    }

    // Este es nuestro constructor (puede variar según lo que queremos mostrar)
    public DispositivoAdapter() {
        mDataSet = new ArrayList<>();
    }

    // Metodo para agregar los datos al recyclerview
    public void setDataSet(ArrayList<Dispositivo> dataSet) {
        mDataSet = dataSet;
        notifyDataSetChanged();
    }

    // El layout manager invoca este método
    // para renderizar cada elemento del RecyclerView

    // Enlaza el adaptador con el archivo "menu_view"
    @Override
    public DispositivoAdapter.ViewHolder onCreateViewHolder(ViewGroup parent,
                                                            int viewType) {
        // Creamos una nueva vista
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.dispositivo_view, null, false);

        // Aquí podemos definir tamaños, márgenes, paddings, etc
        ViewHolder vh = new ViewHolder(view);
        return vh;
    }

    // Este método asigna valores para cada elemento de la lista
    // Establece la comunicacion entre el adaptador y la clase "ViewHolder"
    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        // - obtenemos un elemento del dataset según su posición
        // - reemplazamos el contenido usando tales datos

        holder.aposento.setText(mDataSet.get(position).getNombreAposento());
        holder.dispositivo.setText(mDataSet.get(position).getTipo());
        holder.serie.setText(mDataSet.get(position).getNumeroSerie().toString());

    }

    // Método que define la cantidad de elementos del RecyclerView
    // Puede ser más complejo (por ejem, si implementamos filtros o búsquedas)
    @Override
    public int getItemCount() {
        return mDataSet.size();
    }

}
