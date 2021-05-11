package com.example.appmovil.ui;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.R;
import com.example.appmovil.model.Dispositivo;
import com.example.appmovil.model.DispositivoEncendidoApagado;

import java.util.ArrayList;
import java.util.List;

// Convierte nuestra data en elementos visibles por parte del usuario
public class EncenderApagarDispositivoAdapter extends RecyclerView.Adapter<EncenderApagarDispositivoAdapter.ViewHolder> {

    private ArrayList<DispositivoEncendidoApagado> mDataSet;
    private List<String> mlists;
    private ArrayList<List> selectedValues;

    // Obtener referencias de los componentes visuales para cada elemento
    // Es decir, referencias de los EditText, TextViews, Buttons
    public class ViewHolder extends RecyclerView.ViewHolder {
        // en este ejemplo cada elemento consta solo de un título

        TextView dispositivo;
        Switch aSwitch;

        public ViewHolder(View itemView) {
            super(itemView);

            dispositivo = (TextView) itemView.findViewById(R.id.textViewDispositivo);
            aSwitch = (Switch) itemView.findViewById(R.id.switch_id);

            selectedValues = new ArrayList<List>();

        }
    }

    // Este es nuestro constructor (puede variar según lo que queremos mostrar)
    public EncenderApagarDispositivoAdapter() {
        mDataSet = new ArrayList<>();
    }

    // Metodo para agregar los datos al recyclerview
    public void setDataSet(ArrayList<DispositivoEncendidoApagado> dataSet) {
        mDataSet = dataSet;
        notifyDataSetChanged();
    }

    // El layout manager invoca este método
    // para renderizar cada elemento del RecyclerView

    // Enlaza el adaptador con el archivo "menu_view"
    @Override
    public EncenderApagarDispositivoAdapter.ViewHolder onCreateViewHolder(ViewGroup parent,
                                                                          int viewType) {
        // Creamos una nueva vista
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.encender_apagar_dispositivo_view, null, false);

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
        List<String> mlists = new ArrayList<>();

        holder.dispositivo.setText(mDataSet.get(position).getAposento() + ": " + mDataSet.get(position).getDispositivo());
        if (mDataSet.get(position).getEstadoActual().equals("On")){
            holder.aSwitch.setChecked(true);
        }else{
            holder.aSwitch.setChecked(false);
        }

        holder.aSwitch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(holder.aSwitch.isChecked()){
                    mDataSet.get(position).setEstadoNuevo("On");
                }else{
                    mDataSet.get(position).setEstadoNuevo("Off");
                }
            }
        });

    }

    // Método que define la cantidad de elementos del RecyclerView
    // Puede ser más complejo (por ejem, si implementamos filtros o búsquedas)
    @Override
    public int getItemCount() {
        return mDataSet.size();
    }

    public ArrayList<List> listaOrdenes(){

        for (int i = 0; i < mDataSet.size(); i++){

            if(!mDataSet.get(i).getEstadoActual().equals(mDataSet.get(i).getEstadoNuevo())) {
                //
                Log.v("mytag", mDataSet.get(i).getAposento() + ": " + mDataSet.get(i).getDispositivo());
            }

        }

        return selectedValues;
    }

}
