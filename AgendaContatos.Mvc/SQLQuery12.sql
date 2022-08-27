﻿CREATE TABLE USUARIO(
       IDUSUARIO     UNIQUEIDENTIFIER   NOT NULL,
       NOME          NVARCHAR(150)      NOT NULL,
       EMAIL         NVARCHAR(100)      NOT NULL UNIQUE,
       SENHA         NVARCHAR(50)       NOT NULL,
       DATACADASTRO  DATETIME           NOT NULL,
       PRIMARY KEY(IDUSUARIO))
      

CREATE TABLE CONTATO(
       IDCONTATO      UNIQUEIDENTIFIER   NOT NULL,
       NOME            NVARCHAR(150)      NOT NULL,
       TELEFONE        NVARCHAR(20)       NOT NULL,
       EMAIL           NVARCHAR(100)      NOT NULL,
       DATANASCIMENTO  DATE               NOT NULL,
       IDUSUARIO       UNIQUEIDENTIFIER   NOT NULL,
       PRIMARY KEY(IDCONTATO),
       FOREIGN KEY(IDUSUARIO) REFERENCES USUARIO(IDUSUARIO))
       