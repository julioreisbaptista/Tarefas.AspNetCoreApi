DROP database tarefas;

CREATE DATABASE tarefas;

USE tarefas;

DROP TABLE IF EXISTS `tarefas`;

CREATE TABLE `tarefas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` text,
  `data` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `status` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ;