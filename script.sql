create database aula_xp;

CREATE TABLE pessoas (
    id int NOT NULL AUTO_INCREMENT,
    nome varchar(100) NOT NULL,
    telefone varchar(20),
    PRIMARY KEY ( id )
);


alter table pessoas add column tipo varchar (100);

alter table pessoas add column documento varchar (40);