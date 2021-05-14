CREATE TABLE "Admin"(
    "correo" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
	PRIMARY KEY("correo")
);

CREATE TABLE "Distribuidor"(
    "cedulaJuridica" INTEGER NOT NULL,
    "nombre" VARCHAR(255) NOT NULL,
    "continente" VARCHAR(255) NOT NULL,
    "pais" VARCHAR(255) NOT NULL,
	PRIMARY KEY("cedulaJuridica")
);

CREATE TABLE "DispositivoStock"(
    "numeroSerie" INTEGER NOT NULL,
    "marca" VARCHAR(255) NOT NULL DEFAULT 'N/A',
    "consumoElectrico" DOUBLE PRECISION NOT NULL DEFAULT 0.0,  
	"cedulaJuridica" INTEGER NOT NULL, --FK
	"tipo" VARCHAR(20) NOT NULL DEFAULT 'N/A',
	"tiempoGarantia" INT NOT NULL,
	"descripcion" VARCHAR(50) NOT NULL DEFAULT 'N/A',
	"enVenta" BOOlEAN NOT NULL DEFAULT TRUE,
	PRIMARY KEY("numeroSerie")
);

CREATE TABLE "Pedido"(
    "numero" SERIAL NOT NULL, 
    "correoComprador" VARCHAR(255) NOT NULL, -- FK de PK de Usuario
    "fecha" VARCHAR(20) NOT NULL,
    "numeroSerie" INTEGER NOT NULL, -- FK de PK de DispositivoStock
	PRIMARY KEY("numero")
);


CREATE TABLE "direccionEntrega"(
    "correo" VARCHAR(255) NOT NULL,
    "ubicacion" VARCHAR(255) NOT NULL,
	PRIMARY KEY ("correo", "ubicacion")
);

CREATE TABLE "Usuario"(
    "correo" VARCHAR(255) NOT NULL, --Será FK en Pedido
    "password" VARCHAR(255) NOT NULL,
    "nombre" VARCHAR(255) NOT NULL,
    "apellidos" VARCHAR(255) NOT NULL,
    "continente" VARCHAR(255) NOT NULL,
    "pais" VARCHAR(255) NOT NULL,
	PRIMARY KEY("correo")
);

--Aposento Si o si requiere de su tabla pues proviene de un atributo multivaluado
CREATE TABLE "Aposento"(
    "correo" VARCHAR(255) NOT NULL,
    "nombre" VARCHAR(255) NOT NULL,
    PRIMARY KEY("correo", "nombre")
);


CREATE TABLE "Dispositivo"(
    "numeroSerie" INTEGER NOT NULL,
    "consumoElectrico" DOUBLE PRECISION NOT NULL DEFAULT 0.0,
    "marca" VARCHAR(255) NOT NULL DEFAULT 'N/A',
    "estadoActivo" BOOLEAN NOT NULL DEFAULT FALSE,    
    "nombreAposento" VARCHAR(255) DEFAULT 'N/A',
	"correoPosedor" VARCHAR(50) NOT NULL,
	"tipo" VARCHAR(20) NOT NULL DEFAULT 'N/A',
	"tiempoGarantia" INT NOT NULL,
	"descripcion" VARCHAR(30) NOT NULL DEFAULT 'N/A',
	PRIMARY KEY("numeroSerie")
);

--Relación asociada a todos los usuarios que han sido dueños de un mismo disp.
CREATE TABLE "Registro"(
    "numeroSerie" INTEGER NOT NULL,
    "correo" VARCHAR(255) NOT NULL,
    PRIMARY KEY("numeroSerie", "correo")
);

--Asociado al historial de uso de cada dispositivo
CREATE TABLE "Historial"(
    "fechaActivacion" VARCHAR(20) NOT NULL,
    "fechaDesactivacion" VARCHAR(20) DEFAULT NULL,
    "horaActivacion" VARCHAR(20) NOT NULL,
    "horaDesactivacion" VARCHAR(20) DEFAULT NULL,
    "numeroSerie" INTEGER NOT NULL,
	PRIMARY KEY("numeroSerie", "horaActivacion")
);


ALTER TABLE
    "DispositivoStock" ADD CONSTRAINT "cedulaJuridicaDistribuidor" FOREIGN KEY("cedulaJuridica") REFERENCES "Distribuidor"("cedulaJuridica");
ALTER TABLE
    "Pedido" ADD CONSTRAINT "pedido_correo_foreign" FOREIGN KEY("correoComprador") REFERENCES "Usuario"("correo");
ALTER TABLE
    "Pedido" ADD CONSTRAINT "dispositivo_pedido_foreign" FOREIGN KEY("numeroSerie") REFERENCES "DispositivoStock"("numeroSerie");
ALTER TABLE
    "Dispositivo" ADD CONSTRAINT "correoPosedorComoForeignKey" FOREIGN KEY("correoPosedor") REFERENCES "Usuario"("correo");
--ALTER TABLE
  -- "Dispositivo" ADD CONSTRAINT "dispositivo_nombreaposento_foreign" FOREIGN KEY("nombreAposento") REFERENCES "Aposento"("nombre");
  
