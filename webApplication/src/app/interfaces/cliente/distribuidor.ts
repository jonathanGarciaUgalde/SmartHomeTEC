import {RegionInterface} from  "src/app/interfaces/cliente/region";

export interface Distribuidor {
	cedulaJuridica?: number,
	nombre?: string,
	region?:RegionInterface
}
