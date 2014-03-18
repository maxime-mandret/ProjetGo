CREATE DATABASE  IF NOT EXISTS `goban` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `goban`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: goban
-- ------------------------------------------------------
-- Server version	5.6.12-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `coup`
--

DROP TABLE IF EXISTS `coup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `coup` (
  `idCoup` int(10) NOT NULL AUTO_INCREMENT,
  `idPartie` int(10) NOT NULL,
  `heureCoup` datetime NOT NULL,
  `idJoueur` int(10) unsigned NOT NULL,
  `x` int(10) DEFAULT NULL,
  `y` int(10) DEFAULT NULL,
  PRIMARY KEY (`idCoup`),
  UNIQUE KEY `PK_INDEX` (`idCoup`),
  KEY `idPartie_idx` (`idPartie`),
  CONSTRAINT `FK_idPartie` FOREIGN KEY (`idPartie`) REFERENCES `partie` (`idPartie`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `joueurs`
--

DROP TABLE IF EXISTS `joueur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `joueur` (
  `idJoueur` int(10) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`idJoueur`),
  UNIQUE KEY `idnew_table_UNIQUE` (`idJoueur`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `partie`
--

DROP TABLE IF EXISTS `partie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partie` (
  `idPartie` int(10) NOT NULL AUTO_INCREMENT,
  `idJoueurNoir` int(10) NOT NULL,
  `idJoueurBlanc` int(10) DEFAULT NULL,
  `etatPartie` enum('playing','over','pending') DEFAULT 'playing',
  `heureDebut` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `heureFin` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`idPartie`),
  UNIQUE KEY `idPartie_UNIQUE` (`idPartie`),
  KEY `idJoueurNoir_idx` (`idJoueurNoir`),
  KEY `idJoueurBlanc_idx` (`idJoueurBlanc`),
  CONSTRAINT `idJoueurBlanc` FOREIGN KEY (`idJoueurBlanc`) REFERENCES `joueurs` (`idJoueur`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `idJoueurNoir` FOREIGN KEY (`idJoueurNoir`) REFERENCES `joueurs` (`idJoueur`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-03-16 22:41:30
