# TestSchoolRepo

create table Alumno(
	IdAlumno int not null primary key IDENTITY(1,1),
    NombreAlumno varchar(250),
    ApellidoAlumno varchar(250),
    GeneroAlumno BIT,
    FechaNac date 
);

create table Profesor(
	IdProfesor int not null primary key IDENTITY(1,1),
    NombreProfesor varchar(250),
    ApellidoProfesor varchar(250),
    GeneroProfesor BIT
);

create table Grado(
	IdGrado int not null primary key IDENTITY(1,1),
    NombreGrado varchar(250),
    IdProfesor int,
	foreign key(IdProfesor) references Profesor(IdProfesor)
);

create table AlumnoGrado(
	IdAlumnoGrado int not null primary key IDENTITY(1,1),
    IdAlumno int,
    IdGrado int,
    Seccion varchar(250),
    foreign key(IdAlumno) references Alumno(IdAlumno),
	foreign key(IdGrado) references Grado(IdGrado)
);
