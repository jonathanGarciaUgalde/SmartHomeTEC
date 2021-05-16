package com.example.appmovil.ui;

import android.os.Build;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.RequiresApi;
import androidx.recyclerview.widget.RecyclerView;

import com.example.appmovil.R;
import com.example.appmovil.modelos.Historial;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Duration;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.Period;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Date;

// Convierte nuestra data en elementos visibles por parte del usuario
public class HistorialAdaper extends RecyclerView.Adapter<HistorialAdaper.ViewHolder> {

    private ArrayList<Historial> mDataSet;

    // Obtener referencias de los componentes visuales para cada elemento
    // Es decir, referencias de los EditText, TextViews, Buttons
    public class ViewHolder extends RecyclerView.ViewHolder {
        // en este ejemplo cada elemento consta solo de un título

        TextView fechaActivacion, fechaDesactivacion, horaActivacion, horaDesactivacion, duracion;

        public ViewHolder(View itemView) {
            super(itemView);

            fechaActivacion = (TextView) itemView.findViewById(R.id.textViewFechaActivacion);
            fechaDesactivacion = (TextView) itemView.findViewById(R.id.textViewFechaDesactivacion);
            horaActivacion = (TextView) itemView.findViewById(R.id.textViewHoraActivacion);
            horaDesactivacion = (TextView) itemView.findViewById(R.id.textViewHoraDesactivacion);
            duracion = (TextView) itemView.findViewById(R.id.textViewDuracion);

        }
    }

    // Este es nuestro constructor (puede variar según lo que queremos mostrar)
    public HistorialAdaper() {
        mDataSet = new ArrayList<>();
    }

    // Metodo para agregar los datos al recyclerview
    public void setDataSet(ArrayList<Historial> dataSet) {
        mDataSet = dataSet;
        notifyDataSetChanged();
    }

    // El layout manager invoca este método
    // para renderizar cada elemento del RecyclerView

    // Enlaza el adaptador con el archivo "menu_view"
    @Override
    public HistorialAdaper.ViewHolder onCreateViewHolder(ViewGroup parent,
                                                         int viewType) {
        // Creamos una nueva vista
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.historial_view, null, false);

        // Aquí podemos definir tamaños, márgenes, paddings, etc
        ViewHolder vh = new ViewHolder(view);
        return vh;
    }

    // Este método asigna valores para cada elemento de la lista
    // Establece la comunicacion entre el adaptador y la clase "ViewHolder"
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        // - obtenemos un elemento del dataset según su posición
        // - reemplazamos el contenido usando tales datos

        holder.fechaActivacion.setText(mDataSet.get(position).getFechaActivacion());
        holder.fechaDesactivacion.setText(mDataSet.get(position).getFechaDesactivacion());
        holder.horaActivacion.setText(mDataSet.get(position).getHoraActivacion());
        holder.horaDesactivacion.setText(mDataSet.get(position).getHoraDesactivacion());

        // Calcular la duracion

        // Diferencia en dias
        LocalDate d1 = LocalDate.parse(mDataSet.get(position).getFechaActivacion(), DateTimeFormatter.ISO_LOCAL_DATE);
        LocalDate d2 = LocalDate.parse(mDataSet.get(position).getFechaDesactivacion(), DateTimeFormatter.ISO_LOCAL_DATE);
        Duration diff = Duration.between(d1.atStartOfDay(), d2.atStartOfDay());
        long diffDays = diff.toDays();

        // Diferencia en horas
        String hora1 = mDataSet.get(position).getHoraActivacion();
        String hora2 = mDataSet.get(position).getHoraDesactivacion();

        String[] h1 = hora1.split(":");
        String[] h2 = hora2.split(":");
        int resto = 0;

        int segundo = Integer.parseInt(h2[2]) - Integer.parseInt(h1[2]);
        if (segundo < 0){
            resto = -1;
            segundo = 60 + segundo;
        }

        int minuto = (Integer.parseInt(h2[1]) - Integer.parseInt(h1[1])) - resto;
        resto = 0;
        if (minuto < 0){
            minuto = 60 - minuto;
            resto = -1;
        }

        int hora = (Integer.parseInt(h2[0]) - Integer.parseInt(h1[0])) - resto;

        holder.duracion.setText(hora + " horas " + minuto + " minutos " + segundo + " segundos");

    }

    // Método que define la cantidad de elementos del RecyclerView
    // Puede ser más complejo (por ejem, si implementamos filtros o búsquedas)
    @Override
    public int getItemCount() {
        return mDataSet.size();
    }

}
