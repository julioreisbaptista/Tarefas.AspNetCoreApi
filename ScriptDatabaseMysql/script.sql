DROP database tarefasteste;

CREATE DATABASE tarefasteste;

USE tarefasteste;

DROP TABLE IF EXISTS `tarefas`;

CREATE TABLE `tarefas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` text,
  `data` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `status` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ;