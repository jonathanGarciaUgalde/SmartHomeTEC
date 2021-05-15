
import {RegionInterface} from  "src/app/interfaces/cliente/region";
import {Ubicacion} from  "src/app/interfaces/cliente/ubicacion";


export interface Usuario {

	correo?: string,
	password?: string,
	nombre?: string,
	apellidos?: string,
	region?: RegionInterface,
	direccion?: Ubicacion[]
}
