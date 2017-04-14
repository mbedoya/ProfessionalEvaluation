CREATE DATABASE  IF NOT EXISTS `pro_evaluation` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `pro_evaluation`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: pro_evaluation
-- ------------------------------------------------------
-- Server version	5.6.19

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
-- Table structure for table `assesment`
--

DROP TABLE IF EXISTS `assesment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `assesment` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `AssesmentID` varchar(50) NOT NULL,
  `CompanyID` int(11) NOT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateStarted` datetime DEFAULT NULL,
  `DateFinished` datetime DEFAULT NULL,
  `PersonName` varchar(50) DEFAULT NULL,
  `PersonEmail` varchar(100) DEFAULT NULL,
  `EvaluationID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ASSESMENT_ID` (`AssesmentID`),
  KEY `FK_ASSESMENT_COMPANY_idx` (`CompanyID`),
  CONSTRAINT `FK_ASSESMENT_COMPANY` FOREIGN KEY (`CompanyID`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assesment`
--

LOCK TABLES `assesment` WRITE;
/*!40000 ALTER TABLE `assesment` DISABLE KEYS */;
INSERT INTO `assesment` (`ID`, `AssesmentID`, `CompanyID`, `DateCreated`, `DateStarted`, `DateFinished`, `PersonName`, `PersonEmail`, `EvaluationID`) VALUES (1,'aaaBBBccc',1,'2017-04-09 15:38:18',NULL,NULL,'Julio Medina','julio.medina@perceptio.net',1);
/*!40000 ALTER TABLE `assesment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assesment_context`
--

DROP TABLE IF EXISTS `assesment_context`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `assesment_context` (
  `AssesmentID` int(11) NOT NULL,
  `SectionIndex` int(11) NOT NULL,
  `QuestionIndex` tinyint(4) NOT NULL,
  `MinutesLeft` smallint(6) NOT NULL,
  KEY `FX_ASSESMENTCONTEXT_ASSESMENT_idx` (`AssesmentID`),
  CONSTRAINT `FX_ASSESMENTCONTEXT_ASSESMENT` FOREIGN KEY (`AssesmentID`) REFERENCES `assesment` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assesment_context`
--

LOCK TABLES `assesment_context` WRITE;
/*!40000 ALTER TABLE `assesment_context` DISABLE KEYS */;
INSERT INTO `assesment_context` (`AssesmentID`, `SectionIndex`, `QuestionIndex`, `MinutesLeft`) VALUES (1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60),(1,1,1,60);
/*!40000 ALTER TABLE `assesment_context` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `company` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `LogoPath` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company`
--

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` (`ID`, `Name`, `LogoPath`) VALUES (1,'Perceptio','logo-perceptio.png');
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluation`
--

DROP TABLE IF EXISTS `evaluation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evaluation` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(300) NOT NULL,
  `RoleID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluation`
--

LOCK TABLES `evaluation` WRITE;
/*!40000 ALTER TABLE `evaluation` DISABLE KEYS */;
INSERT INTO `evaluation` (`ID`, `Name`, `Description`, `RoleID`) VALUES (1,'EVALUACIÓN DESARROLLADOR','Esta prueba mide las habilidades como desarrollador de software en diversos aspectos.',NULL);
/*!40000 ALTER TABLE `evaluation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `question`
--

DROP TABLE IF EXISTS `question`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `question` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Text` varchar(500) NOT NULL,
  `Type` varchar(20) NOT NULL,
  `SectionID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_QUESTION_SECTION_idx` (`SectionID`),
  CONSTRAINT `FK_QUESTION_SECTION` FOREIGN KEY (`SectionID`) REFERENCES `section` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `question`
--

LOCK TABLES `question` WRITE;
/*!40000 ALTER TABLE `question` DISABLE KEYS */;
INSERT INTO `question` (`ID`, `Text`, `Type`, `SectionID`) VALUES (1,'El número de posibles reordenamientos de las letras de la palabra perro que no empiezan por la letra p es:','Simple',1),(2,'Si Ángela habla más bajo que Rosa y Celia habla más alto que Rosa, ¿habla Ángela más alto o más bajo que Celia?','Simple',1),(3,'La nota media conseguida en una clase de 20 alumnos ha sido de 6. Ocho alumnos han suspendido con un 3 y el resto superó el 5. ¿Cuál es la nota media de los alumnos aprobados?','Simple',2);
/*!40000 ALTER TABLE `question` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `section`
--

DROP TABLE IF EXISTS `section`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `section` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `EstimatedDuration` double DEFAULT NULL,
  `EvaluationID` int(11) DEFAULT NULL,
  `Type` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FX_SECTION_EVALUATION_idx` (`EvaluationID`),
  CONSTRAINT `FX_SECTION_EVALUATION` FOREIGN KEY (`EvaluationID`) REFERENCES `evaluation` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `section`
--

LOCK TABLES `section` WRITE;
/*!40000 ALTER TABLE `section` DISABLE KEYS */;
INSERT INTO `section` (`ID`, `Name`, `Description`, `EstimatedDuration`, `EvaluationID`, `Type`) VALUES (1,'Capacidad de análisis','Esta prueba mide la capacidad de entendimiento y solución a diversos problemas.',1,1,'Internal'),(2,'Lógica de Programación','Esta prueba mide la capacidad para crear, entender y modificar algoritmos.',2,1,'Internal');
/*!40000 ALTER TABLE `section` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'pro_evaluation'
--

--
-- Dumping routines for database 'pro_evaluation'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-13 19:51:33
