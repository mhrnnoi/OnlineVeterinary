CREATE TABLE IF NOT EXISTS public."Users" (
    "Id" uuid NOT NULL,
    "FirstName" text COLLATE pg_catalog."default" NOT NULL,
    "LastName" text COLLATE pg_catalog."default" NOT NULL,
    "Email" text COLLATE pg_catalog."default" NOT NULL,
    "Password" text COLLATE pg_catalog."default" NOT NULL,
    "Role" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Reservations" (
    "Id" uuid NOT NULL,
    "DoctorId" uuid NOT NULL,
    "PetId" uuid NOT NULL,
    "CareGiverId" uuid NOT NULL,
    "DateOfReservation" timestamp with time zone NOT NULL,
    "DrLastName" text COLLATE pg_catalog."default" NOT NULL,
    "PetName" text COLLATE pg_catalog."default" NOT NULL,
    "CareGiverLastName" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Reservations" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Pets" (
    "Id" uuid NOT NULL,
    "CareGiverId" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "DateOfBirth" timestamp with time zone NOT NULL,
    "PetType" text COLLATE pg_catalog."default" NOT NULL,
    "CareGiverLastName" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Pets" PRIMARY KEY ("Id")
);
